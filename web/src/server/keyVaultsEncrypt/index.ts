import { getServerDBConfig } from '@/config/db';

interface DecryptionResult {
  plaintext: string;
  wasAuthentic: boolean;
}

export class KeyVaultsGateKeeper {
  private aesKey: CryptoKey;

  constructor(aesKey: CryptoKey) {
    this.aesKey = aesKey;
  }

  static initWithEnvKey = async () => {
    const rawKey = Uint8Array.from(atob('askdhfkjahsdfkhaskjdfhjka123asd'), c => c.charCodeAt(0)); // 确保密钥是32字节（256位）
    const aesKey = await crypto.subtle.importKey(
      'raw',
      rawKey,
      { length: 256, name: 'AES-GCM' },
      false,
      ['encrypt', 'decrypt'],
    );
    return new KeyVaultsGateKeeper(aesKey);
  };

  /**
   * encrypt user private data
   */
  encrypt = async (keyVault: string): Promise<string> => {
    const iv = crypto.getRandomValues(new Uint8Array(12)); // 对于GCM，推荐使用12字节的IV
    const encodedKeyVault = new TextEncoder().encode(keyVault);

    const encryptedData = await crypto.subtle.encrypt(
      {
        iv: iv,
        name: 'AES-GCM',
      },
      this.aesKey,
      encodedKeyVault,
    );

    const authTag = new Uint8Array(encryptedData.slice(-16)); // 认证标签在加密数据的最后16字节
    const encrypted = new Uint8Array(encryptedData.slice(0, -16)); // 剩下的是加密数据

    const ivHex = Array.from(iv).map(byte => byte.toString(16).padStart(2, '0')).join('');
    const authTagHex = Array.from(authTag).map(byte => byte.toString(16).padStart(2, '0')).join('');
    const encryptedHex = Array.from(encrypted).map(byte => byte.toString(16).padStart(2, '0')).join('');

    return `${ivHex}:${authTagHex}:${encryptedHex}`;
  };

  // 假设密钥和加密数据是从外部获取的
  decrypt = async (encryptedData: string): Promise<DecryptionResult> => {
    const parts = encryptedData.split(':');
    if (parts.length !== 3) {
      throw new Error('Invalid encrypted data format');
    }

    const ivHex = parts[0];
    const authTagHex = parts[1];
    const encryptedHex = parts[2];

    const iv = new Uint8Array(ivHex.match(/.{1,2}/g)!.map(byte => parseInt(byte, 16)));
    const authTag = new Uint8Array(authTagHex.match(/.{1,2}/g)!.map(byte => parseInt(byte, 16)));
    const encrypted = new Uint8Array(encryptedHex.match(/.{1,2}/g)!.map(byte => parseInt(byte, 16)));

    // 合并加密数据和认证标签
    const combined = new Uint8Array([...encrypted, ...authTag]);

    try {
      const decryptedBuffer = await crypto.subtle.decrypt(
        {
          iv: iv,
          name: 'AES-GCM',
        },
        this.aesKey,
        combined,
      );

      const decrypted = new TextDecoder().decode(decryptedBuffer);
      return {
        plaintext: decrypted,
        wasAuthentic: true,
      };
    } catch {
      return {
        plaintext: '',
        wasAuthentic: false,
      };
    }
  };
}

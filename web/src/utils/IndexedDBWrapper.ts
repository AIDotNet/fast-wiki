// db.ts
class IndexedDBWrapper {
    private dbName: string;
    private dbVersion: number;
    private storeName: string;
    private db: IDBDatabase | null = null;
  
    constructor(dbName: string, dbVersion: number, storeName: string) {
      this.dbName = dbName;
      this.dbVersion = dbVersion;
      this.storeName = storeName;
    }
  
    open(): Promise<IDBDatabase> {
      return new Promise((resolve, reject) => {
        const request = indexedDB.open(this.dbName, this.dbVersion);
  
        request.onupgradeneeded = () => {
          this.db = request.result;
          if (!this.db.objectStoreNames.contains(this.storeName)) {
            this.db.createObjectStore(this.storeName, { keyPath: 'id', autoIncrement: true });
          }
        };
  
        request.onsuccess = () => {
          this.db = request.result;
          resolve(this.db);
        };
  
        request.onerror = () => {
          reject(request.error);
        };
      });
    }
  
    close(): void {
      this.db?.close();
      this.db = null;
    }
  
    add<T>(data: T): Promise<number> {
      return new Promise((resolve, reject) => {
        const transaction = this.db!.transaction(this.storeName, 'readwrite');
        const store = transaction.objectStore(this.storeName);
        const request = store.add(data);
  
        request.onsuccess = () => {
          resolve(request.result as number);
        };
  
        request.onerror = () => {
          reject(request.error);
        };
      });
    }
  
    get<T>(id: number): Promise<T | undefined> {
      return new Promise((resolve, reject) => {
        const transaction = this.db!.transaction(this.storeName, 'readonly');
        const store = transaction.objectStore(this.storeName);
        const request = store.get(id);
  
        request.onsuccess = () => {
          resolve(request.result as T);
        };
  
        request.onerror = () => {
          reject(request.error);
        };
      });
    }
  
    getAll<T>(): Promise<T[]> {
      return new Promise((resolve, reject) => {
        const transaction = this.db!.transaction(this.storeName, 'readonly');
        const store = transaction.objectStore(this.storeName);
        const request = store.getAll();
  
        request.onsuccess = () => {
          resolve(request.result as T[]);
        };
  
        request.onerror = () => {
          reject(request.error);
        };
      });
    }
  
    update<T>(id: number, data: T): Promise<void> {
      return new Promise((resolve, reject) => {
        const transaction = this.db!.transaction(this.storeName, 'readwrite');
        const store = transaction.objectStore(this.storeName);
        const request = store.put(data, id);
  
        request.onsuccess = () => {
          resolve();
        };
  
        request.onerror = () => {
          reject(request.error);
        };
      });
    }
    
    // 只更新部分字段
    updatePartial<T>(id: number, data: Partial<T>): Promise<void> {
        return new Promise((resolve, reject) => {
            const transaction = this.db!.transaction(this.storeName, 'readwrite');
            const store = transaction.objectStore(this.storeName);
            const request = store.get(id);
    
            request.onsuccess = () => {
                const oldData = request.result;
                const newData = { ...oldData, ...data };
                const requestUpdate = store.put(newData, id);
    
                requestUpdate.onsuccess = () => {
                    resolve();
                };
    
                requestUpdate.onerror = () => {
                    reject(requestUpdate.error);
                };
            };
    
            request.onerror = () => {
                reject(request.error);
            };
        });
    }


    delete(id: number): Promise<void> {
      return new Promise((resolve, reject) => {
        const transaction = this.db!.transaction(this.storeName, 'readwrite');
        const store = transaction.objectStore(this.storeName);
        const request = store.delete(id);
  
        request.onsuccess = () => {
          resolve();
        };
  
        request.onerror = () => {
          reject(request.error);
        };
      });
    }

    deleteStr(id: string): Promise<void> {
        return new Promise((resolve, reject) => {
            const transaction = this.db!.transaction(this.storeName, 'readwrite');
            const store = transaction.objectStore(this.storeName);
            const request = store.delete(id);
    
            request.onsuccess = () => {
            resolve();
            };
    
            request.onerror = () => {
            reject(request.error);
            };
        });
    }
  }
  
  export { IndexedDBWrapper };
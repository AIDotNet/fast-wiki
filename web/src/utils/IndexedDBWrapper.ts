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
  
        request.onupgradeneeded = (event: IDBVersionChangeEvent) => {
          this.db = request.result;
          if (!this.db.objectStoreNames.contains(this.storeName)) {
            this.db.createObjectStore(this.storeName, { keyPath: 'id', autoIncrement: true });
          }
        };
  
        request.onsuccess = (event: Event) => {
          this.db = request.result;
          resolve(this.db);
        };
  
        request.onerror = (event: Event) => {
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
  
        request.onsuccess = (event: Event) => {
          resolve(request.result as number);
        };
  
        request.onerror = (event: Event) => {
          reject(request.error);
        };
      });
    }
  
    get<T>(id: number): Promise<T | undefined> {
      return new Promise((resolve, reject) => {
        const transaction = this.db!.transaction(this.storeName, 'readonly');
        const store = transaction.objectStore(this.storeName);
        const request = store.get(id);
  
        request.onsuccess = (event: Event) => {
          resolve(request.result as T);
        };
  
        request.onerror = (event: Event) => {
          reject(request.error);
        };
      });
    }
  
    getAll<T>(): Promise<T[]> {
      return new Promise((resolve, reject) => {
        const transaction = this.db!.transaction(this.storeName, 'readonly');
        const store = transaction.objectStore(this.storeName);
        const request = store.getAll();
  
        request.onsuccess = (event: Event) => {
          resolve(request.result as T[]);
        };
  
        request.onerror = (event: Event) => {
          reject(request.error);
        };
      });
    }
  
    update<T>(id: number, data: T): Promise<void> {
      return new Promise((resolve, reject) => {
        const transaction = this.db!.transaction(this.storeName, 'readwrite');
        const store = transaction.objectStore(this.storeName);
        const request = store.put(data, id);
  
        request.onsuccess = (event: Event) => {
          resolve();
        };
  
        request.onerror = (event: Event) => {
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
    
            request.onsuccess = (event: Event) => {
                const oldData = request.result;
                const newData = { ...oldData, ...data };
                const requestUpdate = store.put(newData, id);
    
                requestUpdate.onsuccess = (event: Event) => {
                    resolve();
                };
    
                requestUpdate.onerror = (event: Event) => {
                    reject(requestUpdate.error);
                };
            };
    
            request.onerror = (event: Event) => {
                reject(request.error);
            };
        });
    }


    delete(id: number): Promise<void> {
      return new Promise((resolve, reject) => {
        const transaction = this.db!.transaction(this.storeName, 'readwrite');
        const store = transaction.objectStore(this.storeName);
        const request = store.delete(id);
  
        request.onsuccess = (event: Event) => {
          resolve();
        };
  
        request.onerror = (event: Event) => {
          reject(request.error);
        };
      });
    }

    deleteStr(id: string): Promise<void> {
        return new Promise((resolve, reject) => {
            const transaction = this.db!.transaction(this.storeName, 'readwrite');
            const store = transaction.objectStore(this.storeName);
            const request = store.delete(id);
    
            request.onsuccess = (event: Event) => {
            resolve();
            };
    
            request.onerror = (event: Event) => {
            reject(request.error);
            };
        });
    }
  }
  
  export { IndexedDBWrapper };
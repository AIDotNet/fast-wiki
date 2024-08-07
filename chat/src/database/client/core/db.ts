import Dexie from 'dexie';

import { DB_File } from '../schemas/files';
import { DB_Message } from '../schemas/message';
import { DBModel, THOR_CHAT_LOCAL_DB_NAME } from './types/db';

export interface LobeDBSchemaMap {
  files: DB_File;
  messages: DB_Message;
}

// Define a local DB
export class BrowserDB extends Dexie {
  public files: BrowserDBTable<'files'>;
  public messages: BrowserDBTable<'messages'>;

  constructor() {
    super(THOR_CHAT_LOCAL_DB_NAME);

    this.files = this.table('files');
    this.messages = this.table('messages');
  }

}

export const browserDB = new BrowserDB();

// types helper
export type BrowserDBSchema = {
  [t in keyof LobeDBSchemaMap]: {
    model: LobeDBSchemaMap[t];
    table: Dexie.Table<DBModel<LobeDBSchemaMap[t]>, string>;
  };
};
type BrowserDBTable<T extends keyof LobeDBSchemaMap> = BrowserDBSchema[T]['table'];

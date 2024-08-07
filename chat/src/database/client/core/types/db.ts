import { z } from 'zod';

export type DBModel<T> = T & {
  createdAt: number;
  id: string;
  updatedAt: number;
};

export const DBBaseFieldsSchema = z.object({
  createdAt: z.number().or(z.string()),
  id: z.string(),
  updatedAt: z.number().or(z.string()),
});

export const THOR_CHAT_LOCAL_DB_NAME = 'THOR_CHAT_DB';

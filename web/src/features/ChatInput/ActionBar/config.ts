import Clear from './Clear';
import History from './History';
import Token from './Token';
export const actionMap = {
  clear: Clear,
  history: History,
  token: Token,
} as const;

type ActionMap = typeof actionMap;

export type ActionKeys = keyof ActionMap;

type getActionList = (mobile?: boolean) => ActionKeys[];

// we can make these action lists configurable in the future
export const getLeftActionList: getActionList = (mobile) =>
  [ 'history',  'token'].filter(
    Boolean,
  ) as ActionKeys[];

export const getRightActionList: getActionList = () => ['clear'].filter(Boolean) as ActionKeys[];

import Clear from './Clear';
export const actionMap = {
  clear: Clear,
} as const;

type ActionMap = typeof actionMap;

export type ActionKeys = keyof ActionMap;

type getActionList = (mobile?: boolean) => ActionKeys[];

// we can make these action lists configurable in the future
export const getLeftActionList: getActionList = () =>
  [].filter(
    Boolean,
  ) as ActionKeys[];

export const getRightActionList: getActionList = () => ['clear'].filter(Boolean) as ActionKeys[];

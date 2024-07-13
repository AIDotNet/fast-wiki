export const generateRandomRoomName = () => {
  const randomString = Math.random().toString(36).substring(2, 7);
  return randomString;
};

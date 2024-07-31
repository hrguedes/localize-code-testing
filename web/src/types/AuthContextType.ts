export type AuthContextType = {
  email: string | null;
  id: string | null;
  nome: string | null;
  token: string | null;
  isAuthenticated: boolean;
  Logout: () => void;
  LogIn: () => void;
};

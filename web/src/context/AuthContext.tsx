"use client";
import { AuthContextType } from "@/types/AuthContextType";
import { createContext, useEffect, useState } from "react";
import Cookies from "js-cookie";

// context
export const AuthContext = createContext<AuthContextType>({
  email: "",
  nome: "",
  token: "",
  id: "",
  isAuthenticated: false,
  Logout: () => {},
  LogIn: () => {}
});

// provider
export const AuthProvider = ({ children }: any) => {
  const [email, setEmail] = useState<string | null>(null);
  const [nome, setNome] = useState<string | null>(null);
  const [token, setToken] = useState<string | null>(null);
  const [id, setId] = useState<string | null>(null);
  const [isAuthenticated, setIsAuthenticated] = useState<boolean>(false);

  useEffect(() => {
    const email = Cookies.get("localize.email");
    const nome = Cookies.get("localize.nome");
    const token = Cookies.get("localize.token");
    const id = Cookies.get("localize.id");
    if (email && nome && token && id) {
      setEmail(email);
      setNome(nome);
      setToken(token);
      setId(id);
      setIsAuthenticated(true);
    }
  }, [nome, email, token, id]);

  function Logout() {
    Cookies.remove("localize.email");
    Cookies.remove("localize.name");
    Cookies.remove("localize.token");
    Cookies.remove("localize.id");
    setEmail(null);
    setNome(null);
    setToken(null);
    setId(null);
    setIsAuthenticated(false);
  }

  function LogIn() {
    setIsAuthenticated(true);
  }

  return (
    <AuthContext.Provider
      value={{
        email,
        nome,
        id,
        token,
        isAuthenticated,
        Logout,
        LogIn
      }}
    >
      {isAuthenticated ? (
        <>
          <h1> Logado </h1>
          {children}
        </>
      ) : (
        <>{children}</>
      )}
    </AuthContext.Provider>
  );
};

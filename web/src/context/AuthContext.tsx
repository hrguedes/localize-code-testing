"use client";
import { AuthContextType } from "@/types/AuthContextType";
import { createContext, useEffect, useState } from "react";
import Cookies from "js-cookie"
import Header from "@/components/layout/Header";
import { ItemMenu } from "@/components/layout/Menu";
import { usePathname } from "next/navigation";

// context
export const AuthContext = createContext<AuthContextType>({
  email: "",
  nome: "",
  token: "",
  id: "",
  isAuthenticated: false,
  Logout: () => {},
  LogIn: () => {},
});

// Static Menu Itens
const itemsMenu: ItemMenu[] = [
  {
    label: "Home",
    url: "/",
    active: true,
  },
  {
    label: "Operações",
    url: "/operacoes/clientes/listar",
    active: false,
  },
];

// provider
export const AuthProvider = ({ children }: any) => {
  var route = usePathname();
  const [menuItems, setMenuItems] = useState<ItemMenu[]>(itemsMenu);
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

    var menu = menuItems.map((m) => {
      if (m.url === route) m.active = true;
      else m.active = false;
      return m;
    });
    setMenuItems(menu);
  }, [nome, email, token, id, route]);

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
    const token = Cookies.get("localize.token");
    if (token)
    {
      setToken(token);
      setIsAuthenticated(true);
    } else {
      
    }
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
        LogIn,
      }}
    >
      {isAuthenticated ? (
        <>
          <div className="flex min-h-screen w-full flex-col">
            <Header
              navbarProps={{ menuItems: menuItems }}
              userMenuProps={{ usuario: nome ?? "" }}
            />
            <main className="flex min-h-[calc(100vh_-_theme(spacing.16))] flex-1 flex-col gap-4 bg-muted/40 p-4 md:gap-8 md:p-10">
              {children}
            </main>
          </div>
        </>
      ) : (
        <>{children}</>
      )}
    </AuthContext.Provider>
  );
};

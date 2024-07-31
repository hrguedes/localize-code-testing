"use client";

import { AuthContext } from "@/context/AuthContext";
import { useRouter } from 'next/navigation';
import { useContext, useEffect } from "react";

export default function Page() {
  const router = useRouter();
  const { Logout } = useContext(AuthContext);

  useEffect(() => {
    Logout();
    router.push("/auth/login");
  }, []);

  return (
    <>
      <h1> Sair </h1>
    </>
  );
}

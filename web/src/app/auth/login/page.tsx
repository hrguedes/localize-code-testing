"use client";

import Image from "next/image";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { ChangeEvent, useContext, useState } from "react";
import AuthenticateUsuarioRequest from "@/models/authentication/AuthenticateUsuarioRequest";
import { authenticarUsuarioAsync } from "@/services/AuthenticationService";
import { Alert, AlertDescription, AlertTitle } from "@/components/ui/alert";
import { Terminal } from "lucide-react";
import { useRouter } from "next/navigation";
import Cookies from "js-cookie";
import { Loader2 } from "lucide-react";
import { AuthContext } from "@/context/AuthContext";

export default function Dashboard() {
  const router = useRouter();
  const { LogIn } = useContext(AuthContext);
  const [disable, setDisable] = useState<boolean>(false);
  const [title, setTitle] = useState<string>("Atenção!");
  const [message, setMessage] = useState<string>("");
  const [exibirAlerta, setExibirAlerta] = useState<boolean>(false);
  const [form, setForm] = useState<AuthenticateUsuarioRequest>({
    Email: "",
    Senha: "",
  });

  const handlerInput = (value: any, prop: string) => {
    setForm((state) => ({ ...state, [prop]: value }));
  };

  const handlerForm = async () => {
    setDisable(true);
    var response = await authenticarUsuarioAsync(form);
    if (response != null) {
      if (response.Ok) {
        if (response.Value != null) {
          localStorage.setItem("refresh_page", "0");
          Cookies.set("localize.email", response.Value.Email);
          Cookies.set("localize.nome", response.Value.Nome);
          Cookies.set("localize.id", response.Value.Id);
          Cookies.set("localize.token", response.Value.Token);
          LogIn();
          router.push("/");
        } else {
          setMessage("Ocorreu um erro");
          setExibirAlerta(true);
        }
      } else if (response.ErrorMessages != null) {
        setTitle(response.ErrorMessages[0].Key ?? "");
        setMessage(response.ErrorMessages[0].Message ?? "");
        setExibirAlerta(true);
      } else {
        setMessage(response.Message ?? "");
        setExibirAlerta(true);
      }
    }
    setDisable(false);
  };

  return (
    <div className="w-full h-lvh lg:p-0 lg:grid lg:grid-cols-2">
      <div className="hidden lg:block">
        <Image
          src="/wallpaper02.webp"
          alt="Image"
          width="1920"
          height="1080"
          className="h-full w-full"
        />
      </div>
      <div className="flex items-center justify-center py-12">
        <div className="mx-auto grid w-[350px] gap-6">
          <div className="grid gap-2 text-center">
            <h1 className="text-3xl font-bold">Autenticar</h1>
            <p className="text-balance text-muted-foreground">
              Informe o seu Email e senha
            </p>
          </div>
          <div className="grid gap-4">
            <div className="grid gap-2">
              <Label htmlFor="email">Email</Label>
              <Input
                disabled={disable}
                onChange={(event: ChangeEvent<HTMLInputElement>) =>
                  handlerInput(event.target.value, "email")
                }
                id="email"
                type="email"
                value={form.Email}
                placeholder="joao@joao.com"
                required
              />
            </div>
            <div className="grid gap-2">
              <div className="flex items-center">
                <Label htmlFor="password">Senha</Label>
              </div>
              <Input
                id="password"
                type="password"
                disabled={disable}
                value={form.Senha}
                onChange={(event: ChangeEvent<HTMLInputElement>) =>
                  handlerInput(event.target.value, "senha")
                }
                required
              />
            </div>
            <div className="grid gap-2">
              {exibirAlerta && (
                <>
                  <Alert variant="destructive">
                    <Terminal className="h-4 w-4" />
                    <AlertTitle>{title}</AlertTitle>
                    <AlertDescription>{message}</AlertDescription>
                  </Alert>
                </>
              )}
            </div>
            <Button
              disabled={disable}
              type="button"
              className="w-full"
              onClick={handlerForm}
            >
              {disable && <Loader2 className="mr-2 h-4 w-4 animate-spin" />}
              Entrar
            </Button>
          </div>
        </div>
      </div>
    </div>
  );
}

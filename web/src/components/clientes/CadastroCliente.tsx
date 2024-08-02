"use client";

import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import {
  Sheet,
  SheetContent,
  SheetDescription,
  SheetFooter,
  SheetHeader,
  SheetTitle,
  SheetTrigger,
} from "@/components/ui/sheet";
import UpdateOrCreateClienteRequest from "@/models/operacoes/clientes/UpdateOrCreateClienteRequest";
import {
  atualizarClienteAsync,
  cadastrarClienteAsync,
} from "@/services/ClienteService";
import { Contact, User, UserPen } from "lucide-react";
import { ChangeEvent, useState } from "react";
import { Alert, AlertDescription, AlertTitle } from "@/components/ui/alert";
import { Terminal } from "lucide-react";

export type CadastrarClienteProps = {
  update: boolean;
  disabled: boolean;
  clienteUpdate: UpdateOrCreateClienteRequest | null;
  callback: () => void;
};

const defaultCliente: UpdateOrCreateClienteRequest = {
  Nome: "",
  Documento: "",
  Telefone: "",
  Endereco: "",
  Id: 0,
};

export default function CadastrarCliente({
  callback,
  clienteUpdate,
  disabled,
  update,
}: CadastrarClienteProps) {
  const [disable, setDisable] = useState<boolean>(disabled);
  const [title, setTitle] = useState<string>("Atenção!");
  const [message, setMessage] = useState<string>("");
  const [exibirAlerta, setExibirAlerta] = useState<boolean>(false);
  const [cliente, setCliente] = useState<UpdateOrCreateClienteRequest>(
    clienteUpdate ?? defaultCliente
  );

  const handlerForm = async () => {
    setDisable(true);
    var response: any;
    if (update) {
      response = await atualizarClienteAsync(cliente);
    } else {
      response = await cadastrarClienteAsync(cliente);
    }
    if (response != null) {
      if (response.Ok) {
        setCliente(defaultCliente);
        callback();
      } else if (response.ErrorMessages != null) {
        setTitle(response.ErrorMessages[0].Key ?? "");
        setMessage(response.ErrorMessages[0].Message ?? "");
        setExibirAlerta(true);
      } else {
        setMessage(response.Message ?? "");
        setExibirAlerta(true);
      }
    } else {
      setExibirAlerta(false);
    }
    setDisable(false);
  };

  return (
    <Sheet>
      <SheetTrigger asChild>
        <Button className="ml-2" variant="outline">
          {update ? (
            <>
              {disabled ? (
                <>
                  <Contact className="mr-2 h-4 w-4" /> Detalhes
                </>
              ) : (
                <>
                  <UserPen className="mr-2 h-4 w-4" /> Editar Cliente
                </>
              )}
            </>
          ) : (
            <>
              <User className="mr-2 h-4 w-4" /> Cadastrar Cliente
            </>
          )}
        </Button>
      </SheetTrigger>
      <SheetContent>
        <SheetHeader>
          <SheetTitle>{update ? "Editar Cliente" : "Novo Cliente"}</SheetTitle>
          <SheetDescription>Informe os dados do cliente</SheetDescription>
        </SheetHeader>
        <div className="grid gap-4 py-4">
          <div className="grid grid-cols-4 items-center gap-4">
            <Label htmlFor="Nome" className="text-right">
              Nome
            </Label>
            <Input
              disabled={disable}
              id="Nome"
              value={cliente.Nome}
              onChange={(event: ChangeEvent<HTMLInputElement>) =>
                setCliente((state) => ({
                  ...state,
                  ["Nome"]: event.target.value,
                }))
              }
              className="col-span-3"
            />
          </div>
          <div className="grid grid-cols-4 items-center gap-4">
            <Label htmlFor="Documento" className="text-right">
              Documento
            </Label>
            <Input
              disabled={disable}
              id="Documento"
              value={cliente.Documento}
              onChange={(event: ChangeEvent<HTMLInputElement>) =>
                setCliente((state) => ({
                  ...state,
                  ["Documento"]: event.target.value,
                }))
              }
              className="col-span-3"
            />
          </div>
          <div className="grid grid-cols-4 items-center gap-4">
            <Label htmlFor="Telefone" className="text-right">
              Telefone
            </Label>
            <Input
              disabled={disable}
              id="Telefone"
              value={cliente.Telefone}
              onChange={(event: ChangeEvent<HTMLInputElement>) =>
                setCliente((state) => ({
                  ...state,
                  ["Telefone"]: event.target.value,
                }))
              }
              className="col-span-3"
            />
          </div>
          <div className="grid grid-cols-4 items-center gap-4">
            <Label htmlFor="Endereco" className="text-right">
              Endereço
            </Label>
            <Input
              disabled={disable}
              id="Endereco"
              value={cliente.Endereco}
              onChange={(event: ChangeEvent<HTMLInputElement>) =>
                setCliente((state) => ({
                  ...state,
                  ["Endereco"]: event.target.value,
                }))
              }
              className="col-span-3"
            />
          </div>
        </div>
        <SheetFooter>
          {!disabled && (
            <Button type="button" onClick={handlerForm}>
              Salvar
            </Button>
          )}
        </SheetFooter>
        {exibirAlerta && (
          <>
            <Alert variant="destructive" className="mt-2">
              <Terminal className="h-4 w-4" />
              <AlertTitle>{title}</AlertTitle>
              <AlertDescription>{message}</AlertDescription>
            </Alert>
          </>
        )}
      </SheetContent>
    </Sheet>
  );
}

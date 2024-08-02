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
import { HandCoins } from "lucide-react";
import { ChangeEvent, useState } from "react";
import { Alert, AlertDescription, AlertTitle } from "@/components/ui/alert";
import { Terminal } from "lucide-react";
import UpdateOrCreateCobrancaRequest from "@/models/operacoes/cobrancas/UpdateOrCreateCobrancaRequest";
import {
  atualizarCobrancaAsync,
  cadastrarCobrancaAsync,
} from "@/services/CobrancaService";
import { format } from "date-fns";
import { Calendar as CalendarIcon } from "lucide-react";
import { cn } from "@/lib/utils";
import { Calendar } from "@/components/ui/calendar";
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from "@/components/ui/popover";

export type CadastroCobrancaProps = {
  update: boolean;
  clienteId: number;
  clienteNome: string;
  cobrancaUpdate: UpdateOrCreateCobrancaRequest | null;
  callback: () => void;
};

export default function CadastroCobranca({
  callback,
  cobrancaUpdate,
  update,
  clienteId = 0,
  clienteNome = "",
}: CadastroCobrancaProps) {
  const [date, setDate] = useState<Date>();
  const [disable, setDisable] = useState<boolean>(false);
  const [title, setTitle] = useState<string>("Atenção!");
  const [message, setMessage] = useState<string>("");
  const [exibirAlerta, setExibirAlerta] = useState<boolean>(false);
  const [cobranca, setCobranca] = useState<UpdateOrCreateCobrancaRequest>(
    cobrancaUpdate ?? {
      Id: 0,
      Pago: false,
      Valor: 1.99,
      Descricao: "",
      DataVencimento: new Date(),
      ClienteId: clienteId,
    }
  );

  const handlerForm = async () => {
    setDisable(true);
    var response: any;
    cobranca.ClienteId = clienteId;
    if (update) {
      response = await atualizarCobrancaAsync(cobranca);
    } else {
      response = await cadastrarCobrancaAsync(cobranca);
    }
    if (response != null) {
      if (response.Ok) {
        setCobranca({
          Id: 0,
          Pago: false,
          Valor: 1.99,
          Descricao: "",
          DataVencimento: new Date(),
          ClienteId: 0,
        });
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
              <HandCoins className="mr-2 h-4 w-4" /> Editar Cobrança
            </>
          ) : (
            <>
              <HandCoins className="mr-2 h-4 w-4" /> Nova cobrança
            </>
          )}
        </Button>
      </SheetTrigger>
      <SheetContent>
        <SheetHeader>
          <SheetTitle>
            {update ? "Editar cobrança" : "Nova cobrança"}
          </SheetTitle>
          <SheetDescription>Informe os dados da cobrança</SheetDescription>
        </SheetHeader>
        <div className="grid gap-4 py-4">
          <div className="w-full">
            <Input
              disabled={true}
              id="Cliente"
              value={clienteNome}
              className="col-span-3"
            />
          </div>
          <div className="grid grid-cols-4 items-center gap-4">
            <Label htmlFor="Valor" className="text-right">
              Valor R$
            </Label>
            <Input
              disabled={disable}
              id="Valor"
              type="number"
              value={cobranca.Valor}
              onChange={(event: ChangeEvent<HTMLInputElement>) =>
                setCobranca((state) => ({
                  ...state,
                  ["Valor"]: Number(event.target.value),
                }))
              }
              className="col-span-3"
            />
          </div>
          <div className="grid grid-cols-4 items-center gap-4">
            <Label htmlFor="Descricao" className="text-right">
              Descrição
            </Label>
            <Input
              disabled={disable}
              id="Nome"
              value={cobranca.Descricao}
              onChange={(event: ChangeEvent<HTMLInputElement>) =>
                setCobranca((state) => ({
                  ...state,
                  ["Descricao"]: event.target.value,
                }))
              }
              className="col-span-3"
            />
          </div>
          <div className="grid grid-cols-4 items-center gap-4">
            <Label htmlFor="DataVencimento" className="text-right">
              Data de Vencimento
            </Label>
            <Popover>
              <PopoverTrigger asChild>
                <Button
                  disabled={disable}
                  variant={"outline"}
                  className={cn(
                    "w-[280px] justify-start text-left font-normal",
                    !date && "text-muted-foreground"
                  )}
                >
                  <CalendarIcon className="mr-2 h-4 w-4" />
                  {date ? format(date, "PPP") : <span>Informe a Data</span>}
                </Button>
              </PopoverTrigger>
              <PopoverContent className="w-auto p-0">
                <Calendar
                  mode="single"
                  selected={date}
                  onSelect={setDate}
                  initialFocus
                />
              </PopoverContent>
            </Popover>
          </div>
        </div>
        <SheetFooter>
          <Button type="button" onClick={handlerForm}>
            Salvar
          </Button>
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

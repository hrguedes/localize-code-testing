"use client";

import { Button } from "@/components/ui/button";
import {
  Sheet,
  SheetClose,
  SheetContent,
  SheetDescription,
  SheetFooter,
  SheetHeader,
  SheetTitle,
  SheetTrigger,
} from "@/components/ui/sheet";
import CobrancaResponse from "@/models/operacoes/cobrancas/CobrancaResponse";
import { listarCobrancasAsync } from "@/services/CobrancaService";
import { DollarSign } from "lucide-react";
import { useEffect, useState } from "react";
import Cobranca from "./Cobranca";
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";

export type ListarCobrancasProps = {
  clienteId: number;
  callback: () => void;
};

export default function ListarCobrancas({
  callback,
  clienteId,
}: ListarCobrancasProps) {
  const [total, setTotal] = useState<number>(0);
  const [totalPorPagina, setTotalPorPagina] = useState<number>(5);
  const [totalRetornado, setTotalRetornado] = useState<number>(10);
  const [totalPagina, setTotalPaginas] = useState<number>(1);
  const [pagina, setPagina] = useState<number>(1);
  const [cobrancas, setCobrancas] = useState<CobrancaResponse[]>([]);

  const requestCobrancas = async () => {
    var params = new URLSearchParams();
    params.append("Pagina", pagina.toString());
    params.append("TotalPorPagina", totalPorPagina.toString());
    params.append("ClienteId", clienteId.toString());

    var response = await listarCobrancasAsync(params);
    if (response != null) {
      if (response.Ok) {
        if (
          response.Value != null &&
          response.Value.Rows != null &&
          response.Value.Rows.length > 0
        ) {
          setTotalPaginas(response.Value.TotalPaginas);
          setTotalRetornado(response.Value.Rows.length);
          setTotalPorPagina(response.Value.TotalPorPagina);
          setTotal(response.Value.Total);
          setCobrancas(response.Value?.Rows);
        }
      }
    }
  };

  useEffect(() => {
    requestCobrancas();
  }, [totalPagina, pagina, clienteId]);

  return (
    <Dialog>
      <DialogTrigger asChild>
        <Button className="ml-2" variant="outline">
          <DollarSign className="mr-2 h-4 w-4" /> Cobranças
        </Button>
      </DialogTrigger>
      <DialogContent>
        <DialogHeader>
          <DialogTitle>Cobranças</DialogTitle>
          <DialogDescription>
            Lista de Cobranças vinculados ao cliente
          </DialogDescription>
        </DialogHeader>
        <Cobranca cobrancas={cobrancas} callback={callback} />
        <DialogFooter>
          <Button type="button">Sair</Button>
        </DialogFooter>
      </DialogContent>
    </Dialog>
  );
}

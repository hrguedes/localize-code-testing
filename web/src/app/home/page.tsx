"use client";

import {
  Card,
  CardContent,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { cn } from "@/lib/utils";
import { Button } from "@/components/ui/button";
import { Check, Handshake } from "lucide-react";
import Link from "next/link";

export default function Page() {

  return (
    <>
      <h1 className="text-3xl font-semibold">Localize - Teste técnico</h1>
      <Card className={cn("w-[380px]")}>
        <CardHeader>
          <CardTitle>Operações</CardTitle>
          <CardDescription>Clientes, Débitos</CardDescription>
        </CardHeader>
        <CardContent className="grid gap-4">
          <div className=" flex items-center space-x-4 rounded-md border p-4">
            <Handshake />
            <div className="flex-1 space-y-1">
              <p className="text-sm font-medium leading-none">
                Controle de Débitos e Clientes
              </p>
              <p className="text-sm text-muted-foreground">
                Cadastros de Clientes e Debitos
              </p>
            </div>
          </div>
        </CardContent>
        <CardFooter>
          <Link href="/operacoes/clientes/listar">
            <Button className="w-full">
              <Check className="mr-2 h-4 w-4" /> Ir para Operações
            </Button>
          </Link>
        </CardFooter>
      </Card>
    </>
  );
}

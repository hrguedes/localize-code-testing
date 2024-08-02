"use client";

import {
  createColumnHelper,
  flexRender,
  getCoreRowModel,
  useReactTable,
} from "@tanstack/react-table";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import {
  Table,
  TableBody,
  TableCell,
  TableFooter,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { useEffect, useState } from "react";
import ClienteResponse from "@/models/operacoes/clientes/ClienteResponse";
import { listarClientesAsync, listarClientesDetalhesAsync } from "@/services/ClienteService";
import Swal from "sweetalert2";
import CadastrarCliente from "@/components/clientes/CadastroCliente";
import UpdateOrCreateClienteRequest from "@/models/operacoes/clientes/UpdateOrCreateClienteRequest";
import CadastroCobranca from "@/components/cobrancas/CadastrarCobranca";
import ListarCobrancas from "@/components/cobrancas/ListarCobrancas";
import { useRouter } from "next/navigation";
import { Badge } from "@/components/ui/badge";
import { listarCobrancasAsync } from "@/services/CobrancaService";
import { todayFormatString } from "@/lib/utils";

const columnHelper = createColumnHelper<ClienteResponse>();

export default function Page() {
  const router = useRouter();
  const [total, setTotal] = useState<number>(0);
  const [totalPorPagina, setTotalPorPagina] = useState<number>(5);
  const [totalRetornado, setTotalRetornado] = useState<number>(10);
  const [pesquisa, setPesquisa] = useState<string>("");
  const [totalPagina, setTotalPaginas] = useState<number>(1);
  const [pagina, setPagina] = useState<number>(1);
  const [atualizar, setAtualizar] = useState<boolean>(false);
  const [clientes, setClientes] = useState<ClienteResponse[]>(() => [...[]]);

  const editarCliente = (id: number): UpdateOrCreateClienteRequest => {
    var cliente = clientes.filter((f) => f.Id === id)[0];
    return {
      Id: cliente.Id,
      Nome: cliente.Nome,
      Documento: cliente.Documento,
      Telefone: cliente.Telefone,
      Endereco: cliente.Endereco,
    };
  };

  const callback = () => {
    Swal.fire("Succeso", "A operação foi concluída com sucesso", "success");
    setAtualizar(true);
    router.refresh();
  };

  const requestClients = async () => {
    var params = new URLSearchParams();
    params.append("Pagina", pagina.toString());
    params.append("TotalPorPagina", totalPorPagina.toString());
    params.append("Pesquisa", pesquisa);

    var response = await listarClientesDetalhesAsync(params);
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
          setClientes(response.Value?.Rows);
        }
      } else {
        Swal.fire("Atenção", response.Message ?? "", "info");
      }
    }
  };

  useEffect(() => {
    requestClients();
  }, [atualizar, totalPagina, pagina, pesquisa]);

  const table = useReactTable({
    data: clientes,
    columns: [
      columnHelper.accessor("Nome", {
        cell: (info) => info.getValue(),
        footer: (info) => info.column.id,
      }),
      columnHelper.accessor("Aberto", {
        header: () => (
          <Badge className="flex justify-center" variant="outline">
            Em Aberto
          </Badge>
        ),
        cell: (info) => (
          <span className="flex justify-center font-bold">
            {info.getValue()}
          </span>
        ),
        footer: (info) => info.column.id,
      }),
      columnHelper.accessor("EmAtraso", {
        header: () => (
          <Badge className="flex justify-center" variant="destructive">
            Atrasados
          </Badge>
        ),
        cell: (info) => (
          <span className="flex justify-center font-bold text-red-600">
            {info.getValue()}
          </span>
        ),
        footer: (info) => info.column.id,
      }),
      columnHelper.accessor("Pagos", {
        header: () => (
          <Badge className="flex justify-center bg-green-400">Pagos</Badge>
        ),
        cell: (info) => (
          <span className="flex justify-center font-bold text-green-400">
            {info.getValue()}
          </span>
        ),
        footer: (info) => info.column.id,
      }),
      columnHelper.accessor("Id", {
        header: () => <span className="flex justify-center">Ações</span>,
        cell: (info) => (
          <div className="flex justify-center">
            <CadastrarCliente
              disabled={false}
              callback={callback}
              clienteUpdate={editarCliente(info.getValue())}
              update={true}
            />
            <CadastrarCliente
              disabled={true}
              callback={callback}
              clienteUpdate={editarCliente(info.getValue())}
              update={true}
            />
            <CadastroCobranca
              callback={callback}
              cobrancaUpdate={null}
              clienteNome={editarCliente(info.getValue()).Nome}
              clienteId={info.getValue()}
              update={false}
            />
            <ListarCobrancas clienteId={info.getValue()} callback={callback} />
          </div>
        ),
        footer: (info) => info.column.id,
      }),
    ],
    getCoreRowModel: getCoreRowModel(),
  });

  return (
    <div className="w-full">
      <div className="flex items-center py-4">
        <Input
          placeholder="Pesquisar..."
          value={pesquisa}
          onChange={(event) => setPesquisa(event.target.value)}
          className="max-w-sm"
        />
        <CadastrarCliente
          callback={callback}
          disabled={false}
          clienteUpdate={null}
          update={false}
        />
      </div>
      <div className="rounded-md border">
        <Table>
          <TableHeader>
            {table.getHeaderGroups().map((headerGroup) => (
              <TableRow key={headerGroup.id}>
                {headerGroup.headers.map((header) => {
                  return (
                    <TableHead key={header.id}>
                      {header.isPlaceholder
                        ? null
                        : flexRender(
                            header.column.columnDef.header,
                            header.getContext()
                          )}
                    </TableHead>
                  );
                })}
              </TableRow>
            ))}
          </TableHeader>
          <TableBody>
            {table.getRowModel().rows?.length ? (
              table.getRowModel().rows.map((row) => (
                <TableRow
                  key={row.id}
                  data-state={row.getIsSelected() && "selected"}
                >
                  {row.getVisibleCells().map((cell) => (
                    <TableCell key={cell.id}>
                      {flexRender(
                        cell.column.columnDef.cell,
                        cell.getContext()
                      )}
                    </TableCell>
                  ))}
                </TableRow>
              ))
            ) : (
              <TableRow>
                <TableCell colSpan={5} className="h-24 text-center">
                  Nenhum resultado encontrado.
                </TableCell>
              </TableRow>
            )}
          </TableBody>
          <TableFooter>
            {table.getFooterGroups().map((footerGroup) => (
              <TableRow key={footerGroup.id}>
                {footerGroup.headers.map((header) => {
                  return (
                    <TableHead key={header.id}>
                      {header.isPlaceholder
                        ? null
                        : flexRender(
                            header.column.columnDef.header,
                            header.getContext()
                          )}
                    </TableHead>
                  );
                })}
              </TableRow>
            ))}
          </TableFooter>
        </Table>
      </div>
      <div className="flex items-center justify-end space-x-2 py-4">
        <div className="flex-1 text-sm text-muted-foreground">
          Exibíndo {totalRetornado} registros de {total}
        </div>
        <div className="space-x-2">
          <Button
            variant="outline"
            size="sm"
            onClick={() => setPagina(pagina - 1)}
            disabled={totalPagina != pagina}
          >
            Anterior
          </Button>
          <Button
            variant="outline"
            size="sm"
            onClick={() => setPagina(pagina + 1)}
            disabled={totalPagina <= pagina}
          >
            Próxima
          </Button>
        </div>
      </div>
    </div>
  );
}

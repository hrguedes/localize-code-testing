import {
  Table,
  TableBody,
  TableCaption,
  TableCell,
  TableFooter,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import CobrancaResponse from "@/models/operacoes/cobrancas/CobrancaResponse";
import { Switch } from "@/components/ui/switch";
import { atualizarCobrancaAsync } from "@/services/CobrancaService";
import Swal from "sweetalert2";

export type CobrancaProps = {
  cobrancas: CobrancaResponse[];
  callback: () => void;
};

export default function Cobranca({ cobrancas, callback }: CobrancaProps) {
  const valorTotal = cobrancas.reduce((sum, current) => sum + current.Valor, 0);

  const changeStatusCobranca = async (status: boolean, id: number | null) => {
    var cobrancaItem = cobrancas.find((f) => f.Id === id);
    if (cobrancaItem != null) {
      var response = await atualizarCobrancaAsync({
        Id: cobrancaItem.Id,
        Pago: status,
        Valor: null,
        Descricao: null,
        DataVencimento: null,
        ClienteId: null,
      });
      if (response != null) {
        if (response.Ok) {
          callback();
        } else {
          Swal.fire("Atenção", response.Message ?? "", "warning");
        }
      } else {
        Swal.fire("Atenção", "Ocorreu um erro", "error");
      }
    }
  };

  return (
    <Table>
      <TableHeader>
        <TableRow>
          <TableHead className="w-[100px]">Vencimento</TableHead>
          <TableHead>Valor</TableHead>
          <TableHead className="text-right">Descrição</TableHead>
          <TableHead className="text-right">Pago?</TableHead>
        </TableRow>
      </TableHeader>
      <TableBody>
        {cobrancas.map((cobranca) => (
          <TableRow key={cobranca.Id}>
            <TableCell className="font-medium">
              {cobranca.DataVencimento}
            </TableCell>
            <TableCell>{cobranca.Valor}</TableCell>
            <TableCell className="text-right">{cobranca.Descricao}</TableCell>
            <TableCell className="text-right">
              <Switch
                checked={cobranca.Pago}
                onCheckedChange={(value: boolean) =>
                  changeStatusCobranca(value, cobranca.Id)
                }
              />
            </TableCell>
          </TableRow>
        ))}
      </TableBody>
      <TableFooter>
        <TableRow>
          <TableCell colSpan={3}>Total</TableCell>
          <TableCell className="text-right">
            R$ {valorTotal.toFixed(2)}
          </TableCell>
        </TableRow>
      </TableFooter>
    </Table>
  );
}

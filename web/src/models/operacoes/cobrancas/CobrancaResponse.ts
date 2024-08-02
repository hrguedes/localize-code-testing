import BaseModel from "@/models/BaseModel";

export default interface CobrancaResponse extends BaseModel<number> {
  Pago: boolean;
  Valor: number;
  Descricao: string;
  DataVencimento: string;
  ClienteId: number;
}

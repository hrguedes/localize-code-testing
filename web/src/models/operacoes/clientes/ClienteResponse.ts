import BaseModel from "@/models/BaseModel";

export default interface ClienteResponse extends BaseModel<number> {
  Nome: string;
  Documento: string;
  Telefone: string;
  Endereco: string;
  Pagos: number;
  EmAtraso: number;
  Aberto: number;
}

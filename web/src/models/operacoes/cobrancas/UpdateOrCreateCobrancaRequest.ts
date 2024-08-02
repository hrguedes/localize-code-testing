export default interface UpdateOrCreateCobrancaRequest {
  Id: number | null;
  Pago: boolean | null;
  Valor: number | null;
  Descricao: string | null;
  DataVencimento: Date | null;
  ClienteId: number | null;
}

import CobrancaResponse from "@/models/operacoes/cobrancas/CobrancaResponse";
import UpdateOrCreateCobrancaRequest from "@/models/operacoes/cobrancas/UpdateOrCreateCobrancaRequest";
import { HttpResponse } from "@/models/responses/HttpResponse";
import PaginationResponse from "@/models/responses/PaginationResponse";
import { httpGetEncoded, httpPost, httpPut } from "@/services/context/BaseContex";

export const listarCobrancasAsync = async (
  search: URLSearchParams
): Promise<HttpResponse<PaginationResponse<CobrancaResponse>>> => {
  return await httpGetEncoded<PaginationResponse<CobrancaResponse>>(
    `/operacao/cobranca/all`,
    search
  );
};

export const cadastrarCobrancaAsync = async (
  command: UpdateOrCreateCobrancaRequest
): Promise<HttpResponse<CobrancaResponse>> => {
  return await httpPost<CobrancaResponse>("/operacao/cobranca", command);
};

export const atualizarCobrancaAsync = async (
  command: UpdateOrCreateCobrancaRequest
): Promise<HttpResponse<CobrancaResponse>> => {
  return await httpPut<CobrancaResponse>("/operacao/cobranca", command);
};


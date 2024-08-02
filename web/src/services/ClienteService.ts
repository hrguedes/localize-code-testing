import ClienteResponse from "@/models/operacoes/clientes/ClienteResponse";
import UpdateOrCreateClienteRequest from "@/models/operacoes/clientes/UpdateOrCreateClienteRequest";
import { HttpResponse } from "@/models/responses/HttpResponse";
import PaginationResponse from "@/models/responses/PaginationResponse";
import { httpGetEncoded, httpPost, httpPut } from "@/services/context/BaseContex";

export const listarClientesAsync = async (
  search: URLSearchParams
): Promise<HttpResponse<PaginationResponse<ClienteResponse>>> => {
  return await httpGetEncoded<PaginationResponse<ClienteResponse>>(
    `/operacao/clientes/all`,
    search
  );
};

export const listarClientesDetalhesAsync = async (
  search: URLSearchParams
): Promise<HttpResponse<PaginationResponse<ClienteResponse>>> => {
  return await httpGetEncoded<PaginationResponse<ClienteResponse>>(
    `/operacao/clientes/all/details`,
    search
  );
};

export const cadastrarClienteAsync = async (
  command: UpdateOrCreateClienteRequest
): Promise<HttpResponse<ClienteResponse>> => {
  return await httpPost<ClienteResponse>("/operacao/clientes", command);
};

export const atualizarClienteAsync = async (
  command: UpdateOrCreateClienteRequest
): Promise<HttpResponse<ClienteResponse>> => {
  return await httpPut<ClienteResponse>("/operacao/clientes", command);
};


import AuthenticateUsuarioRequest from "@/models/authentication/AuthenticateUsuarioRequest";
import AuthenticateUsuarioResponse from "@/models/authentication/AuthenticateUsuarioResponse";
import { HttpResponse } from "@/models/responses/HttpResponse";
import { httpPost } from "@/services/context/BaseContex";

export const authenticarUsuarioAsync = async (
  command: AuthenticateUsuarioRequest
): Promise<HttpResponse<AuthenticateUsuarioResponse>> => {
  return await httpPost<AuthenticateUsuarioResponse>(
    "/authentication",
    command
  );
};

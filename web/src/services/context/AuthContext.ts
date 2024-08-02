import axios, { AxiosInstance, AxiosResponse } from "axios";
import Swal from "sweetalert2";
import { HttpResponse } from "@/models/responses/HttpResponse";
import Cookies from "js-cookie";

const api: AxiosInstance = axios.create({
  baseURL: process.env.apiUrl,
});

const handleApiResponse = <T>(
  response: AxiosResponse<HttpResponse<T>>
): HttpResponse<T> => {
  if (response == null) {
    Swal.fire({
      icon: "error",
      title: "Oops...(500)",
      text: "Servidor fora do Ar, por favor entrar em contato com o suporte.",
    });
    return response;
  } else if (response.status === 404) {
    Swal.fire({
      icon: "error",
      title: "Oops...(404)",
      text: "A URL informada não foi encontrada, por favor entrar em contato com o suporte.",
    });
  } else if (response.status === 400) {
    var resp: HttpResponse<T> = {
      Value: response.data.Value,
      HttpStatusCode: 400,
      Message: response.data.Message,
      Ok: response.data.Ok,
      ErrorMessages: response.data.ErrorMessages,
      Exception: response.data.Exception,
    };
    return resp;
  }
  if (response.status === 500 || response.status === 502) {
    Swal.fire({
      icon: "error",
      title: "Oops...(500)",
      text: "Ocorreu um erro no servidor, por favor entrar em contato com o suporte",
    });
    var resp: HttpResponse<T> = {
      Value: response.data.Value,
      HttpStatusCode: 500,
      Message:
        "Ocorreu um erro no servidor, por favor entrar em contato com o suporte.",
      Ok: false,
      ErrorMessages: response.data.ErrorMessages,
      Exception: response.data.Exception,
    };
    return resp;
  }
  return response.data;
};

const httpGet = async <T>(
  url: string,
  params: any
): Promise<HttpResponse<T>> => {
  try {
    const response = await api.get<HttpResponse<T>>(url, { params });
    return handleApiResponse(response);
  } catch (e: any) {
    return handleApiResponse(e.response);
  }
};

const httpGetEncoded = async <T>(
  url: string,
  params: URLSearchParams
): Promise<HttpResponse<T>> => {
  try {
    const response = await api.get<HttpResponse<T>>(url, { params });
    return handleApiResponse(response);
  } catch (e: any) {
    return handleApiResponse(e.response);
  }
};

const httpPost = async <T>(
  url: string,
  data: any
): Promise<HttpResponse<T>> => {
  try {
    const response = await api.post<HttpResponse<T>>(url, data);
    return handleApiResponse(response);
  } catch (e: any) {
    return handleApiResponse(e.response);
  }
};

const httpPut = async <T>(url: string, data: any): Promise<HttpResponse<T>> => {
  try {
    const response = await api.put<HttpResponse<T>>(url, data);
    return handleApiResponse(response);
  } catch (e: any) {
    return handleApiResponse(e.response);
  }
};

const httpDelete = async <T>(url: string): Promise<HttpResponse<T>> => {
  try {
    const response = await api.delete<HttpResponse<T>>(url);
    return handleApiResponse(response);
  } catch (e: any) {
    return handleApiResponse(e.response);
  }
};

export { httpGet, httpPost, httpPut, httpDelete, httpGetEncoded, api };

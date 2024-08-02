import { ValidationResponse } from "./ValidationResponse"

export interface HttpResponse<T> {
    Value: T | null;
    HttpStatusCode: number;
    Message: string | null;
    ErrorMessages: ValidationResponse[] | null;
    Exception: string | null;
    Ok: boolean;
}
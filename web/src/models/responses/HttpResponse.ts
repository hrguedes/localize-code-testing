import { ValidationResponse } from "./ValidationResponse"

export interface HttpResponse<T> {
    value: T | null;
    httpStatusCode: number;
    message: string | null;
    errorMessages: ValidationResponse[] | null;
    exception: string | null;
    ok: boolean;
}
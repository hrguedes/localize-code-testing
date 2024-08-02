export default interface BaseModel<T> {
    Id: T | null; 
    RegistroAtivo: boolean | null; 
    RegistroCriado: string | null; 
    UltimaAtualizacao: string | null; 
    RegistroRemovido: string | null; 
}
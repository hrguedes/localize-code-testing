namespace Hrguedes.Localize.Infra.Shared.Models;

public class BaseModel<T>
{
    public T Id { get; set; }
    public bool RegistroAtivo { get; set; }
    public string? RegistroCriado { get; set; }
    public string? UltimaAtualizacao { get; set; }
    public string? RegistroRemovido { get; set; }
}
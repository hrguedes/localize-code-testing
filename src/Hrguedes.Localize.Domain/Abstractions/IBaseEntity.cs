namespace Hrguedes.Localize.Domain.Abstractions;

public interface IBaseEntity<T>
{
    public T Id { get; set; }
    public bool RegistroAtivo { get; set; }
    public DateTime RegistroCriado { get; set; }
    public DateTime? UltimaAtualizacao { get; set; }
    public DateTime? RegistroRemovido { get; set; }
}
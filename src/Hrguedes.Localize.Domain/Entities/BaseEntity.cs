using Hrguedes.Localize.Domain.Abstractions;

namespace Hrguedes.Localize.Domain.Entities;

public class BaseEntity<T> : IBaseEntity<T>
{
    public T Id { get; set; }
    public bool RegistroAtivo { get; set; }
    public DateTime RegistroCriado { get; set; }
    public DateTime? UltimaAtualizacao { get; set; }
    public DateTime? RegistroRemovido { get; set; }
}
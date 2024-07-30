namespace Hrguedes.Localize.Infra.Shared.Options;

/// <summary>
/// Configurações padrão para utilizar em paginações de dados
/// </summary>
public class PaginationOption
{
    /// <summary>
    /// Utilizar essa propiedade para delimitar o máximo de registro de retorno em busca na base de dados
    /// </summary>
    public int MaximoRegistroPorPagina { get; set; }
}

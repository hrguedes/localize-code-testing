namespace Hrguedes.Localize.Infra.Shared.Models;

/// <summary>
/// Classe base para utilizar nas classes e modelos de requisição
/// </summary>
public class PaginationRequest
{
    /// <summary>
    /// Numero da página (Default  = 1)
    /// </summary>
    public int Pagina { get; set; } = 1;

    /// <summary>
    /// Total por página (Default = 100)
    /// </summary>
    public int TotalPorPagina { get; set; } = 100;
}
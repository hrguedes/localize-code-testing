namespace Hrguedes.Localize.Infra.Shared.Models;


/// <summary>
/// Classe com padrão de retorno paginação
/// </summary>
/// <typeparam name="T">Tipo do modelo de retorno</typeparam>
public sealed class PaginationResponse<T> where T : class
{
    /// <summary>
    /// Total de items
    /// </summary>
    public long ? Total { get; set; }
    /// <summary>
    /// Lista com o retornos
    /// </summary>
    public List<T>? Rows { get; set; }
    /// <summary>
    /// Página atual
    /// </summary>
    public int Pagina { get; set; } = 1;
    /// <summary>
    /// Total por página
    /// </summary>
    public int TotalPorPagina { get; set; } = 100;

    public long TotalPaginas { get; set; } = 0;

    /// <summary>
    /// Retorno padrão para criar a instancia
    /// </summary>
    /// <param name="request"></param>
    /// <param name="rows"></param>
    /// <returns></returns>
    public static PaginationResponse<T> Paginate(PaginationRequest request, List<T> rows = null, long total = 0)
    {
        return new PaginationResponse<T>
        {
            Rows = rows,
            Total = total,
            Pagina = request.Pagina,
            TotalPorPagina = request.TotalPorPagina,
            TotalPaginas = ((total -1) / request.TotalPorPagina) + 1
        };
    }

}
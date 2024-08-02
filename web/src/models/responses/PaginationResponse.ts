export default interface PaginationResponse<T> {
  Total: number;
  Rows: T[] | null;
  Pagina: number;
  TotalPorPagina: number;
  TotalPaginas: number;
}

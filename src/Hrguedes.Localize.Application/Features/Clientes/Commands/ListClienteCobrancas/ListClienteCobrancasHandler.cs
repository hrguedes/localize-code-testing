using System.Text;
using Dapper;
using Hrguedes.Localize.Application.Features.Cobrancas.Commands.GetByClienteId;
using Hrguedes.Localize.Domain.Entities;
using Hrguedes.Localize.Infra.Abstractions.Repositories;
using Hrguedes.Localize.Infra.Shared.Http;
using Hrguedes.Localize.Infra.Shared.Models;
using Hrguedes.Localize.Infra.Shared.Options;
using Hrguedes.Localize.Infra.Shared.Resources;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Hrguedes.Localize.Application.Features.Clientes.Commands.ListClienteCobrancas;

public sealed class ListClienteCobrancasHandler : IRequestHandler<ListClienteCobrancasRequest, HttpResult<PaginationResponse<ListClienteCobrancasResponse>>>
{
    private readonly ILogger<ListClienteCobrancasHandler> _logger;
    private readonly IMediator _mediator;
    private readonly PaginationOption _paginationOptions;
    private readonly IUnitOfWork _repo;

    public ListClienteCobrancasHandler(
        ILogger<ListClienteCobrancasHandler> logger,
        IOptions<PaginationOption> paginationOptions,
        IUnitOfWork repo, IMediator mediator)
    {
        ArgumentNullException.ThrowIfNull(paginationOptions, nameof(paginationOptions));
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));
        ArgumentNullException.ThrowIfNull(repo, nameof(repo));
        ArgumentNullException.ThrowIfNull(mediator, nameof(mediator));

        _paginationOptions = paginationOptions.Value;
        _logger = logger;
        _repo = repo;
        _mediator = mediator;
    }

    public async Task<HttpResult<PaginationResponse<ListClienteCobrancasResponse>>> Handle(ListClienteCobrancasRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.UsuarioId == Guid.Empty)
                return HttpResult<PaginationResponse<ListClienteCobrancasResponse>>.BadRequest(nameof(request.UsuarioId), DefaultResources.NaoPodeSerVazio);

            var query = new StringBuilder();
            query.AppendLine(@"SELECT * FROM Operacao.Clientes C WHERE C.UsuarioId = @UsuarioId");

            if (!string.IsNullOrEmpty(request.Pesquisa))
                query.AppendLine($"AND CONCAT(LOWER(C.Nome), LOWER(C.Documento), LOWER(C.Id), LOWER(C.Telefone), LOWER(C.Endereco)) LIKE '%{request.Pesquisa.ToLower()}%'");

            query.AppendLine("ORDER BY C.UltimaAtualizacao DESC");
            query.AppendLine("OFFSET (@pagina - 1) * @qtdePorPagina ROWS FETCH NEXT @qtdePorPagina ROWS ONLY;");

            var rows = await _repo.Read.QueryAsync<Cliente>(query.ToString(), new
            {
                request.UsuarioId,
                pagina = request.Pagina,
                qtdePorPagina = request.TotalPorPagina
            });

            var clientes = new List<ListClienteCobrancasResponse>();
            foreach (var item in rows)
            {
                var totaisCobrancasStatus = await ObterCobrancasPorTipo(item.Id, cancellationToken);
                clientes.Add(new ListClienteCobrancasResponse
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Documento = item.Documento,
                    Telefone = item.Telefone,
                    Endereco = item.Endereco,
                    UsuarioId = item.UsuarioId,
                    Pagos = totaisCobrancasStatus.Item1,
                    EmAtraso = totaisCobrancasStatus.Item2,
                    Aberto = totaisCobrancasStatus.Item3
                });
            }

            var response = PaginationResponse<ListClienteCobrancasResponse>.Paginate(
                request,
                clientes,
                await _repo.Clientes.CountAsync()
            );
            return HttpResult<PaginationResponse<ListClienteCobrancasResponse>>.Success(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return HttpResult<PaginationResponse<ListClienteCobrancasResponse>>.InternalServerError(ex.Message);
        }
    }

    private async Task<Tuple<int, int, int>> ObterCobrancasPorTipo(int clienteId, CancellationToken cancellationToken)
    {
        try
        {
            var cobrancas = await _mediator.Send(new GetCobrancasByClienteIdRequest { ClienteId = clienteId }, cancellationToken);
            if (cobrancas.Value == null) return new Tuple<int, int, int>(0, 0, 0);
            if (cobrancas.Value.Rows is { Count: <= 0 }) return new Tuple<int, int, int>(0, 0, 0);
            var pagos = cobrancas.Value.Rows.Count(c => c.Pago);
            var emAtraso = cobrancas.Value.Rows.Count(c => c.Pago == false && DateTime.Now.CompareTo(c.Vencimento) > 0);
            var emAberto = cobrancas.Value.Rows.Count(c => 
                                                               c.Pago == false &&
                                                               c.Vencimento.CompareTo(DateTime.Now) > 0);

            return new Tuple<int, int, int>(pagos, emAtraso, emAberto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            throw;
        }
    }
}
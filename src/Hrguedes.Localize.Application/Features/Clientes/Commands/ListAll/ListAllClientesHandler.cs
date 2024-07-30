using System.Text;
using Dapper;
using Hrguedes.Localize.Application.Features.Usuarios.Commands.ListAll;
using Hrguedes.Localize.Domain.Entities;
using Hrguedes.Localize.Infra.Abstractions.Repositories;
using Hrguedes.Localize.Infra.Shared.Http;
using Hrguedes.Localize.Infra.Shared.Models;
using Hrguedes.Localize.Infra.Shared.Options;
using Hrguedes.Localize.Infra.Shared.Resources;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Hrguedes.Localize.Application.Features.Clientes.Commands.ListAll;

public sealed class ListAllClientesHandler : IRequestHandler<ListAllClientesRequest, HttpResult<PaginationResponse<ListAllClientesResponse>>>
{
    private readonly ILogger<ListAllClientesHandler> _logger;
    private readonly PaginationOption _paginationOptions;
    private readonly IUnitOfWork _repo;

    public ListAllClientesHandler(
        ILogger<ListAllClientesHandler> logger,
        IOptions<PaginationOption> paginationOptions,
        IUnitOfWork repo)
    {
        ArgumentNullException.ThrowIfNull(paginationOptions, nameof(paginationOptions));
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));
        ArgumentNullException.ThrowIfNull(repo, nameof(repo));

        _paginationOptions = paginationOptions.Value;
        _logger = logger;
        _repo = repo;
    }
    
    public async Task<HttpResult<PaginationResponse<ListAllClientesResponse>>> Handle(ListAllClientesRequest request, CancellationToken cancellationToken)
    {
        try
        {

            if (request.UsuarioId == Guid.Empty)
                return HttpResult<PaginationResponse<ListAllClientesResponse>>.BadRequest(nameof(request.UsuarioId), DefaultResources.NaoPodeSerVazio);

            var query = new StringBuilder();
            query.Append(@"SELECT * FROM Operacao.Clientes C WHERE C.UsuarioId = @UsuarioId ");

            if (!string.IsNullOrEmpty(request.Pesquisa))
                query.AppendLine($"AND CONCAT(LOWER(U.Nome), LOWER(U.Email), LOWER(U.Id)) LIKE '%{request.Pesquisa.ToLower()}%'");
            
            var rows = await _repo.Read.QueryAsync<Cliente>(query.ToString(), new
            {
                request.UsuarioId
            });
            
            var response = PaginationResponse<ListAllClientesResponse>.Paginate(
                request,
                rows.Adapt<List<ListAllClientesResponse>>(),
                await _repo.Usuarios.CountAsync()
            );
            return HttpResult<PaginationResponse<ListAllClientesResponse>>.Success(response);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return HttpResult<PaginationResponse<ListAllClientesResponse>>.InternalServerError(ex.Message);
        }
    }
}
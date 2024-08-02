using Dapper;
using Hrguedes.Localize.Domain.Entities;
using Hrguedes.Localize.Infra.Abstractions.Repositories;
using Hrguedes.Localize.Infra.Shared.Http;
using Hrguedes.Localize.Infra.Shared.Models;
using Hrguedes.Localize.Infra.Shared.Resources;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hrguedes.Localize.Application.Features.Cobrancas.Commands.GetByClienteId;

public sealed class GetCobrancasByClienteIdHandler : IRequestHandler<GetCobrancasByClienteIdRequest, HttpResult<PaginationResponse<GetCobrancasByClienteIdResponse>>>
{
    private readonly ILogger<GetCobrancasByClienteIdHandler> _logger;
    private readonly IUnitOfWork _repo;

    public GetCobrancasByClienteIdHandler(
        ILogger<GetCobrancasByClienteIdHandler> logger,
        IUnitOfWork repo)
    {
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));
        ArgumentNullException.ThrowIfNull(repo, nameof(repo));
        
        _logger = logger;
        _repo = repo;
    }

    public async Task<HttpResult<PaginationResponse<GetCobrancasByClienteIdResponse>>> Handle(GetCobrancasByClienteIdRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.ClienteId == int.MinValue)
                return HttpResult<PaginationResponse<GetCobrancasByClienteIdResponse>>.BadRequest(nameof(request.ClienteId), DefaultResources.NaoPodeSerVazio);

            var rows = await _repo.Read.QueryAsync<Cobranca>(@"
                    SELECT *
                    FROM Operacao.Cobrancas
                    WHERE ClienteId = @ClienteId;", new
            {
                request.ClienteId
            });
            
            var response = PaginationResponse<GetCobrancasByClienteIdResponse>.Paginate(
                request,
                rows.Adapt<List<GetCobrancasByClienteIdResponse>>(),
                await _repo.Cobrancas.CountAsync()
            );
            return HttpResult<PaginationResponse<GetCobrancasByClienteIdResponse>>.Success(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return HttpResult<PaginationResponse<GetCobrancasByClienteIdResponse>>.InternalServerError(ex.Message);
        }
    }
}
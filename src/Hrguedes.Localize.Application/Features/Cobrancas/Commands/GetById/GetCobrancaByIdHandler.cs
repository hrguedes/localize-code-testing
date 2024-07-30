using Dapper;
using Hrguedes.Localize.Domain.Entities;
using Hrguedes.Localize.Infra.Abstractions.Repositories;
using Hrguedes.Localize.Infra.Shared.Http;
using Hrguedes.Localize.Infra.Shared.Resources;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hrguedes.Localize.Application.Features.Cobrancas.Commands.GetById;

public sealed class GetCobrancaByIdHandler : IRequestHandler<GetCobrancaByIdRequest, HttpResult<GetCobrancaByIdResponse>>
{
    private readonly ILogger<GetCobrancaByIdHandler> _logger;
    private readonly IUnitOfWork _repo;

    public GetCobrancaByIdHandler(
        ILogger<GetCobrancaByIdHandler> logger,
        IUnitOfWork repo)
    {
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));
        ArgumentNullException.ThrowIfNull(repo, nameof(repo));

        _logger = logger;
        _repo = repo;
    }

    public async Task<HttpResult<GetCobrancaByIdResponse>> Handle(GetCobrancaByIdRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.CobrancaId == int.MinValue)
                return HttpResult<GetCobrancaByIdResponse>.BadRequest(nameof(request.CobrancaId), DefaultResources.NaoPodeSerVazio);

            var usuario = await _repo.Read.QueryFirstOrDefaultAsync<Cobranca>(@"
                    SELECT *
                    FROM Operacao.Cobrancas
                    WHERE Id = @CobrancaId", new
            {
                request.CobrancaId
            });

            return usuario == null
                ? HttpResult<GetCobrancaByIdResponse>.BadRequest(DefaultResources.NaoEncontrado)
                : HttpResult<GetCobrancaByIdResponse>.Success(usuario.Adapt<GetCobrancaByIdResponse>());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return HttpResult<GetCobrancaByIdResponse>.InternalServerError(ex.Message);
        }
    }
}
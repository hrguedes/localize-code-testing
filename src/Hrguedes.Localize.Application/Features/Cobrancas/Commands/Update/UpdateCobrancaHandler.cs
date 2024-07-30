using Dapper;
using Hrguedes.Localize.Domain.Entities;
using Hrguedes.Localize.Infra.Abstractions.Repositories;
using Hrguedes.Localize.Infra.Shared.Http;
using Hrguedes.Localize.Infra.Shared.Resources;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hrguedes.Localize.Application.Features.Cobrancas.Commands.Update;

public sealed class UpdateCobrancaHandler : IRequestHandler<UpdateCobrancaRequest, HttpResult<UpdateCobrancaResponse>>
{
    private readonly ILogger<UpdateCobrancaHandler> _logger;
    private readonly IUnitOfWork _repo;

    public UpdateCobrancaHandler(
        ILogger<UpdateCobrancaHandler> logger,
        IUnitOfWork repo)
    {
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));
        ArgumentNullException.ThrowIfNull(repo, nameof(repo));

        _logger = logger;
        _repo = repo;
    }

    public async Task<HttpResult<UpdateCobrancaResponse>> Handle(UpdateCobrancaRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Id == 0)
                return HttpResult<UpdateCobrancaResponse>.BadRequest(nameof(request.Id), DefaultResources.NaoPodeSerVazio);

            var entity = await _repo.Read.QueryFirstOrDefaultAsync<Cobranca>(@"SELECT * FROM Operacao.Cobrancas WHERE Id = @Id;", new
            {
                request.Id
            });

            if (entity == null)
                return HttpResult<UpdateCobrancaResponse>.BadRequest(DefaultResources.NaoEncontrado);
            
            _repo.Cobrancas.Update(request.Adapt(entity));
            await _repo.SaveChangesAsync(cancellationToken);
            
            return HttpResult<UpdateCobrancaResponse>.Success(entity.Adapt<UpdateCobrancaResponse>());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return HttpResult<UpdateCobrancaResponse>.InternalServerError(ex.Message);
        }
    }
}
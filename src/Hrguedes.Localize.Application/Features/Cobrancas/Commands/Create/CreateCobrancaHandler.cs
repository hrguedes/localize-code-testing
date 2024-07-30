using Hrguedes.Localize.Domain.Entities;
using Hrguedes.Localize.Infra.Abstractions.Repositories;
using Hrguedes.Localize.Infra.Shared.Http;
using Hrguedes.Localize.Infra.Shared.Resources;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hrguedes.Localize.Application.Features.Cobrancas.Commands.Create;

public sealed class CreateCobrancaHandler : IRequestHandler<CreateCobrancaRequest, HttpResult<CreateCobrancaResponse>>
{
    private readonly ILogger<CreateCobrancaHandler> _logger;
    private readonly IUnitOfWork _repo;

    public CreateCobrancaHandler(
        ILogger<CreateCobrancaHandler> logger,
        IUnitOfWork repo)
    {
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));
        ArgumentNullException.ThrowIfNull(repo, nameof(repo));

        _logger = logger;
        _repo = repo;
    }
    
    public async Task<HttpResult<CreateCobrancaResponse>> Handle(CreateCobrancaRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Invalid)
                return HttpResult<CreateCobrancaResponse>.BadRequest(DefaultResources.ErroValidacao, request.ObterValidacoes);

            var response = await _repo.Cobrancas.CreateAsyncEntry(request.Adapt<Cobranca>());
            await _repo.SaveChangesAsync(cancellationToken);
            return HttpResult<CreateCobrancaResponse>.Success(response.Entity.Adapt<CreateCobrancaResponse>());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return HttpResult<CreateCobrancaResponse>.InternalServerError(ex.Message);
        }
    }
}
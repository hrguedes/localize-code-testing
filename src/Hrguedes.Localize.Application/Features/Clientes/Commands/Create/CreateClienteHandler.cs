using Hrguedes.Localize.Domain.Entities;
using Hrguedes.Localize.Infra.Abstractions.Repositories;
using Hrguedes.Localize.Infra.Shared.Http;
using Hrguedes.Localize.Infra.Shared.Resources;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hrguedes.Localize.Application.Features.Clientes.Commands.Create;

public sealed class CreateClienteHandler : IRequestHandler<CreateClienteRequest, HttpResult<CreateClienteResponse>>
{
    private readonly ILogger<CreateClienteHandler> _logger;
    private readonly IUnitOfWork _repo;

    public CreateClienteHandler(
        ILogger<CreateClienteHandler> logger,
        IUnitOfWork repo)
    {
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));
        ArgumentNullException.ThrowIfNull(repo, nameof(repo));

        _logger = logger;
        _repo = repo;
    }
    
    public async Task<HttpResult<CreateClienteResponse>> Handle(CreateClienteRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.UsuarioId == Guid.Empty)
                return HttpResult<CreateClienteResponse>.BadRequest(nameof(request.UsuarioId), DefaultResources.NaoPodeSerVazio);
            
            if (request.Invalid)
                return HttpResult<CreateClienteResponse>.BadRequest(DefaultResources.ErroValidacao, request.ObterValidacoes);
            
            var response = await _repo.Clientes.CreateAsyncEntry(request.Adapt<Cliente>());
            await _repo.SaveChangesAsync(cancellationToken);
            return HttpResult<CreateClienteResponse>.Success(response.Entity.Adapt<CreateClienteResponse>());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return HttpResult<CreateClienteResponse>.InternalServerError(ex.Message);
        }
    }
}
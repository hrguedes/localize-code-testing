using Dapper;
using Hrguedes.Localize.Domain.Entities;
using Hrguedes.Localize.Infra.Abstractions.Repositories;
using Hrguedes.Localize.Infra.Shared.Http;
using Hrguedes.Localize.Infra.Shared.Resources;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hrguedes.Localize.Application.Features.Clientes.Commands.Update;

public sealed class UpdateClienteHandler : IRequestHandler<UpdateClienteRequest, HttpResult<UpdateClienteResponse>>
{
    private readonly ILogger<UpdateClienteHandler> _logger;
    private readonly IUnitOfWork _repo;

    public UpdateClienteHandler(
        ILogger<UpdateClienteHandler> logger,
        IUnitOfWork repo)
    {
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));
        ArgumentNullException.ThrowIfNull(repo, nameof(repo));

        _logger = logger;
        _repo = repo;
    }

    public async Task<HttpResult<UpdateClienteResponse>> Handle(UpdateClienteRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.UsuarioId == Guid.Empty)
                return HttpResult<UpdateClienteResponse>.BadRequest(nameof(request.UsuarioId), DefaultResources.NaoPodeSerVazio);
            
            if (request.Id == 0)
                return HttpResult<UpdateClienteResponse>.BadRequest(nameof(request.Id), DefaultResources.NaoPodeSerVazio);

            var entity = await _repo.Read.QueryFirstOrDefaultAsync<Cliente>(@"SELECT * FROM Operacao.Clientes WHERE Id = @clienteId;", new
            {
                clienteId = request.Id
            });

            if (entity == null)
                return HttpResult<UpdateClienteResponse>.BadRequest(DefaultResources.NaoEncontrado);

            _repo.Clientes.Update(request.Adapt(entity));
            await _repo.SaveChangesAsync(cancellationToken);
            
            return HttpResult<UpdateClienteResponse>.Success(entity.Adapt<UpdateClienteResponse>());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return HttpResult<UpdateClienteResponse>.InternalServerError(ex.Message);
        }
    }
}
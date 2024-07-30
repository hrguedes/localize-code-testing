using Dapper;
using Hrguedes.Localize.Domain.Entities;
using Hrguedes.Localize.Infra.Abstractions.Repositories;
using Hrguedes.Localize.Infra.Shared.Http;
using Hrguedes.Localize.Infra.Shared.Resources;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hrguedes.Localize.Application.Features.Clientes.Commands.GetById;

public sealed class GetClienteByIdHandler : IRequestHandler<GetClienteByIdRequest, HttpResult<GetClienteByIdResponse>>
{
    private readonly ILogger<GetClienteByIdHandler> _logger;
    private readonly IUnitOfWork _repo;

    public GetClienteByIdHandler(
        ILogger<GetClienteByIdHandler> logger,
        IUnitOfWork repo)
    {
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));
        ArgumentNullException.ThrowIfNull(repo, nameof(repo));

        _logger = logger;
        _repo = repo;
    }

    public async Task<HttpResult<GetClienteByIdResponse>> Handle(GetClienteByIdRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.UsuarioId == Guid.Empty)
                return HttpResult<GetClienteByIdResponse>.BadRequest(nameof(request.UsuarioId), DefaultResources.NaoPodeSerVazio);

            if (request.ClienteId == int.MinValue)
                return HttpResult<GetClienteByIdResponse>.BadRequest(nameof(request.ClienteId), DefaultResources.NaoPodeSerVazio);

            var usuario = await _repo.Read.QueryFirstOrDefaultAsync<Cliente>(@"
                    SELECT *
                    FROM Operacao.Clientes
                    WHERE Id = @ClienteId AND UsuarioId = @UsuarioId;", new
            {
                request.UsuarioId,
                request.ClienteId
            });

            return usuario == null
                ? HttpResult<GetClienteByIdResponse>.BadRequest(DefaultResources.NaoEncontrado)
                : HttpResult<GetClienteByIdResponse>.Success(usuario.Adapt<GetClienteByIdResponse>());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return HttpResult<GetClienteByIdResponse>.InternalServerError(ex.Message);
        }
    }
}
using Dapper;
using Hrguedes.Localize.Domain.Entities;
using Hrguedes.Localize.Infra.Abstractions.Repositories;
using Hrguedes.Localize.Infra.Shared.Http;
using Hrguedes.Localize.Infra.Shared.Resources;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hrguedes.Localize.Application.Features.Usuarios.Commands.GetById;

public sealed class GetUsuarioByIdHandler : IRequestHandler<GetUsuarioByIdRequest, HttpResult<GetUsuarioByIdResponse>>
{
    private readonly ILogger<GetUsuarioByIdHandler> _logger;
    private readonly IUnitOfWork _repo;

    public GetUsuarioByIdHandler(
        ILogger<GetUsuarioByIdHandler> logger,
        IUnitOfWork repo)
    {
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));
        ArgumentNullException.ThrowIfNull(repo, nameof(repo));

        _logger = logger;
        _repo = repo;
    }

    public async Task<HttpResult<GetUsuarioByIdResponse>> Handle(GetUsuarioByIdRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Id == Guid.Empty)
                return HttpResult<GetUsuarioByIdResponse>.BadRequest(nameof(request.Id), DefaultResources.NaoPodeSerVazio);

            var usuario = await _repo.Read.QueryFirstOrDefaultAsync<Usuario>(@"
                    SELECT id,
                           nome,
                           email,
                           senha,
                           registroativo,
                           registrocriado,
                           ultimaatualizacao,
                           registroremovido
                    FROM Sistema.Usuarios
                    WHERE Id = @usuarioId;", new
            {
                usuarioId = request.Id
            });

            return usuario == null
                ? HttpResult<GetUsuarioByIdResponse>.BadRequest(DefaultResources.NaoEncontrado)
                : HttpResult<GetUsuarioByIdResponse>.Success(usuario.Adapt<GetUsuarioByIdResponse>());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return HttpResult<GetUsuarioByIdResponse>.InternalServerError(ex.Message);
        }
    }
}
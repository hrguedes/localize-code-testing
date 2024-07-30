using Dapper;
using Hrguedes.Localize.Domain.Entities;
using Hrguedes.Localize.Infra.Abstractions.Repositories;
using Hrguedes.Localize.Infra.Shared.Http;
using Hrguedes.Localize.Infra.Shared.Resources;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hrguedes.Localize.Application.Features.Usuarios.Commands.GetByEmail;

public sealed class GetUsuarioByEmailHandler : IRequestHandler<GetUsuarioByEmailRequest, HttpResult<GetUsuarioByEmailResponse>>
{
    private readonly ILogger<GetUsuarioByEmailHandler> _logger;
    private readonly IUnitOfWork _repo;

    public GetUsuarioByEmailHandler(
        ILogger<GetUsuarioByEmailHandler> logger,
        IUnitOfWork repo)
    {
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));
        ArgumentNullException.ThrowIfNull(repo, nameof(repo));

        _logger = logger;
        _repo = repo;
    }

    public async Task<HttpResult<GetUsuarioByEmailResponse>> Handle(GetUsuarioByEmailRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrEmpty(request.Email))
                return HttpResult<GetUsuarioByEmailResponse>.BadRequest(nameof(request.Email), DefaultResources.NaoPodeSerVazio);

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
                    WHERE Email = @emailUsuario;", new
            {
                emailUsuario = request.Email
            });

            return usuario == null
                ? HttpResult<GetUsuarioByEmailResponse>.BadRequest(DefaultResources.NaoEncontrado)
                : HttpResult<GetUsuarioByEmailResponse>.Success(usuario.Adapt<GetUsuarioByEmailResponse>());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return HttpResult<GetUsuarioByEmailResponse>.InternalServerError(ex.Message);
        }
    }
}
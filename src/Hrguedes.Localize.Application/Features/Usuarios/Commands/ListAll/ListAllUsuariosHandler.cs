using System.Text;
using Dapper;
using Hrguedes.Localize.Domain.Entities;
using Hrguedes.Localize.Infra.Abstractions.Repositories;
using Hrguedes.Localize.Infra.Shared.Http;
using Hrguedes.Localize.Infra.Shared.Models;
using Hrguedes.Localize.Infra.Shared.Options;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Hrguedes.Localize.Application.Features.Usuarios.Commands.ListAll;

public sealed class ListAllUsuariosHandler : IRequestHandler<ListAllUsuariosRequest, HttpResult<PaginationResponse<ListAllUsuariosResponse>>>
{
    private readonly ILogger<ListAllUsuariosHandler> _logger;
    private readonly PaginationOption _paginationOptions;
    private readonly IUnitOfWork _repo;

    public ListAllUsuariosHandler(
        ILogger<ListAllUsuariosHandler> logger,
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
    
    public async Task<HttpResult<PaginationResponse<ListAllUsuariosResponse>>> Handle(ListAllUsuariosRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var query = new StringBuilder();
            query.Append(@"SELECT * FROM Sistema.Usuarios U ");

            if (!string.IsNullOrEmpty(request.Pesquisa))
                query.AppendLine($"WHERE CONCAT(LOWER(U.Nome), LOWER(U.Email), LOWER(U.Id)) LIKE '%{request.Pesquisa.ToLower()}%'");
            
            var rows = await _repo.Read.QueryAsync<Usuario>(query.ToString());
            
            var response = PaginationResponse<ListAllUsuariosResponse>.Paginate(
                request,
                rows.Adapt<List<ListAllUsuariosResponse>>(),
                await _repo.Usuarios.CountAsync()
            );
            return HttpResult<PaginationResponse<ListAllUsuariosResponse>>.Success(response);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return HttpResult<PaginationResponse<ListAllUsuariosResponse>>.InternalServerError(ex.Message);
        }
    }
}
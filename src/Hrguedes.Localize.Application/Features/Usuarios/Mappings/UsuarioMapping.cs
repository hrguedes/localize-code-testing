using Hrguedes.Localize.Application.Features.Usuarios.Commands.GetByEmail;
using Hrguedes.Localize.Application.Features.Usuarios.Models;
using Hrguedes.Localize.Domain.Entities;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace Hrguedes.Localize.Application.Features.Usuarios.Mappings;

public static class UsuarioMapping
{
    public static void RegisterUsuarioMapping(this IServiceCollection services)
    {
        TypeAdapterConfig<GetUsuarioByEmailRequest, Usuario>.NewConfig()
            .IgnoreNullValues(true);
        
        TypeAdapterConfig<Usuario, UsuarioModel>.NewConfig()
            .Map(dest => dest.RegistroCriado, sourc => sourc.RegistroCriado.ToString("dd/MM/yyyy"))
            .Map(dest => dest.RegistroRemovido, sourc => sourc.RegistroRemovido!.Value.ToString("dd/MM/yyyy"))
            .Map(dest => dest.UltimaAtualizacao, sourc => sourc.UltimaAtualizacao!.Value.ToString("dd/MM/yyyy"))
            .IgnoreNullValues(true);
    }
}
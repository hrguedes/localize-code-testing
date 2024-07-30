using Hrguedes.Localize.Application.Features.Usuarios.Commands.GetByEmail;
using Hrguedes.Localize.Application.Features.Usuarios.Models;
using Hrguedes.Localize.Domain.Entities;
using Mapster;

namespace Hrguedes.Localize.Application.Features.Usuarios.Mappings;

public class UsuarioMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<GetUsuarioByEmailRequest, Usuario>()
            .IgnoreNullValues(true);
        
        config.NewConfig<Usuario, UsuarioModel>()
            .Map(dest => dest.RegistroCriado, sourc => sourc.RegistroCriado.ToString("dd/MM/yyyy"))
            .Map(dest => dest.RegistroRemovido, sourc => sourc.RegistroRemovido!.Value.ToString("dd/MM/yyyy"))
            .Map(dest => dest.UltimaAtualizacao, sourc => sourc.UltimaAtualizacao!.Value.ToString("dd/MM/yyyy"))
            .IgnoreNullValues(true);
    }
}
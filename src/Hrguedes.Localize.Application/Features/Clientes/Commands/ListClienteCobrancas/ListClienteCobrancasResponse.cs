using Hrguedes.Localize.Application.Features.Clientes.Models;

namespace Hrguedes.Localize.Application.Features.Clientes.Commands.ListClienteCobrancas;

public class ListClienteCobrancasResponse : ClienteModel
{
    public int Pagos { get; set; }
    public int EmAtraso { get; set; }
    public int Aberto { get; set; }
}
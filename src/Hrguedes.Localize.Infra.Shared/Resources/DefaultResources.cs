namespace Hrguedes.Localize.Infra.Shared.Resources;


/// <summary>
/// Mensagem já pré-definidas para serem utilizadas em retornos
/// </summary>
public static class DefaultResources
{
    /// <summary>
    /// Usuário não autenticado
    /// </summary>
    public static string UsuarioNaoAutenticado => "Não autenticado.";
    /// <summary>
    /// Recurso não autorizado
    /// </summary>
    public static string RecursoNaoAutorizado => "O recurso solicitado não foi autorizado.";
    /// <summary>
    /// Registro duplicado.
    /// </summary>
    public static string RegistroDuplicado => "Já existe um registro igual na base.";
    /// <summary>
    /// Registro duplicado.
    /// </summary>
    public static string EmailInvalido => "O Email informado e inválido";
    /// <summary>
    /// As Datas de Inicio e Fim são iguais.
    /// </summary>
    public static string DatasIguals => "As Datas de Inicio e Fim são iguais.";
    /// <summary>
    /// O limite maximo de registros por requisição foi atingido.
    /// </summary>
    public static string TamanhoMaximoAtingido => "O limite maximo de registros por requisição foi atingido";
    /// <summary>
    /// Foram encontrados erros e sua requisição.
    /// </summary>
    public static string ErroValidacao => "Foram encontrados erros e sua requisição";
    /// <summary>
    /// Nenhum registro encontrado.
    /// </summary>
    public static string NaoEncontrado => "Nenhum registro encontrado.";
    /// <summary>
    /// O campo não pode ser vazio.
    /// </summary>
    public static string NaoPodeSerVazio => "O campo não pode ser vazio.";
    /// <summary>
    /// Ocorreu um erro no serviço, por favor contacte o administrador.
    /// </summary>
    public static string ErroDeServico => "Ocorreu um erro no serviço, por favor entrar em contato com o administrador.";
    /// <summary>
    /// Ocorreu um erro interno, por favor entrar em contato com o suporte.
    /// </summary>
    public static string ErroInterno => "Ocorreu um erro interno, por favor entrar em contato com o suporte.";
    /// <summary>
    /// O campo so aceita {max} de caracteres.
    /// </summary>
    /// <param name="max">Tamanho máximo de caracteres aceitos</param>
    /// <returns></returns>
    public static string TamanhoMaximoIncorreto(int max) => $"O campo so aceita {max} de caracteres.";
    /// <summary>
    /// O tamanho mínimo é de {min} de caracteres.
    /// </summary>
    /// <param name="min">Tamanho minímo de caracteres aceitos</param>
    /// <returns></returns>
    public static string TamanhoMinimoIncorreto(int min) => $"O tamanho mínimo é de {min} de caracteres.";
}

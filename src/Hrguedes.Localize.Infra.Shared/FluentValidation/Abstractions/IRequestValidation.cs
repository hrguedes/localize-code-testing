using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.Results;

namespace Hrguedes.Localize.Infra.Shared.FluentValidation.Abstractions;


/// <summary>
/// Interface para implementação de validação
/// </summary>
public interface IRequestValidation
{
    /// <summary>
    /// O modelo é válido
    /// </summary>
    [JsonIgnore]
    public bool Valid { get; set; }

    /// <summary>
    /// O modelo é inválido
    /// </summary>
    [JsonIgnore]
    public bool Invalid => !Valid;

    /// <summary>
    /// Retorna as mensagens de validações
    /// </summary>
    [JsonIgnore]
    public ValidationResult ObterValidacoes { get; }

    /// <summary>
    /// Chamada para realizar as validações do modelo (Normalmente utilizado no construtor da classe)
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <param name="model"></param>
    /// <param name="validator"></param>
    /// <returns></returns>
    public bool Validate<TModel>(TModel model, AbstractValidator<TModel> validator);
}

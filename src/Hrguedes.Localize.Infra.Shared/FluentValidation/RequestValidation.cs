using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.Results;
using Hrguedes.Localize.Infra.Shared.FluentValidation.Abstractions;

namespace Hrguedes.Localize.Infra.Shared.FluentValidation;


/// <summary>
/// Classe base para extender para modelos que utilizaram Fluent Validation para validação
/// </summary>
public class RequestValidation : IRequestValidation
{
    /// <summary>
    /// O modelo é válido
    /// </summary>
    [JsonIgnore]
    public bool Valid { get; set; } = true;

    /// <summary>
    /// O modelo é inválido
    /// </summary>
    [JsonIgnore]
    public bool Invalid => !Valid;

    /// <summary>
    /// Retorna as mensagens de validações
    /// </summary>
    [JsonIgnore]
    public ValidationResult ObterValidacoes => ValidationResult;

    /// <summary>
    /// Retorna as mensagens de validações
    /// </summary>
    [JsonIgnore]
    private ValidationResult ValidationResult { get; set; }

    /// <summary>
    /// Chamada para realizar as validações do modelo (Normalmente utilizado no construtor da classe)
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <param name="model"></param>
    /// <param name="validator"></param>
    /// <returns></returns>
    public bool Validate<TModel>(TModel model, AbstractValidator<TModel> validator)
    {
        ValidationResult = validator.Validate(model);
        return Valid = ValidationResult.IsValid;
    }
}
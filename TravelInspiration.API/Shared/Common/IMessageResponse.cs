namespace TravelInspiration.API.Shared.Common;

public interface IMessageResponse
{
    public bool HasValidationError { get; }
    public bool HasLogicError { get; }
    public bool HasError { get; }
    public short StatusCode { get; set; }
    public Dictionary<string, string[]> ValidationErrors { get; set; }
    public string? LogicError { get; set; }
}

public abstract class Response<T> : IMessageResponse
    where T : class
{
    public abstract T ResponseData { get; set; }
    public string? LogicError { get; set; }
    public Dictionary<string, string[]> ValidationErrors { get; set; } = new();
    public short StatusCode { get; set; }
    public bool HasValidationError => ValidationErrors.Any();
    public bool HasLogicError => !string.IsNullOrWhiteSpace(LogicError);
    public bool HasError => HasValidationError || HasLogicError;
}

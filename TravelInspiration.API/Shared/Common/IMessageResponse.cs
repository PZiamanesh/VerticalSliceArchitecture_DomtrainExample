namespace TravelInspiration.API.Shared.Common;

public interface IMessageResponse
{
    public bool HasError { get; }
    public short StatusCode { get; set; }
    public Dictionary<string, string[]> Errors { get; set; }
}

public abstract class Response<T> : IMessageResponse
    where T : class
{
    public abstract T ResponseData { get; set; }
    public bool HasError => Errors.Any();
    public Dictionary<string, string[]> Errors { get; set; } = new();
    public short StatusCode { get; set; }
}

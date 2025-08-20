namespace TravelInspiration.API.Shared.Common;

public interface IMessageResponse<T> 
    where T : class
{
    public T? ResponseData { get; set; }
    public bool HasError { get; set; }
    public short ErrorCode { get; set; }
    public string? ErrorMessage { get; set; }
}

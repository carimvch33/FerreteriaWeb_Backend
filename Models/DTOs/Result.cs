namespace FerreteríaWeb_Backend.Models.DTOs;

public class Result<T>
{
    /// <summary>
    /// The payload or result of the operation
    /// </summary>
    public T? Data { get; set; }
    /// <summary>
    /// Message to show in the view
    /// </summary>
    public string? Message { get; set; }
    /// <summary>
    /// Indicates wheater the operations was completed without exceptions/errors or not
    /// </summary>
    public bool IsAccomplished { get; set; } = true;
    /// <summary>
    /// Thrown exception if ocurred an exception during operation execution
    /// </summary>
    public Exception? InnerException {get; set;}
}
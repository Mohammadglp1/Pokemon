using AutoMapper.Internal.Mappers;

namespace ThePokemonProject;

public class ResultWrapper<T> : IResultWrapper<T> where T : class
{
    private ResultWrapper(T? value, int? statusCode)
    {
        Value = value;
        StatusCode = statusCode;
    }
    public T? Value { get; set; }
    public string? ErrorMessage { get; set; }
    public int? StatusCode { get; set; }
    public bool? HasError { get; private set; }

    public static IResultWrapper<T> Create(T? value, int? statusCode = 200)
        => new ResultWrapper<T>(value, statusCode);

    public IResultWrapper SetError(string errorMessage, int statusCode)
    {
        this.HasError = true;
        this.StatusCode = statusCode;
        this.ErrorMessage = errorMessage;
        return this;
    }

    public static IResultWrapper<T> Empty()
        => new ResultWrapper<T>(null, null);
}
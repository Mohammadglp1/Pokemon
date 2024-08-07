namespace ThePokemonProject;

public interface IErrorSetter
{
   IResultWrapper SetError(string errorMessage, int statusCode);
}

public interface IResultWrapper : IErrorSetter
{
   
}
public interface IResultWrapper<T> : IResultWrapper
{
}

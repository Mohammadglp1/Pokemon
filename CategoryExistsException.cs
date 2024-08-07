namespace ThePokemonProject;

public class CategoryExistsException : Exception
{
    public CategoryExistsException(string message) : base(message)
    {
    }
}

using ThePokemonProject.Interfaces;

namespace ThePokemonProject;

public interface IUnitOfWork:IDisposable

    {
        TRepository GetRepository<TRepository>() where TRepository : class;
        Task<int> SaveChange();
}

using Microsoft.EntityFrameworkCore;
using ThePokemonProject.Data;
using ThePokemonProject.Interfaces;
using ThePokemonProject.Repositories;

namespace ThePokemonProject;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _datacontext;

    private readonly Dictionary<Type, object> _repositories;

    public UnitOfWork(DataContext datacontext)
        {
            _datacontext = datacontext;
            _repositories = new Dictionary<Type, object>
            {
                { typeof(IPokemonRepository), new PokemonRepository(_datacontext) }
            };
    
    }

        public TRepository GetRepository<TRepository>() where TRepository : class
        {
            if (_repositories.TryGetValue(typeof(TRepository), out var repository))
            {
                return repository as TRepository;
            }
            throw new InvalidOperationException($"Repository of type {typeof(TRepository)} is not registered.");
        }

        public void Dispose()
        {
            _datacontext.Dispose();
        }
    

public async Task<int> SaveChange()
{
    return await _datacontext.SaveChangesAsync();
}
}
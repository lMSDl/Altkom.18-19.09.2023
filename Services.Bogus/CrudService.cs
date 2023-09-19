using Bogus;
using Models;
using Services.Bogus.Fakes;
using Services.Interfaces;
using System.Security.Cryptography.X509Certificates;

namespace Services.Bogus
{
    public class CrudService<T> : ICrudService<T> where T : Entity
    {
        private readonly ICollection<T> _entities;

        public CrudService(Faker<T> faker, int count)
        {
            _entities = faker.Generate(count);
        }

        public Task<int> CreateAsync(T entity)
        {
            entity.Id = _entities.Max(x => x.Id) + 1;
            _entities.Add(entity);
            return Task.FromResult(entity.Id);
        }

        public async Task DeleteAsync(int id)
        {
            _entities.Remove(await ReadAsync(id));
        }

        public Task<IEnumerable<T>> ReadAsync()
        {
            return Task.FromResult(_entities.ToList().AsEnumerable());
        }

        public Task<T?> ReadAsync(int id)
        {
            return Task.FromResult(_entities.SingleOrDefault(x => x.Id == id));
        }

        public async Task UpdateAsync(int id, T entity)
        {
            await DeleteAsync(id);
            entity.Id = id;
            _entities.Add(entity);
        }
    }
}
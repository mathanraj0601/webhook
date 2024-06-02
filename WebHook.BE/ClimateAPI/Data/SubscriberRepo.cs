using ClimateAPI.Data.Context;
using ClimateAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace ClimateAPI.Data
{
    public class SubscriberRepo : IRepo<Subscriber, int>
    {
        private readonly ClimateContext _context;
        public SubscriberRepo(ClimateContext context)
        {
            _context = context;
        }
        public async Task<Subscriber> Add(Subscriber entity)
        {
            if(_context.Subscribers == null)
                throw new Exception("Context is empty");
            if(await _context.Subscribers.FirstOrDefaultAsync(x=>x.WebHookType == entity.WebHookType && x.SubscriberUrl == entity.SubscriberUrl) != null)
                throw new Exception("Subscriber already exists");
            _context.Subscribers.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Subscriber> Delete(int id)
        {
            if(_context.Subscribers == null)
                throw new Exception("Context is empty");
            var entity = await _context.Subscribers.FirstOrDefaultAsync(x => x.Id == id);
            if(entity == null)
                throw new Exception("Subscriber not found");
            _context.Subscribers.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;

        }

        public async Task<Subscriber?> Get(int id)
        {
            if(_context.Subscribers == null)
                throw new Exception("Context is empty");
            return await _context.Subscribers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<IQueryable<Subscriber>> GetAll()
        {
            if(_context.Subscribers == null)
                throw new Exception("Context is empty");
            return Task.FromResult(_context.Subscribers.AsQueryable());
        }

        public async Task<Subscriber> Update(Subscriber entity)
        {
            if(_context.Subscribers == null)
                throw new Exception("Context is empty");
            var subscriber = _context.Subscribers.FirstOrDefault(x => x.Id == entity.Id);
            if(subscriber == null)
                throw new Exception("Subscriber not found");
            subscriber.SubscriberUrl = entity.SubscriberUrl;
            await _context.SaveChangesAsync();
            return subscriber;
        }
    }
}

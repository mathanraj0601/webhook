using ClimateAPI.Data.Context;
using ClimateAPI.MessageBus;
using ClimateAPI.Model;
using ClimateAPI.Model.DTO;
using Microsoft.EntityFrameworkCore;

namespace ClimateAPI.Data
{
    public class ClimateRepo : IRepo<Climate, int>
    {

        private readonly ClimateContext _context;
        private readonly IMessageBus _messageBus;
        public ClimateRepo(ClimateContext context,IMessageBus messageBus)
        {
            _messageBus = messageBus;
            _context = context; 
        }
        public async Task<Climate> Add(Climate entity)
        {
            if (_context.Climates == null)
                throw new Exception("Context is empty");
            if (await _context.Climates.FirstOrDefaultAsync(x => x.Area == entity.Area) != null)
                throw new Exception("Climate already exists");
            _context.Climates.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Climate> Delete(int id)
        {
            if(_context.Climates == null)
                throw new Exception("Context is empty");
            var entity = await _context.Climates.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
                throw new Exception("Climate not found");
            _context.Climates.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Climate?> Get(int id)
        {
            if(_context.Climates == null)
                throw new Exception("Context is empty");
            return await _context.Climates.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IQueryable<Climate>> GetAll()
        {
            if(_context.Climates == null)
                throw new Exception("Context is empty");
            return await Task.FromResult(_context.Climates.AsQueryable());
        }


        public async Task<Climate> Update(Climate entity)
        {
            if(_context.Climates == null)
                throw new Exception("Context is empty");
            var climate = await _context.Climates.FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (climate == null)
                throw new Exception("Climate not found");
            var oldTemp = climate.Temp;
            climate.Temp = entity.Temp;

            if(oldTemp != entity.Temp)
            {
                var webHookWorkerDto = new WebHookWorkerDto
                {
                    Area = entity.Area,
                    NewTemp = entity.Temp,
                    OldTemp = oldTemp,
                    Publisher = "indcli",
                    WebHookType = "pricechange"
                };
                _messageBus.publish(webHookWorkerDto);
            }

            await _context.SaveChangesAsync();
            return climate;

        }
    }
}

using AgentAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace AgentAPI.Data
{
    public class WebhookRepo : IRepo<WebHook, int>
    {
        private readonly AgentContext _context;
        public WebhookRepo(AgentContext context)
        {
            _context = context;
        }
        public async Task<WebHook> Add(WebHook entity)
        {
            if(_context.WebHooks == null)
                throw new Exception("WebHooks is empty");
            _context.WebHooks.Add(entity);
             await _context.SaveChangesAsync();
            return entity;

        }

        public async Task<WebHook> Delete(int id)
        {
            if(_context.WebHooks == null)
                throw new Exception("WebHooks is empty");
            var webHook = _context.WebHooks.FirstOrDefault(x => x.Id == id);
            if(webHook == null)
                throw new Exception("WebHook not found");
            _context.WebHooks.Remove(webHook);
            await _context.SaveChangesAsync();
            return webHook ;
        }

        public async Task<WebHook> Get(int id)
        {
            if(_context.WebHooks == null)
                throw new Exception("WebHooks is empty");
            var webHook = await _context.WebHooks.FirstOrDefaultAsync(x => x.Id == id);
            if(webHook == null)
                throw new Exception("WebHook not found");
            return webHook;
        }

        public Task<IQueryable<WebHook>> GetAll()
        {
           if(_context.WebHooks == null)
                throw new Exception("WebHooks is empty");
            return Task.FromResult(_context.WebHooks.AsQueryable());
        }

        public async Task<WebHook> Update(WebHook entity)
        {
           if(_context.WebHooks == null)
                throw new Exception("WebHooks is empty");
            var webHook = _context.WebHooks.FirstOrDefault(x => x.Id == entity.Id);
            if(webHook == null)
                throw new Exception("WebHook not found");
            webHook.Secret = entity.Secret;
            await _context.SaveChangesAsync();
            return webHook;
        }
    }
}

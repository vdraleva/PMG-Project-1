namespace PMG.Services.Message
{
    using Microsoft.EntityFrameworkCore;
    using PMG.Data;
    using PMG.Domain.Home;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class MessageService : IMessageService
    {
        private readonly PMGDbContext context;
        public MessageService(PMGDbContext context)
        {
            this.context = context;
        }

        public async Task Create(Messages message)
        {
            await context.Messages.AddAsync(message);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Messages>> GetAll()
        {
            var messages = await context.Messages.ToListAsync();
            return messages;
        }

        public async Task<Messages> GetById(string id)
        {
            var message = await context.Messages.SingleOrDefaultAsync(x => x.Id == id);
            return message;
        }

        public async Task Remove(Messages message)
        {
            context.Messages.Remove(message);
            await context.SaveChangesAsync();
        }

        public void RemoveById(string id)
        {
            var message = GetById(id).GetAwaiter().GetResult();
            Remove(message).GetAwaiter().GetResult();
        }
    }
}
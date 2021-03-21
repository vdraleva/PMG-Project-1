namespace PMG.Services.Message
{
    using Domain.Home;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMessageService
    {
        Task Create(Messages message);
        Task Remove(Messages message);
        Task<IEnumerable<Messages>> GetAll();
        Task<Messages> GetById(string id);
        void RemoveById(string id);
    }
}
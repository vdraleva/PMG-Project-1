namespace PMG.Services.User
{
    using Domain;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<PMGUser> GetUserWithBookmarkByUsrename(string username);
        Task<IEnumerable<PMGUser>> GetAllUsers();
    }
}
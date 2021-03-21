namespace PMG.Services.Marks
{
    using PMG.Domain;
    using System.Threading.Tasks;

    public interface IMarkService
    {
        Task CreateMarkMathematics(decimal mark, PMGUser user);
        Task CreateMarkPhilosophy(decimal mark, PMGUser user);
        Task CreateMarkEnglish(decimal mark, PMGUser user);
        Task DeleteMark(string MarkId, string BookmarkId);
    }
}
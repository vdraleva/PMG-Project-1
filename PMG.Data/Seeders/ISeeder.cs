namespace PMG.Data.Seeders
{
    using System.Threading.Tasks;

    public interface ISeeder
    {
       Task SeedAsync();
    }
}
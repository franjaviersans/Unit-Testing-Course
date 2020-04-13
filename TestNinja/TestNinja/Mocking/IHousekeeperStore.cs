using System.Linq;

namespace TestNinja.Mocking
{
    public interface IHousekeeperStore
    {
        IQueryable<Housekeeper> GetHousekeepers();
    }
}
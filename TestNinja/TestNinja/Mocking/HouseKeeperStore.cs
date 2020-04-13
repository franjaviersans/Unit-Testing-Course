using System.Linq;

namespace TestNinja.Mocking
{
    public class HousekeeperStore : IHousekeeperStore
    {
        public IQueryable<Housekeeper> GetHousekeepers()
        {
            UnitOfWork UnitOfWork = new UnitOfWork();
            return UnitOfWork.Query<Housekeeper>();
        }
    }
}

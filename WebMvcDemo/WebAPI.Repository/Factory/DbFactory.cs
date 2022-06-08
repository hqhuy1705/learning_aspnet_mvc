using WebAPI.Entity;

namespace WebAPI.Repository
{
    public class DbFactory : IDbFactory
    {
        MyEntities dbContext;

        public MyEntities Init()
        {
            return dbContext ?? (dbContext = new MyEntities());
        }
    }
}

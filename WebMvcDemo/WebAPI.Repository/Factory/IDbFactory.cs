using WebAPI.Entity;

namespace WebAPI.Repository
{
    public interface IDbFactory
    {
        MyEntities Init();
    }
}

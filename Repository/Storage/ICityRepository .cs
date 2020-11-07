namespace Repository.Storage
{
    public interface ICityRepository : IRepository<DataAccess.Models.City>
    {
        List<City> GetAllCityByMap(Guid mapId);
    }

}
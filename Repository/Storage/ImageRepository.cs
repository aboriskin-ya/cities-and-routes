using DataAccess.Models;

namespace Repository.Storage
{
    public class ImageRepository : Repository<Image>, IImageRepository
    {
        public ImageRepository(CityRouteContext context) : base(context)
        {

        }
    }
}

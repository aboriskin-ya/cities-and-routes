using DataAccess;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Storages
{
    public class ImageRepository : Repository<Image>, IImageRepository
    {
        public ImageRepository(CityRouteContext context) : base(context)
        {

        }
    }
}

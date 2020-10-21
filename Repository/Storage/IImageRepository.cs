using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Storages
{
    public interface IImageRepository : IRepository<DataAccess.Models.Image>
    {
    }
}

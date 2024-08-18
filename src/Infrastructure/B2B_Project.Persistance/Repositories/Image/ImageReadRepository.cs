using B2B_Project.Application;
using B2B_Project.Domain.Entities;
using B2B_Project.Persistance.Context;
using B2B_Project.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2B_Project.Persistance
{
    public class ImageReadRepository : ReadRepository<Image>, IImageReadRepository
    {
        public ImageReadRepository(B2B_ProjectDbContext context) : base(context)
        {
        }
    }
}

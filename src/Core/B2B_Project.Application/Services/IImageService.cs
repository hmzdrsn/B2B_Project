using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2B_Project.Application.Services
{
    public interface IImageService
    {
        Task<bool> DeleteById(Guid imageId);
    }
}

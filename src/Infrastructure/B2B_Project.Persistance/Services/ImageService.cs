using B2B_Project.Application;
using B2B_Project.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B2B_Project.Persistance.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageWriteRepository _imageWriteRepository;
        private readonly IImageReadRepository _imageReadRepository;
        public ImageService(IImageWriteRepository imageWriteRepository, IImageReadRepository imageReadRepository)
        {
            _imageWriteRepository = imageWriteRepository;
            _imageReadRepository = imageReadRepository;
        }

        public async Task<bool> DeleteById(Guid imageId)
        {
            var image = await _imageReadRepository.GetByIdAsync(imageId.ToString());
            bool res = _imageWriteRepository.Remove(image);
            if(res)
            {
                await _imageWriteRepository.SaveAsync();
            }
            return res;
        }
    }
}

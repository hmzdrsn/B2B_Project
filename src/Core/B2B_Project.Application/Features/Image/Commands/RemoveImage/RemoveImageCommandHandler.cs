using B2B_Project.Application.Common.Models;
using B2B_Project.Application.Services;
using MediatR;

namespace B2B_Project.Application.Features.Image.Commands.RemoveImage
{
    public class RemoveImageCommandHandler : IRequestHandler<RemoveImageCommandRequest, HandlerResponse<RemoveImageCommandResponse>>
    {
        private readonly IImageService _imageService;

        public RemoveImageCommandHandler(IImageService imageService)
        {
            _imageService = imageService;
        }

        public async Task<HandlerResponse<RemoveImageCommandResponse>> Handle(RemoveImageCommandRequest request, CancellationToken cancellationToken)
        {
            if (await _imageService.DeleteById(request.ImageId))
                return new()
                {
                    Message = "Image Successfully Deleted"
                };
            return new()
            {
                Message = "An error was encountered while deleting the image!"
            };
        }
    }
}

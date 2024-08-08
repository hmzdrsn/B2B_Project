using B2B_Project.Application.Common.Models;
using B2B_Project.Application.Repositories;
using MediatR;

namespace B2B_Project.Application.Features.Company.Commands.CreateCompany
{
    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommandRequest, HandlerResponse<CreateCompanyCommandResponse>>
    {
        private readonly ICompanyWriteRepository _companyWriteRepository;

        public CreateCompanyCommandHandler(ICompanyWriteRepository companyWriteRepository)
        {
            _companyWriteRepository = companyWriteRepository;
        }

        public Task<HandlerResponse<CreateCompanyCommandResponse>> Handle(CreateCompanyCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

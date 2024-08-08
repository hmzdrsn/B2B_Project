using B2B_Project.Application.Common.Models;
using MediatR;

namespace B2B_Project.Application.Features.Company.Commands.CreateCompany
{
    public class CreateCompanyCommandRequest :IRequest<HandlerResponse< CreateCompanyCommandResponse>>
    {
    }
}

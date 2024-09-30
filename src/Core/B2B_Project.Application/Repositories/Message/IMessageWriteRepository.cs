using B2B_Project.Application.Repositories;
using B2B_Project.Domain.Entities;

namespace B2B_Project.Application
{
    public interface IMessageWriteRepository : IWriteRepository<Message>
    {
    }
}

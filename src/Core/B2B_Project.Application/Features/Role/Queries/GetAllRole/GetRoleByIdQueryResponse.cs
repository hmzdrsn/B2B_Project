namespace B2B_Project.Application.Features.Role.Queries.GetAllRole
{
    public class GetRoleByIdQueryResponse
    {
        public string RoleId { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string NormalizedName { get; set; } = string.Empty;
    }
}

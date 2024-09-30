namespace B2B_Project.Application.Features.User.Queries.GetUserShortProperties
{
    public class GetUserShortPropertiesQueryResponse
    {
        public string UserId { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public bool IsOnline { get; set; }
    }
}

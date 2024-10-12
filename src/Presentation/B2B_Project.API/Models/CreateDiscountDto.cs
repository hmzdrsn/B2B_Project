namespace B2B_Project.API.Models
{
    public record CreateDiscountDto(
        string DiscountCode,
        double DiscountRate,
        DateTime? ValidFrom,
        DateTime? ValidUntil,
        int MaxUsagePerUser
        );
}

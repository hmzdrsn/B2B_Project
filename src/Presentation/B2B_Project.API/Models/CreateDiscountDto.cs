namespace B2B_Project.API.Models
{
    public record CreateDiscountDto(
        string DiscountCode,
        double DiscountAmount,
        bool IsPercentage,
        DateTime? ValidFrom,
        DateTime? ValidUntil,
        int MaxUsagePerUser
        );
}

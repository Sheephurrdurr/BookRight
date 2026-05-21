
using System.Runtime.CompilerServices;

namespace BookRight.Domain.Enums
{
    // Class is an extension class of DiscountType enum.
    // The parameter "this DiscountType discountType" allows us to call the method "ToDisplayName"
    // directly on any instance of DiscountType.

    // Class is used in DiscountResult to get the display name of the discount type, which is used in the UI to show the user what kind of discount they have received.
    public static class DiscountTypeExtension
    {
        // 
        public static string ToDisplayName(this DiscountType discountType, decimal percentage)
        {
            // Switch statement returns a string based on the value of the discountType parameter.
            // The percentage parameter is used to include the percentage in the display name for Loyalty and Campaign discounts.
            return discountType switch
            {
                DiscountType.Loyalty => $"Loyalitetsrabat {percentage}%",
                DiscountType.Birthday => $"Fødselsdagsrabat",
                DiscountType.Campaign => $"{percentage}% rabat",
                _ => throw new ArgumentOutOfRangeException(nameof(discountType))
            };
        }
    }
}

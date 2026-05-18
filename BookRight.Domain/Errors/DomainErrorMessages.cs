namespace BookRight.Domain.Errors;

public static class DomainErrorMessages //Static because we don't want to create an object
{
    public const string NameCannotBeEmpty =
        "Navn skal være udfyldt";

    public const string AddOnPercentageOutOfRange =
        "Tillæg skal være mellem 0-100%";
}
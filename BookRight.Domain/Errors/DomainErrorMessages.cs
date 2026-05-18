namespace BookRight.Domain.Errors;

public static class DomainErrorMessages //Static because we don't want to create an object
{
    public const string NameCannotBeEmpty = //COnst = Known value at run-time and never changes. 
        "Navn skal være udfyldt";

    public const string AddOnPercentageOutOfRange =
        "Tillæg skal være mellem 0-100%";
    public const string InvalidId =
    "Invalid id.";

    public const string ValueCannotBeNull =
        "Value cannot be null.";
}
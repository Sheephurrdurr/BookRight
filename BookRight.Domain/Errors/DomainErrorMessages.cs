namespace BookRight.Domain.Errors;

public static class DomainErrorMessages //Static because we don't want to create an object
{
    public const string NameCannotBeEmpty = //Const = Known value at run-time and never changes. 
        "Udfyld navn!";

    public const string AddressCannotBeNull =
        "Udfyld adresse!";

    public const string PhoneNumberCannotBeNull =
        "Udfyld telefonnummer!";

    public const string DateOfBirthIsRequired =
        "Fødselsdato er påkrævet.";

    public const string AddOnPercentageOutOfRange =
        "Tillæg skal være mellem 0-100%";

    public const string EndDateCannotBeBeforeStartDate =
        "Slutdato må ikke være før startdato";

    public const string NumberOfTreatmentRoomsMustBeGreaterThanZero =
        "Antal behandlingsrum skal være større end 0";

    public const string OpeningTimeMustBeBeforeClosingTime =
        "Åbningstid skal være før lukketid";
}
    


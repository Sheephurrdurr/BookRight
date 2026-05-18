namespace BookRight.Domain.Errors;

public static class DomainErrorMessages //Static because we don't want to create an object
{
    public const string NameCannotBeEmpty = //Const = Known value at run-time and never changes. 
        "Navn er påkræ";

    public const string AddressCannotBeNull =
        "Adresse er påkrævet";

    public const string PhoneNumberCannotBeNull =
        "Telefonnummer er påkrævet";

    public const string DateOfBirthIsRequired =
        "Fødselsdato er påkrævet";

    public const string SpecializationIsRequired =
        "Specialisering er påkrævet";

    public const string AddOnPercentageOutOfRange =
        "Tillæg skal være mellem 0-100%";

    public const string EndDateCannotBeBeforeStartDate =
        "Slutdato må ikke være før startdato.";

    public const string DateCannotBeBeforeToday =
        "Dato må ikke være i fortiden";

    public const string NumberOfTreatmentRoomsMustBeGreaterThanZero =
        "Antal behandlingsrum skal være større end 0";

    public const string OpeningTimeMustBeBeforeClosingTime =
        "Åbningstid skal være før lukketid";

    public const string TherapistIsNotAvailable =
        "Behandleren er ikke tilgængelig på det valgte tidspunkt";

    public const string TherapistAlreadyHasBooking =
        "Behandleren er allerede booket på det valgte tidspunkt";

    public const string PriceMustBeGreaterThanZero =
        "Prisen skal være højere end 0 kr.";

    public const string DurationMustBeGreaterThanZero =
        "Varighed skal være mere end 0 min.";

    public const string MaxParticipantsMustBeGreaterThanZero =
        "Maks. antal deltagere skal være større end 0";

}
    


using BookRight.Domain.ValueObjects;

namespace BookRight.Domain.Errors;

public static class DomainErrorMessages //Static because we don't want to create an object
{
    public const string NameCannotBeEmpty = //Const = Known value at run-time and never changes. 
        "Navn er påkrævet";

    public const string AddressCannotBeNull =
        "Adresse er påkrævet";

    public const string PhoneNumberCannotBeNull =
        "Telefonnummer er påkrævet";

    public const string DateOfBirthIsRequired =
        "Fødselsdato er påkrævet";

    public const string SpecializationIsRequired =
        "Specialisering er påkrævet";

    public const string StreetCannotBeEmpty =
        "Gadenavn er påkrævet";

    public const string CityCannotBeEmpty =
        "By er påkrævet";

    public const string PostalCodeCannotBeEmpty =
        "Postnummer er påkrævet";

    public const string FirstNameIsRequired =
        "Fornavn er påkrævet";

    public const string LastNameIsRequired =
        "Efternavn er påkrævet";

    public const string InvalidEmailAddress =
        "E-mailadressen er ugyldig";

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

    public const string CUstomerAlreadyHasBooking =
        "Kunden er allerede booket på det valgte tidspunkt";

    public const string PriceMustBeGreaterThanZero =
        "Prisen skal være højere end 0 kr.";

    public const string DurationMustBeGreaterThanZero =
        "Varighed skal være mere end 0 min.";

    public const string MaxParticipantsMustBeGreaterThanZero =
        "Maks. antal deltagere skal være større end 0";

    public const string InsufficientAmount =
        "Beløbet er ikke tilstrækkeligt";

    public const string EndTimeMustBeLaterThanStartTime =
        "Sluttidspunkt skal være senere end starttidspunkt";

    public static string CustomerNotFound(Guid customerId) //Metode til CustomException med errormessage med customerid parameter)
        => $"Kunde med ID '{customerId}' findes ikke";

    public static string ClinicNotFound(Guid clinicId)
    => $"Klinik med ID '{clinicId}' findes ikke";

    public static string TreatmentTypeNotFound(Guid treatmentTypeId)
   => $"Behandlingstype med ID '{treatmentTypeId}' findes ikke";


    public static string TherapistNotFound(Guid therapistId)
    => $"Klinik med ID '{therapistId}' findes ikke";

    public static string EmailAlreadyExists(string email)
        => $"'{email}' er allerede i brug";

    public static string BookingNotFound(Guid bookingId)
    => $"Booking med ID '{bookingId}' findes ikke";

    public const string UnknownTreatmentType = "Ukendt behandlingstype";

    public const string TreatmentTypeIdsMustNotBeEmpty =
    "En kampagne skal gælde for mindst én behandlingstype";

    public static string TreatmentTypeCannotBeCombinedWith(string treatmentTypeName)
        => $"Behandlingstype {treatmentTypeName} kan ikke blive kombineret med andre.";

    public static string BookingOutsideOpeningHours(Guid clinicId, TimeSlot timeSlot)
       => $"Klinik: '{clinicId}' er ikke åben på det valgte tidspunkt: {timeSlot.StartTime} - {timeSlot.EndTime}.";

}



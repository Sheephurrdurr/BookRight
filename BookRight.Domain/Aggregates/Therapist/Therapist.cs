using BookRight.Domain.Errors;
using BookRight.Domain.ValueObjects;

namespace BookRight.Domain.Aggregates.TherapistAggregate //Rename because of nameconflict with class and namespace
{
    public class Therapist
    {
        public Guid Id { get; private set; }
        public FullName Name { get; private set; } = null!;//Not nullable. It's a promise to the constructor, that property is set later. Fixes warning. 
        public Email Email { get; private set; } = null!;
        public string Specialization { get; private set; } = null!;
        public Authorization Authorization { get; private set; } = null!;
        public Guid ClinicId { get; private set; } //FK to Clinic. 1 therapist belongs to 1 Clinic

        private readonly List<TherapistTreatmentType> _qualifications = new();
        public IReadOnlyCollection<TherapistTreatmentType> Qualifications 
            => _qualifications.AsReadOnly();

        private Therapist() 
        {
        }

        public Therapist(FullName name, Email email, string specialization, Authorization authorization, Guid clinicId)
        {
            if (clinicId == Guid.Empty)
                throw new ArgumentException(nameof(clinicId));

            if (string.IsNullOrWhiteSpace(specialization))
                throw new ArgumentException(
                    DomainErrorMessages.SpecializationIsRequired,
                    nameof(specialization));
            
            Id = Guid.NewGuid();
            ClinicId = clinicId;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Specialization = specialization;
            Authorization = authorization ?? throw new ArgumentNullException(nameof(authorization));
        }

        // Tilføj en kvalifikation for en behandlingstype
        public void AddQualification(Guid treatmentTypeId, decimal basePrice)
        {
            var qualification = new TherapistTreatmentType(Id, treatmentTypeId, basePrice);
            _qualifications.Add(qualification);
        }

        // Fjern en kvalifikation for en behandlingstype
        public void RemoveQualification(Guid treatmentTypeId)
        {
            var qualification = _qualifications
                .FirstOrDefault(q => q.TreatmentTypeId == treatmentTypeId);

            if (qualification != null)
            {
                _qualifications.Remove(qualification);
            }
        }   
    }
}

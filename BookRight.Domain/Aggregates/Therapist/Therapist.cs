using BookRight.Domain.ValueObjects;

namespace BookRight.Domain.Aggregates.Therapist
{
    public class Therapist
    {
        public Guid Id { get; private set; }
        public FullName Name { get; private set; } = null!;//Not nullable. It's a promise to the constructor, that property is set later. Fixes warning. 
        public Email Email { get; private set; } = null!;
        public string Specialization { get; private set; } = null!;

        private readonly List<TherapistTreatmentType> _qualifications = new();
        public IReadOnlyCollection<TherapistTreatmentType> Qualifications 
            => _qualifications.AsReadOnly();

        private Therapist() 
        {
        }

        public Therapist(FullName name, Email email, string specialization)
        {
            if (string.IsNullOrWhiteSpace(specialization))
                throw new ArgumentException("Specialization is required.", nameof(specialization));

            Id = Guid.NewGuid();
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Specialization = specialization;
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

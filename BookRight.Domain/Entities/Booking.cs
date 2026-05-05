using BookRight.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Text;

namespace BookRight.Domain.Entities
{
    public class Booking
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        public Guid ServiceId { get; set; }

        [ForeignKey("ServiceId")]
        public Service Service { get; set; }
        public DateTime Date { get; private set; } // Representerer dagen (fx 2023-12-01)
                                                   //public TimeSpan Time { get; private set; }  Representerer Klokkeslættet (fx 14:´30:00)
        public decimal Price { get; private set; }
        public BookingStatus Status { get; private set; }
        // Foreign key til doctor
        public Guid DoctorId { get; set; }

        // Navigation property: En booking tilhører En læge
        public Doctor Doctor { get; set; }

        // Andre relationer (kunder og klinik)
        public Guid CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        // Service

        public DateTime BookingDate { get; set; }

        public Guid ClinicId { get; set; }
        public Clinic Clinic { get; set; }



        public int TreatmentId { get; set; }
        public Treatment Treatment { get; set; } = default!;


        public Guid TreatmentTypeId { get; set; }
        public TreatmentType TreatmentType { get; set; }

        // en property der returnerer true, hvis status er 'Copleted'
        public bool IsCompleted => Status == BookingStatus.Completed;

        //EF core skal bruge en constructor uden parameter ( eller en hvor alle kan være null)
        private Booking() { }
        public Booking(Guid id, DateTime date, decimal price, BookingStatus status)
        {
            Id = id;
            Date = date;
            Price = price;
            Status = status;
        }
    }
}
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BookRight.Domain.Entities
{
    public record Clinic
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }
        public string Phone { get; private set; }
        public int NumTreatmentRooms { get; private set; }

        public Dictionary<DayOfWeek, OpeningHours> OpeningHours { get; private set; }

        //// Constructor: bruges til at oprette en ny Clinic og initialisere dens værdier
        public Clinic(string name, string address, string phone, int numTreatmentRooms)
        {
            
            Id = Guid.NewGuid();
            Name = name;
            Address = address;
            Phone = phone;
            NumTreatmentRooms = numTreatmentRooms;
            OpeningHours = new Dictionary<DayOfWeek, OpeningHours>();//Når en ny Clinic oprettes, får den en tom liste/dictionary til åbningstider.
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasigSystem.Shared
{
    public class PatientRecord
    {

        [Column("PatientId")] 
        public Guid Id { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string MiddleName { get; set; }
        public string MaidenMiddleName { get; set; }
        [Required]
        public DateTime? BirthDate { get; set; }
        [Required]
        public string CivilStatus { get; set; }
        [Required]
        public string Gender { get; set; } = "FEMALE";
        public byte[] photo { get; set; }
        public string P2 { get; set; }

        [StringLength(100)]
        public string HomeAddress { get; set; }
        [Required]
        public string Barangay { get; set; }
        public string Company { get; set; }
        public string CompanyAddress { get; set; }
        public string Category { get; set; } = "NON-FOOD";
        public string PostedBy { get; set; }
        public DateTime? PostedDate { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }
        public string Origin { get; set; }
        public string OrderPackage { get; set; }
        public Decimal OrderAmt { get; set; }
        public DateTime? PaymentDate { get; set; }
        public Decimal PaymentAmt { get; set; }
        public string PaymentRef { get; set; }

    }
}

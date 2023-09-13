using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIAnnuaire.Models
{
    public class Employees
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Department { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? MobilePhone { get; set; }
        public string? JobTitle { get; set; }
        public string? JobDescription { get; set; }

        // Clé étrangère pour Site
        public int? SiteId { get; set; }
        public Sites? Sites { get; set; }

        // Clé étrangère pour Service
        public int? ServiceId { get; set; }
        public Services? Services { get; set; }
    }


}

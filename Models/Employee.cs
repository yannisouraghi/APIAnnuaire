namespace APIAnnuaire.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string MobilePhone { get; set; } = string.Empty;
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public string Service { get; set; }
        public string Site { get; set; }
    }
}

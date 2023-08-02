using APIAnnuaire.Models;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace APIAnnuaire
{
    public static class EmployeeData
    {
        public static List<Employee> LoadDataFromXml(string xmlFilePath)
        {
            XDocument xmlDoc = XDocument.Load(xmlFilePath);
            List<Employee> employees = xmlDoc.Root.Elements("table")
                .Where(table => table.Attribute("name")?.Value == "employees")
                .Elements("row")
                .Select(row => new Employee
                {
                    Id = int.Parse(row.Element("Id").Value),
                    FirstName = row.Element("FirstName").Value,
                    LastName = row.Element("LastName").Value,
                    Department = row.Element("Department").Value,
                    Email = row.Element("Email").Value,
                    PhoneNumber = row.Element("PhoneNumber").Value,
                    MobilePhone = row.Element("MobilePhone").Value,
                    JobTitle = row.Element("JobTitle").Value,
                    JobDescription = row.Element("JobDescription").Value,
                    Service = row.Element("Service").Value,
                    Site = row.Element("Site").Value
                })
                .ToList();

            return employees;
        }
    }
}

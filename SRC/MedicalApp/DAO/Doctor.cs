using System.ComponentModel.DataAnnotations;

namespace MedicalApp.DAO
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Specialty { get; set; }
        public string Location { get; set; }
        // Outras propriedades necessárias
    }
}

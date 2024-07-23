using System.ComponentModel.DataAnnotations;

namespace MedicalApp.DAO
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public DateTime AvailableTime { get; set; }
    }
}

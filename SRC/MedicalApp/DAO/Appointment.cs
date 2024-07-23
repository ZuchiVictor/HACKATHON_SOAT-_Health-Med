using System.ComponentModel.DataAnnotations;

namespace MedicalApp.DAO
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public string PatientId { get; set; }
        public DateTime AppointmentTime { get; set; }
        public bool? IsAccepted { get; set; }
    }
}

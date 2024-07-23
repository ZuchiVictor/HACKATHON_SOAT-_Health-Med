using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalApp.Tests.Model
{
    public class BookingRequest
    {
        public int DoctorId { get; set; }
        public DateTime AppointmentTime { get; set; }
    }
}

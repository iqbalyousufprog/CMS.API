using CMS.API.Model.Dtos.Department;
using CMS.API.Model.Dtos.Doctor;
using CMS.API.Model.Dtos.Patient;

namespace CMS.API.Model.Dtos.Consultation
{
    public class ResponseConsultationDto : ResponseBaseDto
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public int DepartmentId { get; set; }
        public DateTime ConsultationDate { get; set; }
        public DateTime ConsultationTime { get; set; }
        public string Remarks { get; set; }
        public ResponsePatientDto Patient { get; set; }
        public ResponseDoctorDto Doctor { get; set; }
        public ResponseDepartmentDto Department { get; set; }
    }
}

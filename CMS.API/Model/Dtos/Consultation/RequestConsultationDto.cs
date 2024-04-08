namespace CMS.API.Model.Dtos.Consultation
{
    public class RequestConsultationDto
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public int DepartmentId { get; set; }
        public DateTime ConsultationDate { get; set; }
        public DateTime ConsultationTime { get; set; }
        public string Remarks { get; set; }
    }
}

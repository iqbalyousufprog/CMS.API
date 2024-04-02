namespace CMS.API.Model.Domain
{
    public class Consultation : BaseEntity
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime ConsultationDate { get; set; }
        public DateTime ConsultationTime { get; set; }
        public string Remarks { get; set; }

        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }

    }
}

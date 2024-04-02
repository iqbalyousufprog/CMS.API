using CMS.API.Model.Dtos.Patient;

namespace CMS.API.Model.Dtos.Visit
{
    public class ResponseVisitDto : ResponseBaseDto
    {
        public int PatientId { get; set; }
        public DateTime VisitDate { get; set; }
        public DateTime VisitTime { get; set; }
        public ResponsePatientDto Patient { get; set; }
    }
}

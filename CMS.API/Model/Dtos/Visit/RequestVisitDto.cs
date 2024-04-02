namespace CMS.API.Model.Dtos.Visit
{
    public class RequestVisitDto
    {
        public int PatientId { get; set; }
        public DateTime VisitDate { get; set; }
        public DateTime VisitTime { get; set; }
    }
}

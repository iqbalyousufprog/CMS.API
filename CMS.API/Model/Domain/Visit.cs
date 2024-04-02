namespace CMS.API.Model.Domain
{
    public class Visit : BaseEntity
    {
        public int PatientId { get; set; }
        public DateTime VisitDate { get; set; }
        public DateTime VisitTime { get; set; }

        public Patient Patient { get; set; }
    }
}

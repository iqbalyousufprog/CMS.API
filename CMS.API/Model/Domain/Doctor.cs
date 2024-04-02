namespace CMS.API.Model.Domain
{
    public class Doctor : BaseEntity
    {
        public string DoctorName { get; set; }
        public string Specialization { get; set; }

        public ICollection<Consultation> Consultations { get; set; }

    }
}

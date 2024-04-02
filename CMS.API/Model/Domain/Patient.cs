namespace CMS.API.Model.Domain
{
    public class Patient : BaseEntity
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public ICollection<Visit> Visits { get; set; }
        public ICollection<Disease> Diseases { get; set; }
        public ICollection<Consultation> Consultations { get; set; }

    }
}

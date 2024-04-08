namespace CMS.API.Model.Domain
{
    public class Department : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Consultation> Consultations { get; set; }
    }
}

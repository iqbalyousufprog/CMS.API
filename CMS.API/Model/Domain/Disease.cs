namespace CMS.API.Model.Domain
{
    public class Disease : BaseEntity
    {
        public string DiseaseName { get; set; }

        public ICollection<Patient> Patients { get; set; }
    }
}

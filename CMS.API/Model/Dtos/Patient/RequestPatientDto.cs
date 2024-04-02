using CMS.API.Model.Domain;

namespace CMS.API.Model.Dtos.Patient
{
    public class RequestPatientDto
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public int[] Diseases { get; set; }
    }
}

using CMS.API.Model.Domain;
using CMS.API.Model.Dtos.Consultation;
using CMS.API.Model.Dtos.Disease;

namespace CMS.API.Model.Dtos.Patient
{
    public class ResponsePatientDto : ResponseBaseDto
    {  
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public ICollection<ResponseDiseaseDto> Diseases { get; set; }

        public ICollection<ResponseConsultationDto> Consultations { get; set; }
    }
}

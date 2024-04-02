using Azure.Core;
using CMS.API.Model;
using CMS.API.Model.Domain;
using CMS.API.Model.Dtos.Consultation;
using CMS.API.Model.Dtos.Disease;
using CMS.API.Model.Dtos.Patient;
using CMS.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace CMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientRepository patientRepository;
        private readonly IDiseaseRepository diseaseRepository;
        private readonly IDoctorRepository doctorRepository;

        public PatientsController(IPatientRepository patientRepository, IDiseaseRepository diseaseRepository, IDoctorRepository doctorRepository)
        {
            this.patientRepository = patientRepository;
            this.diseaseRepository = diseaseRepository;
            this.doctorRepository = doctorRepository;
        }

        //Post: https://localhost:7017/api/patients
        [HttpPost]        
        public async Task<ResponseObject<ResponsePatientDto>> CreatePatient(RequestPatientDto requestModel)
        {
            ResponseObject<ResponsePatientDto> responseObject = new ResponseObject<ResponsePatientDto>();

            try
            {
                if (requestModel == null)
                    throw new Exception("Request is invalid!");

                var patient = new Patient
                {
                    Name = requestModel.Name,
                    Age = requestModel.Age,
                    Gender = requestModel.Gender,
                    PhoneNumber = requestModel.PhoneNumber,
                    Address = requestModel.Address,
                    Diseases = new List<Disease>()
                };

                foreach (var disease in requestModel.Diseases)
                {
                    var existDisease = await diseaseRepository.GetByIdAsync(disease);

                    if (existDisease != null)
                        patient.Diseases.Add(existDisease);
                }

                patient = await patientRepository.CreateAsync(patient);

                if (patient == null)
                    throw new Exception("Data not found!");

                var responseDto = new ResponsePatientDto
                {
                    Id = patient.Id,
                    Name = patient.Name,
                    Age = patient.Age,
                    Gender = patient.Gender,
                    PhoneNumber = patient.PhoneNumber,
                    Address = patient.Address,
                    //Diseases = patient.Diseases.Select(x => new ResponseDiseaseDto
                    //{
                    //    Id = x.Id,
                    //    DiseaseName = x.DiseaseName
                    //}).ToList()
                };

                responseObject.IsSuccess = true;
                responseObject.Result = responseDto;
                responseObject.Message = $"Data successfuly created against Id: {responseDto.Id}";
            }
            catch (Exception ex)
            {
                responseObject.IsSuccess = false;
                responseObject.Result = null;
                responseObject.Message = ex.Message;
            }

            return responseObject;
        }

        //Post: https://localhost:7017/api/patients/filter
        [HttpPost]
        [Route("filter")]
        public async Task<ResponseObject<List<ResponsePatientDto>>> GetAllPatients(PatientSearchFilter patientSearchFilter)
        {
            ResponseObject<List<ResponsePatientDto>> responseObject = new ResponseObject<List<ResponsePatientDto>>();

            try
            {
                var patients = await patientRepository.GetAllAsync(patientSearchFilter);

                if (patients == null)
                    throw new Exception("Data not found!");

                var responseDtos = new List<ResponsePatientDto>();
                foreach (var patient in patients)
                {
                    responseDtos.Add(new ResponsePatientDto
                    {
                        Id = patient.Id,
                        Name = patient.Name,
                        Age = patient.Age,
                        Gender = patient.Gender,
                        PhoneNumber = patient.PhoneNumber,
                        Address = patient.Address,
                        Diseases = patient.Diseases.Select(x => new ResponseDiseaseDto
                        {
                            Id = x.Id,
                            DiseaseName = x.DiseaseName
                        }).ToList()
                    });
                }

                responseObject.IsSuccess = true;
                responseObject.Result = responseDtos;
                responseObject.Message = "";
            }
            catch (Exception ex)
            {
                responseObject.IsSuccess = false;
                responseObject.Result = null;
                responseObject.Message = ex.Message;
            }

            return responseObject;
        }

        //Get: https://localhost:7017/api/patients
        [HttpGet]
        public async Task<ResponseObject<List<ResponsePatientDto>>> GetAllPatients()
        {
            ResponseObject<List<ResponsePatientDto>> responseObject = new ResponseObject<List<ResponsePatientDto>>();

            try
            {
                var patients = await patientRepository.GetAllAsync();

                if (patients == null)
                    throw new Exception("Data not found!");

                var responseDtos = new List<ResponsePatientDto>();
                foreach (var patient in patients)
                {
                    responseDtos.Add(new ResponsePatientDto
                    {
                        Id = patient.Id,
                        Name = patient.Name,
                        Age = patient.Age,
                        Gender = patient.Gender,
                        PhoneNumber = patient.PhoneNumber,
                        Address = patient.Address,
                        Diseases = patient.Diseases.Select(x => new ResponseDiseaseDto
                        {
                            Id = x.Id,
                            DiseaseName = x.DiseaseName
                        }).ToList()
                    });
                }

                responseObject.IsSuccess = true;
                responseObject.Result = responseDtos;
                responseObject.Message = "";
            }
            catch (Exception ex)
            {
                responseObject.IsSuccess = false;
                responseObject.Result = null;
                responseObject.Message = ex.Message;
            }

            return responseObject;
        }

        //Get: https://localhost:7017/api/patients/{id}
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ResponseObject<ResponsePatientDto>> GetPatientById(int id)
        {
            ResponseObject<ResponsePatientDto> responseObject = new ResponseObject<ResponsePatientDto>();

            try
            {
                var patient = await patientRepository.GetByIdAsync(id);

                if (patient == null)
                    throw new Exception("Data not found!");

                var responseDto = new ResponsePatientDto
                {
                    Id = patient.Id,
                    Name = patient.Name,
                    Age = patient.Age,
                    Gender = patient.Gender,
                    PhoneNumber = patient.PhoneNumber,
                    Address = patient.Address,
                    Diseases = patient.Diseases.Select(x => new ResponseDiseaseDto
                    {
                        Id = x.Id,
                        DiseaseName = x.DiseaseName
                    }).ToList(),
                    Consultations = (await Task.WhenAll(patient.Consultations.Select(async x => 
                    {
                        var responseConsultationDto = new ResponseConsultationDto
                        {
                            Id = x.Id,
                            PatientId = x.PatientId,
                            DoctorId = x.DoctorId,
                            ConsultationDate = x.ConsultationDate,
                            ConsultationTime = x.ConsultationTime,
                            Remarks = x.Remarks
                        };

                        var doctor = await doctorRepository.GetByIdAsync(responseConsultationDto.DoctorId);

                        if (doctor != null)
                        {
                            responseConsultationDto.Doctor = new Model.Dtos.Doctor.ResponseDoctorDto
                            {
                                Id = doctor.Id,
                                DoctorName = doctor.DoctorName,
                                Specialization = doctor.Specialization
                            };
                        }
                        return responseConsultationDto;

                    }))).ToList()
                };

                responseObject.IsSuccess = true;
                responseObject.Result = responseDto;
                responseObject.Message = "";
            }
            catch (Exception ex)
            {
                responseObject.IsSuccess = false;
                responseObject.Result = null;
                responseObject.Message = ex.Message;
            }

            return responseObject;
        }

        //Put: https://localhost:7017/api/patients/{id}
        [HttpPut]
        [Route("{id:int}")]
        public async Task<ResponseObject<ResponsePatientDto>> UpdatePatient([FromRoute] int id, RequestPatientDto requestModel)
        {
            ResponseObject<ResponsePatientDto> responseObject = new ResponseObject<ResponsePatientDto>();

            try
            {
                if (id <= 0 || requestModel == null)
                    throw new Exception("Request is invalid!");

                //convert Dto to Domain Model
                var patient = new Patient
                {
                    Id = id,
                    Name = requestModel.Name,
                    Age = requestModel.Age,
                    Gender = requestModel.Gender,
                    PhoneNumber = requestModel.PhoneNumber,
                    Address = requestModel.Address,
                    Diseases = new List<Disease>()
                };

                foreach (var disease in requestModel.Diseases)
                {
                    var existDisease = await diseaseRepository.GetByIdAsync(disease);

                    if (existDisease != null)
                        patient.Diseases.Add(existDisease);
                }

                patient = await patientRepository.UpdateAsync(patient);

                if (patient == null)
                    throw new Exception("Data not found!");

                //convert Domain Model to Dto
                var responseDto = new ResponsePatientDto
                {
                    Id = patient.Id,
                    Name = patient.Name,
                    Age = patient.Age,
                    Gender = patient.Gender,
                    PhoneNumber = patient.PhoneNumber,
                    Address = patient.Address,
                    Diseases = patient.Diseases.Select(x => new ResponseDiseaseDto
                    {
                        Id = x.Id,
                        DiseaseName = x.DiseaseName
                    }).ToList()
                };

                responseObject.IsSuccess = true;
                responseObject.Result = responseDto;
                responseObject.Message = $"Data successfuly updated against Id: {responseDto.Id}";
            }
            catch (Exception ex)
            {
                responseObject.IsSuccess = false;
                responseObject.Result = null;
                responseObject.Message = ex.Message;
            }

            return responseObject;
        }

        //Put: https://localhost:7017/api/patients/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ResponseObject<ResponsePatientDto>> DeletePatient(int id)
        {
            ResponseObject<ResponsePatientDto> responseObject = new ResponseObject<ResponsePatientDto>();

            try
            {
                if (id <= 0)
                    throw new Exception("Request is invalid!");

                var patient = await patientRepository.DeleteAsync(id);

                if (patient == null)
                    throw new Exception("Data not found!");

                var responseDto = new ResponsePatientDto
                {
                    Id = patient.Id,
                    Name = patient.Name,
                    Age = patient.Age,
                    Gender = patient.Gender,
                    PhoneNumber = patient.PhoneNumber,
                    Address = patient.Address,
                };

                responseObject.IsSuccess = true;
                responseObject.Result = responseDto;
                responseObject.Message = $"Data successfuly deleted against Id: {responseDto.Id}";
            }
            catch (Exception ex)
            {
                responseObject.IsSuccess = false;
                responseObject.Result = null;
                responseObject.Message = ex.Message;
            }

            return responseObject;
        }
    }
}

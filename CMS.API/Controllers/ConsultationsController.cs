using CMS.API.Model.Domain;
using CMS.API.Model.Dtos.Consultation;
using CMS.API.Model.Dtos.Disease;
using CMS.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultationsController : ControllerBase
    {
        private readonly IConsultationRepository consultationRepository;
        private readonly IPatientRepository patientRepository;

        public ConsultationsController(IConsultationRepository consultationRepository, IPatientRepository patientRepository)
        {
            this.consultationRepository = consultationRepository;
            this.patientRepository = patientRepository;
        }

        //Post: https://localhost:7017/api/consultations
        [HttpPost]
        public async Task<ResponseObject<ResponseConsultationDto>> CreateConsultation(RequestConsultationDto requestModel)
        {
            ResponseObject<ResponseConsultationDto> responseObject = new ResponseObject<ResponseConsultationDto>();

            try
            {
                if (requestModel == null)
                    throw new Exception("Request is invalid!");

                var consultation = new Consultation
                {
                    PatientId = requestModel.PatientId,
                    DoctorId = requestModel.DoctorId,
                    DepartmentId = requestModel.DepartmentId,
                    ConsultationDate = requestModel.ConsultationDate,
                    ConsultationTime = requestModel.ConsultationTime,
                    Remarks = requestModel.Remarks
                };

                consultation = await consultationRepository.CreateAsync(consultation);

                if (consultation is null)
                    throw new Exception("Data not found!");

                var responseDto = new ResponseConsultationDto
                {
                    Id = consultation.Id,
                    PatientId = consultation.PatientId,
                    DoctorId = consultation.DoctorId,
                    DepartmentId = consultation.DepartmentId,
                    ConsultationDate = consultation.ConsultationDate,
                    ConsultationTime = consultation.ConsultationTime,
                    Remarks = consultation.Remarks,
                    //Patient = new Model.Dtos.Patient.ResponsePatientDto()
                    //{
                    //    Id = consultation.Patient.Id,
                    //    Name = consultation.Patient.Name,
                    //    Age = consultation.Patient.Age,
                    //    Gender = consultation.Patient.Gender,
                    //    PhoneNumber = consultation.Patient.PhoneNumber,
                    //    Address = consultation.Patient.Address,
                    //},
                    //Doctor = new Model.Dtos.Doctor.ResponseDoctorDto()
                    //{
                    //    Id = consultation.Doctor.Id,
                    //    DoctorName = consultation.Doctor.DoctorName,
                    //    Specialization = consultation.Doctor.Specialization
                    //}
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

        //Get: https://localhost:7017/api/consultations
        [HttpGet]
        public async Task<ResponseObject<List<ResponseConsultationDto>>> GetAllConsultations()
        {
            ResponseObject<List<ResponseConsultationDto>> responseObject = new ResponseObject<List<ResponseConsultationDto>>();

            try
            {
                var consultations = await consultationRepository.GetAllAsync();

                if (consultations == null)
                    throw new Exception("Data not found!");

                var responseDtos = new List<ResponseConsultationDto>();
                foreach (var consultation in consultations)
                {
                    responseDtos.Add(new ResponseConsultationDto
                    {
                        Id = consultation.Id,
                        PatientId = consultation.PatientId,
                        DoctorId = consultation.DoctorId,
                        DepartmentId = consultation.DepartmentId,
                        ConsultationDate = consultation.ConsultationDate,
                        ConsultationTime = consultation.ConsultationTime,
                        Remarks = consultation.Remarks,
                        Patient = new Model.Dtos.Patient.ResponsePatientDto()
                        {
                            Id = consultation.Patient.Id,
                            Name = consultation.Patient.Name,
                            Age = consultation.Patient.Age,
                            Gender = consultation.Patient.Gender,
                            PhoneNumber = consultation.Patient.PhoneNumber,
                            Address = consultation.Patient.Address,
                        },
                        Doctor = new Model.Dtos.Doctor.ResponseDoctorDto()
                        {
                            Id = consultation.Doctor.Id,
                            DoctorName = consultation.Doctor.DoctorName,
                            Specialization = consultation.Doctor.Specialization
                        },
                        Department = new Model.Dtos.Department.ResponseDepartmentDto()
                        {
                            Id = consultation.Doctor.Id,
                            Name = consultation.Department.Name
                        }
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
        //Get: https://localhost:7017/api/consultations/{id}
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ResponseObject<ResponseConsultationDto>> GetConsultationById(int id)
        {
            ResponseObject<ResponseConsultationDto> responseObject = new ResponseObject<ResponseConsultationDto>();

            try
            {
                var consultation = await consultationRepository.GetByIdAsync(id);

                if (consultation == null)
                    throw new Exception("Data not found!");

                var responseDto = new ResponseConsultationDto
                {
                    Id = consultation.Id,
                    PatientId = consultation.PatientId,
                    DoctorId = consultation.DoctorId,
                    DepartmentId = consultation.DepartmentId,
                    ConsultationDate = consultation.ConsultationDate,
                    ConsultationTime = consultation.ConsultationTime,
                    Remarks = consultation.Remarks,
                    Patient = new Model.Dtos.Patient.ResponsePatientDto()
                    {
                        Id = consultation.Patient.Id,
                        Name = consultation.Patient.Name,
                        Age = consultation.Patient.Age,
                        Gender = consultation.Patient.Gender,
                        PhoneNumber = consultation.Patient.PhoneNumber,
                        Address = consultation.Patient.Address,
                    },
                    Doctor = new Model.Dtos.Doctor.ResponseDoctorDto()
                    {
                        Id = consultation.Doctor.Id,
                        DoctorName = consultation.Doctor.DoctorName,
                        Specialization = consultation.Doctor.Specialization
                    },
                    Department = new Model.Dtos.Department.ResponseDepartmentDto()
                    {
                        Id = consultation.Doctor.Id,
                        Name = consultation.Department.Name
                    }

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

        //Put: https://localhost:7017/api/consultations/{id}
        [HttpPut]
        [Route("{id:int}")]
        public async Task<ResponseObject<ResponseConsultationDto>> UpdateConsultation([FromRoute] int id, RequestConsultationDto requestModel)
        {
            ResponseObject<ResponseConsultationDto> responseObject = new ResponseObject<ResponseConsultationDto>();

            try
            {
                if (id <= 0 || requestModel == null)
                    throw new Exception("Request is invalid!");

                //convert Dto to Domain Model
                var consultation = new Consultation
                {
                    Id = id,
                    PatientId = requestModel.PatientId,
                    DoctorId = requestModel.DoctorId,
                    DepartmentId = requestModel.DepartmentId,
                    ConsultationDate = requestModel.ConsultationDate,
                    ConsultationTime = requestModel.ConsultationTime,
                    Remarks = requestModel.Remarks
                };

                consultation = await consultationRepository.UpdateAsync(consultation);

                if (consultation is null)
                    throw new Exception("Data not found!");

                //convert Domain Model to Dto
                var responseDto = new ResponseConsultationDto
                {
                    Id = consultation.Id,
                    PatientId = consultation.PatientId,
                    DoctorId = consultation.DoctorId,
                    DepartmentId = consultation.DepartmentId,
                    ConsultationDate = consultation.ConsultationDate,
                    ConsultationTime = consultation.ConsultationTime,
                    Remarks = consultation.Remarks,
                    //Patient = new Model.Dtos.Patient.ResponsePatientDto()
                    //{
                    //    Id = consultation.Patient.Id,
                    //    Name = consultation.Patient.Name,
                    //    Age = consultation.Patient.Age,
                    //    Gender = consultation.Patient.Gender,
                    //    PhoneNumber = consultation.Patient.PhoneNumber,
                    //    Address = consultation.Patient.Address,
                    //},
                    //Doctor = new Model.Dtos.Doctor.ResponseDoctorDto()
                    //{
                    //    Id = consultation.Doctor.Id,
                    //    DoctorName = consultation.Doctor.DoctorName,
                    //    Specialization = consultation.Doctor.Specialization
                    //}
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

        //Put: https://localhost:7017/api/consultations/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ResponseObject<ResponseConsultationDto>> DeleteConsultation(int id)
        {
            ResponseObject<ResponseConsultationDto> responseObject = new ResponseObject<ResponseConsultationDto>();

            try
            {
                if (id <= 0)
                    throw new Exception("Request is invalid!");

                var consultation = await consultationRepository.DeleteAsync(id);

                if (consultation == null)
                    throw new Exception("Data not found!");

                var responseDto = new ResponseConsultationDto
                {
                    Id = consultation.Id,
                    PatientId = consultation.PatientId,
                    DoctorId = consultation.DoctorId,
                    DepartmentId = consultation.DepartmentId,
                    ConsultationDate = consultation.ConsultationDate,
                    ConsultationTime = consultation.ConsultationTime,
                    Remarks = consultation.Remarks,
                    //Patient = new Model.Dtos.Patient.ResponsePatientDto()
                    //{
                    //    Id = consultation.Patient.Id,
                    //    Name = consultation.Patient.Name,
                    //    Age = consultation.Patient.Age,
                    //    Gender = consultation.Patient.Gender,
                    //    PhoneNumber = consultation.Patient.PhoneNumber,
                    //    Address = consultation.Patient.Address,
                    //},
                    //Doctor = new Model.Dtos.Doctor.ResponseDoctorDto()
                    //{
                    //    Id = consultation.Doctor.Id,
                    //    DoctorName = consultation.Doctor.DoctorName,
                    //    Specialization = consultation.Doctor.Specialization
                    //}
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

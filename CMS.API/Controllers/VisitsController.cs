using Azure;
using CMS.API.Model.Domain;
using CMS.API.Model.Dtos.Visit;
using CMS.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitsController : ControllerBase
    {
        private readonly IVisitRepository visitRepository;
        private readonly IPatientRepository patientRepository;

        public VisitsController(IVisitRepository visitRepository, IPatientRepository patientRepository)
        {
            this.visitRepository = visitRepository;
            this.patientRepository = patientRepository;
        }

        //Post: https://localhost:7017/api/visits
        [HttpPost]
        public async Task<ResponseObject<ResponseVisitDto>> CreateVisit(RequestVisitDto requestModel)
        {
            ResponseObject<ResponseVisitDto> responseObject = new ResponseObject<ResponseVisitDto>();

            try
            {
                if (requestModel == null)
                    throw new Exception("Request is invalid!");

                var visit = new Visit
                {
                    PatientId = requestModel.PatientId,
                    VisitDate = requestModel.VisitDate,
                    VisitTime = requestModel.VisitTime,
                    Patient = new Patient()
                };

                var existingPatient = await patientRepository.GetByIdAsync(visit.PatientId);

                if (existingPatient != null)
                    visit.Patient = existingPatient;

                visit = await visitRepository.CreateAsync(visit);

                if (visit == null)
                    throw new Exception("Data not found!");

                var responseDto = new ResponseVisitDto
                {
                    Id = visit.Id,
                    PatientId = visit.PatientId,
                    VisitDate = visit.VisitDate,
                    VisitTime = visit.VisitTime,
                    Patient = new Model.Dtos.Patient.ResponsePatientDto
                    {
                        Id = visit.Patient.Id,
                        Name = visit.Patient.Name,
                        Age = visit.Patient.Age,
                        Gender = visit.Patient.Gender,
                        PhoneNumber = visit.Patient.PhoneNumber,
                        Address = visit.Patient.Address
                    }
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

        //Get: https://localhost:7017/api/visits
        [HttpGet]
        public async Task<ResponseObject<List<ResponseVisitDto>>> GetAllVisits()
        {
            ResponseObject<List<ResponseVisitDto>> responseObject = new ResponseObject<List<ResponseVisitDto>>();

            try
            {
                var visits = await visitRepository.GetAllAsync();

                if (visits == null)
                    throw new Exception("Data not found!");

                var responseDtos = new List<ResponseVisitDto>();
                foreach (var visit in visits)
                {
                    responseDtos.Add(new ResponseVisitDto
                    {
                        Id = visit.Id,
                        PatientId = visit.PatientId,
                        VisitDate = visit.VisitDate,
                        VisitTime = visit.VisitTime,
                        Patient = new Model.Dtos.Patient.ResponsePatientDto
                        {
                            Id = visit.Patient.Id,
                            Name = visit.Patient.Name,
                            Age = visit.Patient.Age,
                            Gender = visit.Patient.Gender,
                            PhoneNumber = visit.Patient.PhoneNumber,
                            Address = visit.Patient.Address
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
        //Get: https://localhost:7017/api/visits/{id}
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ResponseObject<ResponseVisitDto>> GetVisitById(int id)
        {
            ResponseObject<ResponseVisitDto> responseObject = new ResponseObject<ResponseVisitDto>();

            try
            {
                var visit = await visitRepository.GetByIdAsync(id);

                if (visit == null)
                    throw new Exception("Data not found!");

                var responseDto = new ResponseVisitDto
                {
                    Id = visit.Id,
                    PatientId = visit.PatientId,
                    VisitDate = visit.VisitDate,
                    VisitTime = visit.VisitTime,
                    Patient = new Model.Dtos.Patient.ResponsePatientDto
                    {
                        Id = visit.Patient.Id,
                        Name = visit.Patient.Name,
                        Age = visit.Patient.Age,
                        Gender = visit.Patient.Gender,
                        PhoneNumber = visit.Patient.PhoneNumber,
                        Address = visit.Patient.Address
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

        //Put: https://localhost:7017/api/visits/{id}
        [HttpPut]
        [Route("{id:int}")]
        public async Task<ResponseObject<ResponseVisitDto>> UpdateVisit([FromRoute] int id, RequestVisitDto requestModel)
        {
            ResponseObject<ResponseVisitDto> responseObject = new ResponseObject<ResponseVisitDto>();

            try
            {
                if (id <= 0 || requestModel == null)
                    throw new Exception("Request is invalid!");

                //convert Dto to Domain Model
                var visit = new Visit
                {
                    Id = id,
                    PatientId = requestModel.PatientId,
                    VisitDate = requestModel.VisitDate,
                    VisitTime = requestModel.VisitTime,
                    Patient = new Patient()
                };

                var existingPatient = await patientRepository.GetByIdAsync(visit.PatientId);

                if (existingPatient != null)
                    visit.Patient = existingPatient;

                visit = await visitRepository.UpdateAsync(visit);

                if (visit == null)
                    throw new Exception("Data not found!");

                //convert Domain Model to Dto
                var responseDto = new ResponseVisitDto
                {
                    Id = visit.Id,
                    PatientId = visit.PatientId,
                    VisitDate = visit.VisitDate,
                    VisitTime = visit.VisitTime,
                    Patient = new Model.Dtos.Patient.ResponsePatientDto
                    {
                        Id = visit.Patient.Id,
                        Name = visit.Patient.Name,
                        Age = visit.Patient.Age,
                        Gender = visit.Patient.Gender,
                        PhoneNumber = visit.Patient.PhoneNumber,
                        Address = visit.Patient.Address
                    }
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

        //Put: https://localhost:7017/api/visits/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ResponseObject<ResponseVisitDto>> DeleteVisit(int id)
        {
            ResponseObject<ResponseVisitDto> responseObject = new ResponseObject<ResponseVisitDto>();

            try
            {
                if (id <= 0)
                    throw new Exception("Request is invalid!");

                var visit = await visitRepository.DeleteAsync(id);

                if (visit == null)
                    throw new Exception("Data not found!");

                var responseDto = new ResponseVisitDto
                {
                    Id = visit.Id,
                    PatientId = visit.PatientId,
                    VisitDate = visit.VisitDate,
                    VisitTime = visit.VisitTime,
                    Patient = new Model.Dtos.Patient.ResponsePatientDto
                    {
                        Id = visit.Patient.Id,
                        Name = visit.Patient.Name,
                        Age = visit.Patient.Age,
                        Gender = visit.Patient.Gender,
                        PhoneNumber = visit.Patient.PhoneNumber,
                        Address = visit.Patient.Address
                    }
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

using CMS.API.Model.Domain;
using CMS.API.Model.Dtos.Doctor;
using CMS.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorRepository doctorRepository;

        public DoctorsController(IDoctorRepository doctorRepository)
        {
            this.doctorRepository = doctorRepository;
        }

        //Post: https://localhost:7017/api/doctors
        [HttpPost]
        public async Task<ResponseObject<ResponseDoctorDto>> CreateDoctor(RequestDoctorDto requestModel)
        {
            ResponseObject<ResponseDoctorDto> responseObject = new ResponseObject<ResponseDoctorDto>();

            try
            {
                if (requestModel == null)
                    throw new Exception("Request is invalid!");

                var doctor = new Doctor
                {
                    DoctorName = requestModel.DoctorName,
                    Specialization = requestModel.Specialization
                };

                doctor = await doctorRepository.CreateAsync(doctor);

                if (doctor == null)
                    throw new Exception("Data not found!");

                var responseDto = new ResponseDoctorDto
                {
                    Id = doctor.Id,
                    DoctorName = doctor.DoctorName,
                    Specialization = doctor.Specialization
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

        //Get: https://localhost:7017/api/doctors
        [HttpGet]
        public async Task<ResponseObject<List<ResponseDoctorDto>>> GetAllDoctors()
        {
            ResponseObject<List<ResponseDoctorDto>> responseObject = new ResponseObject<List<ResponseDoctorDto>>();

            try
            {
                var doctors = await doctorRepository.GetAllAsync();

                if (doctors == null)
                    throw new Exception("Data not found!");

                var responseDtos = new List<ResponseDoctorDto>();
                foreach (var doctor in doctors)
                {
                    responseDtos.Add(new ResponseDoctorDto
                    {
                        Id = doctor.Id,
                        DoctorName = doctor.DoctorName,
                        Specialization = doctor.Specialization
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
        //Get: https://localhost:7017/api/doctors/{id}
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ResponseObject<ResponseDoctorDto>> GetDoctorById(int id)
        {
            ResponseObject<ResponseDoctorDto> responseObject = new ResponseObject<ResponseDoctorDto>();

            try
            {
                var doctor = await doctorRepository.GetByIdAsync(id);

                if (doctor == null)
                    throw new Exception("Data not found!");

                var responseDto = new ResponseDoctorDto
                {
                    Id = doctor.Id,
                    DoctorName = doctor.DoctorName,
                    Specialization = doctor.Specialization
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

        //Put: https://localhost:7017/api/doctors/{id}
        [HttpPut]
        [Route("{id:int}")]
        public async Task<ResponseObject<ResponseDoctorDto>> UpdateDoctor([FromRoute] int id, RequestDoctorDto requestModel)
        {
            ResponseObject<ResponseDoctorDto> responseObject = new ResponseObject<ResponseDoctorDto>();

            try
            {
                if (id <= 0 || requestModel == null)
                    throw new Exception("Request is invalid!");

                //convert Dto to Domain Model
                var doctor = new Doctor
                {
                    Id = id,
                    DoctorName = requestModel.DoctorName,
                    Specialization = requestModel.Specialization
                };

                doctor = await doctorRepository.UpdateAsync(doctor);

                if (doctor == null)
                    throw new Exception("Data not found!");

                //convert Domain Model to Dto
                var responseDto = new ResponseDoctorDto
                {
                    Id = doctor.Id,
                    DoctorName = doctor.DoctorName,
                    Specialization = doctor.Specialization
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

        //Put: https://localhost:7017/api/doctors/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ResponseObject<ResponseDoctorDto>> DeleteDoctor(int id)
        {
            ResponseObject<ResponseDoctorDto> responseObject = new ResponseObject<ResponseDoctorDto>();

            try
            {
                if (id <= 0)
                    throw new Exception("Request is invalid!");

                var doctor = await doctorRepository.DeleteAsync(id);

                if (doctor == null)
                    throw new Exception("Data not found!");

                var responseDto = new ResponseDoctorDto
                {
                    Id = doctor.Id,
                    DoctorName = doctor.DoctorName,
                    Specialization = doctor.Specialization
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

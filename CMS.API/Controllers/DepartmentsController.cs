using CMS.API.Model.Domain;
using CMS.API.Model.Dtos.Department;
using CMS.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentRepository departmentRepository;

        public DepartmentsController(IDepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }

        //Post: https://localhost:7017/api/departments
        [HttpPost]
        public async Task<ResponseObject<ResponseDepartmentDto>> CreateDepartment(RequestDepartmentDto requestModel)
        {
            ResponseObject<ResponseDepartmentDto> responseObject = new ResponseObject<ResponseDepartmentDto>();
            try
            {
                if (requestModel == null)
                    throw new Exception("Request is invalid!");

                var department = new Department
                {
                    Name = requestModel.Name
                };

                department = await departmentRepository.CreateAsync(department);

                if (department == null)
                    throw new Exception("Data not found!");

                var responseDto = new ResponseDepartmentDto
                {
                    Id = department.Id,
                    Name = department.Name,
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

        //Get: https://localhost:7017/api/departments
        [HttpGet]
        public async Task<ResponseObject<List<ResponseDepartmentDto>>> GetAllDepartments()
        {
            ResponseObject<List<ResponseDepartmentDto>> responseObject = new ResponseObject<List<ResponseDepartmentDto>>();
            try
            {
                var departments = await departmentRepository.GetAllAsync();

                if (departments == null)
                    throw new Exception("Data not found!");

                var responseDtos = new List<ResponseDepartmentDto>();
                foreach (var department in departments)
                {
                    responseDtos.Add(new ResponseDepartmentDto
                    {
                        Id = department.Id,
                        Name = department.Name,
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
        //Get: https://localhost:7017/api/departments/{id}
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ResponseObject<ResponseDepartmentDto>> GetDepartmentById(int id)
        {
            ResponseObject<ResponseDepartmentDto> responseObject = new ResponseObject<ResponseDepartmentDto>();
            try
            {
                var department = await departmentRepository.GetByIdAsync(id);

                if (department == null)
                    throw new Exception("Data not found!");

                var responseDto = new ResponseDepartmentDto
                {
                    Id = department.Id,
                    Name = department.Name,
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

        //Put: https://localhost:7017/api/departments/{id}
        [HttpPut]
        [Route("{id:int}")]
        public async Task<ResponseObject<ResponseDepartmentDto>> UpdateDepartment([FromRoute] int id, RequestDepartmentDto requestModel)
        {
            ResponseObject<ResponseDepartmentDto> responseObject = new ResponseObject<ResponseDepartmentDto>();

            try
            {
                if (id <= 0 || requestModel == null)
                    throw new Exception("Request is invalid!");

                //convert Dto to Domain Model
                var department = new Department
                {
                    Id = id,
                    Name = requestModel.Name,
                };

                department = await departmentRepository.UpdateAsync(department);

                if (department == null)
                    throw new Exception("Data not found!");

                //convert Domain Model to Dto
                var responseDto = new ResponseDepartmentDto
                {
                    Id = department.Id,
                    Name = department.Name,
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

        //Put: https://localhost:7017/api/departments/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ResponseObject<ResponseDepartmentDto>> DeleteDepartment(int id)
        {
            ResponseObject<ResponseDepartmentDto> responseObject = new ResponseObject<ResponseDepartmentDto>();

            try
            {
                if (id <= 0)
                    throw new Exception("Request is invalid!");

                var department = await departmentRepository.DeleteAsync(id);

                if (department == null)
                    throw new Exception("Data not found!");

                var responseDto = new ResponseDepartmentDto
                {
                    Id = department.Id,
                    Name = department.Name,
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

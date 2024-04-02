using CMS.API.Model.Domain;
using CMS.API.Model.Dtos.Disease;
using CMS.API.Model.Dtos.Disease;
using CMS.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace CMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiseasesController : ControllerBase
    {
        private readonly IDiseaseRepository diseaseRepository;

        public DiseasesController(IDiseaseRepository diseaseRepository)
        {
            this.diseaseRepository = diseaseRepository;
        }

        //Post: https://localhost:7017/api/diseases
        [HttpPost]
        public async Task<ResponseObject<ResponseDiseaseDto>> CreateDisease(RequestDiseaseDto requestModel)
        {
            ResponseObject<ResponseDiseaseDto> responseObject = new ResponseObject<ResponseDiseaseDto>();
            try
            {
                if (requestModel == null)
                    throw new Exception("Request is invalid!");

                var disease = new Disease
                {
                    DiseaseName = requestModel.DiseaseName
                };

                disease = await diseaseRepository.CreateAsync(disease);

                if (disease == null)
                    throw new Exception("Data not found!");

                var responseDto = new ResponseDiseaseDto
                {
                    Id = disease.Id,
                    DiseaseName = disease.DiseaseName,
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

        //Get: https://localhost:7017/api/diseases
        [HttpGet]
        public async Task<ResponseObject<List<ResponseDiseaseDto>>> GetAllDiseases()
        {
            ResponseObject<List<ResponseDiseaseDto>> responseObject = new ResponseObject<List<ResponseDiseaseDto>>();
            try
            {
                var diseases = await diseaseRepository.GetAllAsync();

                if (diseases == null)
                    throw new Exception("Data not found!");

                var responseDtos = new List<ResponseDiseaseDto>();
                foreach (var disease in diseases)
                {
                    responseDtos.Add(new ResponseDiseaseDto
                    {
                        Id = disease.Id,
                        DiseaseName = disease.DiseaseName,
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
        //Get: https://localhost:7017/api/diseases/{id}
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ResponseObject<ResponseDiseaseDto>> GetDiseaseById(int id)
        {
            ResponseObject<ResponseDiseaseDto> responseObject = new ResponseObject<ResponseDiseaseDto>();
            try
            {
                var disease = await diseaseRepository.GetByIdAsync(id);

                if (disease == null)
                    throw new Exception("Data not found!");

                var responseDto = new ResponseDiseaseDto
                {
                    Id = disease.Id,
                    DiseaseName = disease.DiseaseName,
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

        //Put: https://localhost:7017/api/diseases/{id}
        [HttpPut]
        [Route("{id:int}")]
        public async Task<ResponseObject<ResponseDiseaseDto>> UpdateDisease([FromRoute] int id, RequestDiseaseDto requestModel)
        {
            ResponseObject<ResponseDiseaseDto> responseObject = new ResponseObject<ResponseDiseaseDto>();

            try
            {
                if (id <= 0 || requestModel == null)
                    throw new Exception("Request is invalid!");

                //convert Dto to Domain Model
                var disease = new Disease
                {
                    Id = id,
                    DiseaseName = requestModel.DiseaseName,
                };

                disease = await diseaseRepository.UpdateAsync(disease);

                if (disease == null)
                    throw new Exception("Data not found!");

                //convert Domain Model to Dto
                var responseDto = new ResponseDiseaseDto
                {
                    Id = disease.Id,
                    DiseaseName = disease.DiseaseName,
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

        //Put: https://localhost:7017/api/diseases/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ResponseObject<ResponseDiseaseDto>> DeleteDisease(int id)
        {
            ResponseObject<ResponseDiseaseDto> responseObject = new ResponseObject<ResponseDiseaseDto>();

            try
            {
                if (id <= 0)
                    throw new Exception("Request is invalid!");

                var disease = await diseaseRepository.DeleteAsync(id);

                if (disease == null)
                    throw new Exception("Data not found!");

                var responseDto = new ResponseDiseaseDto
                {
                    Id = disease.Id,
                    DiseaseName = disease.DiseaseName,
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

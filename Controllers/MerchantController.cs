using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PaymentSystemAPI.Dtos;
using PaymentSystemAPI.IServices;
using PaymentSystemAPI.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PaymentSystemAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class MerchantController : ControllerBase
    {
        private IMerchantService _merchantService;
        private IMapper _mapper;

        public MerchantController(IMerchantService merchantService, IMapper mapper)
        {
            _merchantService = merchantService;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]       
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<GetMerchantDto>>), StatusCodes.Status200OK)]        
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<GetMerchantDto>>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            ApiResponse<IEnumerable<GetMerchantDto>> response = new ApiResponse<IEnumerable<GetMerchantDto>>();

            try
            {
                var merchants = _mapper.Map<IEnumerable<GetMerchantDto>>(await _merchantService.GetAll());

                if (merchants == null)
                {
                    response.Message = "Merchants not available";

                    return NotFound(response);
                }

                response.Result = merchants ?? null;

                response.IsSuccessful = true;

                return Ok(response);
            }
            catch (Exception eX)
            {               
                string msg = eX.Message;

                if (eX.InnerException != null)
                {
                    msg += "; " + eX.InnerException.Message; if (eX.InnerException.InnerException != null) { msg += ";" + eX.InnerException.InnerException.Message; }
                }

                //log error

                response.Message = "An error occured while processing request";
               
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet("GetMerchantByMerchantNumber/{merchantNumber}")]
        [ProducesResponseType(typeof(ApiResponse<GetMerchantDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<GetMerchantDto>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<GetMerchantDto>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMerchantByMerchantNumber(string merchantNumber)
        {
            ApiResponse<GetMerchantDto> response = new ApiResponse<GetMerchantDto>();

            try
            {
                var merchant = _mapper.Map<GetMerchantDto>(await _merchantService.GetMerchantByMerchantNumber(merchantNumber));

                if (merchant == null)
                {
                    response.Message = "Merchant not found";

                    return NotFound(response);
                }

                response.Result = merchant ?? null; //this certainly has a value
                response.Message = "Merchant found";
                response.IsSuccessful = true;

                return Ok(response);
            }
            catch (Exception eX)
            {
                string msg = eX.Message;

                if (eX.InnerException != null)
                {
                    msg += "; " + eX.InnerException.Message; if (eX.InnerException.InnerException != null) { msg += ";" + eX.InnerException.InnerException.Message; }
                }

                //log error

                response.Message = "An error occured while processing request";

                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPost("CreateMerchant")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateMerchant([FromBody] CreateMerchantDto model)
        {
            ApiResponse<bool> response = new ApiResponse<bool>();

            try
            {
                if (!ModelState.IsValid)
                {
                    response.Message = "Invalid request";

                    return BadRequest(response);
                }

                if (model == null)
                {
                    response.Message = "Missing request body";

                    return BadRequest(response);
                }

                //check if merchant number already exists
                var existingMerchant = await _merchantService.GetMerchantByMerchantNumber(model.MerchantNumber);

                if (existingMerchant != null)
                {
                    response.Message = "Merchant no already exists.";

                    return BadRequest(response);
                }

                await _merchantService.CreateMerchant(model);

                response.Message = "Merchant created successfully";
                response.Result = true;
                response.IsSuccessful = true;
                
                return Ok(response);
            }
            catch (Exception eX)
            {
                string msg = eX.Message;

                if (eX.InnerException != null)
                {
                    msg += "; " + eX.InnerException.Message; if (eX.InnerException.InnerException != null) { msg += ";" + eX.InnerException.InnerException.Message; }
                }

                //log error

                response.Message = "An error occured while processing request";

                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
        
        [HttpPost("UpdateMerchant/{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateMerchant(int id, [FromBody] UpdateMerchantDto model)
        {
            ApiResponse<bool> response = new ApiResponse<bool>();

            try
            {
                if (!ModelState.IsValid)
                {
                    response.Message = "Invalid request";

                    return BadRequest(response);
                }

                if (model == null)
                {
                    response.Message = "Missing request body";

                    return BadRequest(response);
                }

                //search for the merchant to update
                var merchantToUpdate = _merchantService.GetMerchantById(id);

                if (merchantToUpdate == null)
                {
                    response.Message = "Merchant not found";

                    return NotFound(response);
                }

                //check if merchant id to be updated already exists
                var merchantWithUpdatedMerchantId = await _merchantService.GetMerchant(id, model.MerchantNumber);

                if (merchantWithUpdatedMerchantId != null)
                {
                    response.Message = "Merchant id already exists";

                    return BadRequest(response);
                }

                await _merchantService.UpdateMerchant(id, model);

                response.Message = "Merchant updated successfully";
                response.Result = true;
                response.IsSuccessful = true;

                return Ok(response);
            }
            catch (Exception eX)
            {
                string msg = eX.Message;

                if (eX.InnerException != null)
                {
                    msg += "; " + eX.InnerException.Message; if (eX.InnerException.InnerException != null) { msg += ";" + eX.InnerException.InnerException.Message; }
                }

                //log error

                response.Message = "An error occured while processing request";

                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpDelete("DeleteMerchant/{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]        
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteMerchant(int id)
        {
            ApiResponse<bool> response = new ApiResponse<bool>();

            try
            {
                //search for the merchant to update
                var merchantToUpdate = await _merchantService.GetMerchantById(id);

                if (merchantToUpdate == null)
                {
                    response.Message = "Merchant not found";
                    return NotFound(response);
                }

                await _merchantService.DeleteMerchant(id);

                response.Message = "Merchant deleted successfully";
                response.Result = true;
                response.IsSuccessful = true;

                return Ok(response);
            }
            catch (Exception eX)
            {
                string msg = eX.Message;

                if (eX.InnerException != null)
                {
                    msg += "; " + eX.InnerException.Message; if (eX.InnerException.InnerException != null) { msg += ";" + eX.InnerException.InnerException.Message; }
                }

                //log error

                response.Message = "An error occured while processing request";

                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}

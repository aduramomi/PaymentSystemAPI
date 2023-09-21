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
    public class CustomerController : ControllerBase
    {
        private ICustomerService _CustomerService;
        private IMapper _mapper;

        public CustomerController(ICustomerService CustomerService, IMapper mapper)
        {
            _CustomerService = CustomerService;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]       
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<GetCustomerDto>>), StatusCodes.Status200OK)]        
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<GetCustomerDto>>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            ApiResponse<IEnumerable<GetCustomerDto>> response = new ApiResponse<IEnumerable<GetCustomerDto>>();

            try
            {
                var Customers = _mapper.Map<IEnumerable<GetCustomerDto>>(await _CustomerService.GetAll());

                if (Customers == null)
                {
                    response.Message = "Customers not available";

                    return NotFound(response);
                }

                response.Result = Customers ?? null;

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

        [HttpGet("GetCustomerByCustomerNumber/{CustomerNumber}")]
        [ProducesResponseType(typeof(ApiResponse<GetCustomerDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<GetCustomerDto>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<GetCustomerDto>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCustomerByCustomerNumber(string CustomerNumber)
        {
            ApiResponse<GetCustomerDto> response = new ApiResponse<GetCustomerDto>();

            try
            {
                var Customer = _mapper.Map<GetCustomerDto>(await _CustomerService.GetCustomerByCustomerNumber(CustomerNumber));

                if (Customer == null)
                {
                    response.Message = "Customer not found";

                    return NotFound(response);
                }

                response.Result = Customer ?? null; //this certainly has a value
                response.Message = "Customer found";
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

        [HttpPost("CreateCustomer")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerDto model)
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

                //check if Customer number already exists
                var existingCustomer = await _CustomerService.GetCustomerByCustomerNumber(model.CustomerNumber);

                if (existingCustomer != null)
                {
                    response.Message = "Customer no already exists.";

                    return BadRequest(response);
                }

                await _CustomerService.CreateCustomer(model);

                response.Message = "Customer created successfully";
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
        
        [HttpPost("UpdateCustomer/{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] UpdateCustomerDto model)
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

                //search for the Customer to update
                var CustomerToUpdate = _CustomerService.GetCustomerById(id);

                if (CustomerToUpdate == null)
                {
                    response.Message = "Customer not found";

                    return NotFound(response);
                }

                //check if Customer number to be updated already exists
                var CustomerWithUpdatedCustomerNo = await _CustomerService.GetCustomer(id, model.CustomerNumber);

                if (CustomerWithUpdatedCustomerNo != null)
                {
                    response.Message = "Customer no already exists";

                    return BadRequest(response);
                }

                await _CustomerService.UpdateCustomer(id, model);

                response.Message = "Customer updated successfully";
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

        [HttpDelete("DeleteCustomer/{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]        
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            ApiResponse<bool> response = new ApiResponse<bool>();

            try
            {
                //search for the Customer to update
                var CustomerToUpdate = await _CustomerService.GetCustomerById(id);

                if (CustomerToUpdate == null)
                {
                    response.Message = "Customer not found";
                    return NotFound(response);
                }

                await _CustomerService.DeleteCustomer(id);

                response.Message = "Customer deleted successfully";
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

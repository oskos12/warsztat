using AutoMapper;
using AutoWrapper.Filters;
using AutoWrapper.Models;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Warsztat.Contracts;
using Warsztat.Data.DataManager;
using Warsztat.Data.Entity;
using Warsztat.DTO.Request;
using Warsztat.DTO.Response;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Warsztat.API.v1
{
    [Route("api/v1/efcore")]
    [ApiController]
    public class SqlServerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISqlServerManager _manager;
        public SqlServerController(IMapper mapper, ISqlServerManager manager)
        {
            _mapper = mapper;
            _manager = manager;
        }

        [HttpGet]
        [AutoWrapIgnore]
        [Route("users")]
        [ProducesResponseType(typeof(Response), Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsResponse), Status404NotFound)]
        public async Task<ApiResponse> GetUsersWithEF()
        {
            var users = await _manager.GetUsers();
            if (!users.Any())
                return new ApiResponse(new ApiProblemDetailsException("User wasn't found.", Status404NotFound), 404);

            var data = _mapper.Map<IEnumerable<Users>>(users);
            return new ApiResponse(data);
        }

        [HttpGet]
        [AutoWrapIgnore]
        [Route("clients")]
        [ProducesResponseType(typeof(Response), Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsResponse), Status404NotFound)]
        public IActionResult GetClients()
        {
            var clients = _manager.GetAllClients();
            if (!clients.Any())
                return NotFound(new ApiProblemDetailsException("Client wasn't found.", Status404NotFound));

            var data = _mapper.Map<IEnumerable<Clients>>(clients);
            return Ok(new Response { count = data.Count(), data = data });
        }

        [HttpGet]
        [AutoWrapIgnore]
        [Route("clientsEF")]
        [ProducesResponseType(typeof(Response), Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsResponse), Status404NotFound)]
        public async Task<IActionResult> GetClientsEF()
        {
            var clients = await _manager.GetAllClientsEF();
            if (!clients.Any())
                return NotFound(new ApiProblemDetailsException("Client wasn't found.", Status404NotFound));

            var data = _mapper.Map<IEnumerable<Clients>>(clients);
            return Ok(new Response { count = data.Count(), data = data });
        }

        [HttpGet]
        [AutoWrapIgnore]
        [Route("clientsDapper")]
        [ProducesResponseType(typeof(Response), Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsResponse), Status404NotFound)]
        public IActionResult GetClientsDapper()
        {
            var clients = _manager.GetAllClientsDapper();
            if (!clients.Any())
                return NotFound(new ApiProblemDetailsException("Client wasn't found.", Status404NotFound));

            var data = _mapper.Map<IEnumerable<Clients>>(clients);
            return Ok(new Response { count = data.Count(), data = data });
        }

        [HttpPost]
        [AutoWrapIgnore]
        [Route("client")]
        [NonAction]
        [ProducesResponseType(typeof(int), Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsResponse), Status400BadRequest)]
        public async Task<IActionResult> AddClient(AddClientRequest request)
        {
            var client = await _manager.AddNewClient(request.Name, request.Surname, request.PhoneNumber, request.Address, request.City, request.Post, request.Email);
            if (client < 0)
                return BadRequest(new ApiProblemDetailsException("Client wasn't added.", Status400BadRequest));

            return Ok(client);
        }

        [HttpPost]
        [AutoWrapIgnore]
        [Route("clientSimple")]
        [ProducesResponseType(typeof(int), Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsResponse), Status400BadRequest)]
        public async Task<IActionResult> AddClientSimple(int count)
        {
            var client = await _manager.AddCountOfClientsSimple(count);
            if (client < 0)
                return BadRequest(new ApiProblemDetailsException("Client wasn't added.", Status400BadRequest));

            return Ok(client);
        }

        [HttpPost]
        [AutoWrapIgnore]
        [Route("clientConnection")]
        [ProducesResponseType(typeof(int), Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsResponse), Status400BadRequest)]
        public async Task<IActionResult> AddClientConnection(int count)
        {
            var client = await _manager.AddCountOfClientsConnection(count);
            if (client < 0)
                return BadRequest(new ApiProblemDetailsException("Client wasn't added.", Status400BadRequest));

            return Ok(client);
        }

        [HttpPost]
        [AutoWrapIgnore]
        [Route("clientStringBuilder")]
        [ProducesResponseType(typeof(int), Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsResponse), Status400BadRequest)]
        public async Task<IActionResult> AddClientStringBuilder(int count)
        {
            var client = await _manager.AddCountOfClientsStringBuilder(count);
            if (client < 0)
                return BadRequest(new ApiProblemDetailsException("Client wasn't added.", Status400BadRequest));

            return Ok(client);
        }

        [HttpPost]
        [AutoWrapIgnore]
        [Route("clientEF")]
        [ProducesResponseType(typeof(int), Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsResponse), Status400BadRequest)]
        public async Task<IActionResult> AddClientEF(int count)
        {
            var client = await _manager.AddCountOfClientsEF(count);
            if (client < 0)
                return BadRequest(new ApiProblemDetailsException("Client wasn't added.", Status400BadRequest));

            return Ok(client);
        }
        [HttpPost]
        [AutoWrapIgnore]
        [Route("clientEFRange")]
        [ProducesResponseType(typeof(int), Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsResponse), Status400BadRequest)]
        public async Task<IActionResult> AddClientEFRange(int count)
        {
            var client = await _manager.AddCountOfClientsEFRange(count);
            if (client < 0)
                return BadRequest(new ApiProblemDetailsException("Client wasn't added.", Status400BadRequest));

            return Ok(client);
        }
        [HttpPost]
        [AutoWrapIgnore]
        [Route("clientDapperSimple")]
        [ProducesResponseType(typeof(int), Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsResponse), Status400BadRequest)]
        public async Task<IActionResult> AddClientDapperSimple(int count)
        {
            var client = await _manager.AddCountOfClientsDapperSimple(count);
            if (client < 0)
                return BadRequest(new ApiProblemDetailsException("Client wasn't added.", Status400BadRequest));

            return Ok(client);
        }
        [HttpPost]
        [AutoWrapIgnore]
        [Route("clientDapperList")]
        [ProducesResponseType(typeof(int), Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsResponse), Status400BadRequest)]
        public async Task<IActionResult> AddClientDapperList(int count)
        {
            var client = await _manager.AddCountOfClientsDapperList(count);
            if (client < 0)
                return BadRequest(new ApiProblemDetailsException("Client wasn't added.", Status400BadRequest));

            return Ok(client);
        }
        [HttpPost]
        [AutoWrapIgnore]
        [Route("clientDapperBulk")]
        [ProducesResponseType(typeof(int), Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsResponse), Status400BadRequest)]
        public async Task<IActionResult> AddClientDapperBulk(int count)
        {
            var client = await _manager.AddCountOfClientsDapperBulk(count);
            if (client < 0)
                return BadRequest(new ApiProblemDetailsException("Client wasn't added.", Status400BadRequest));

            return Ok(client);
        }
    }
}

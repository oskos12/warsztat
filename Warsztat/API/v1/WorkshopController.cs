using AutoMapper;
using AutoWrapper.Extensions;
using AutoWrapper.Filters;
using AutoWrapper.Models;
using AutoWrapper.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Warsztat.Contracts;
using Warsztat.Data.Entity;
using Warsztat.DTO.Request;
using Warsztat.DTO.Response;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Warsztat.API.v1
{
    [Route("api/v1")]
    [ApiController]
    public class WorkshopController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWorkshopManager _workshopManager;
        public WorkshopController(IMapper mapper, IWorkshopManager workshopManager)
        {
            _mapper = mapper;
            _workshopManager = workshopManager;
        }

        [HttpGet]
        [AutoWrapIgnore]
        [Route("users")]
        [ProducesResponseType(typeof(IEnumerable<Users>), Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsResponse), Status404NotFound)]
        public async Task<ApiResponse> GetUsers()
        {
            var users = await _workshopManager.GetUsers();
            if (!users.Any())
                return new ApiResponse(new ApiProblemDetailsException("User wasn't found.", Status404NotFound), 404);

            var data = _mapper.Map<IEnumerable<Users>>(users);
            return new ApiResponse(data);
        }

        [HttpPost]
        [AutoWrapIgnore]
        [Route("login")]
        [ProducesResponseType(typeof(Response), Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsResponse), Status404NotFound)]
        public async Task<ApiResponse> Login([FromBody] UsersRequest request)
        {
            var token = await _workshopManager.LoginUser(request.Login, request.Password);
            if (token == "Niepoprawny login")
                return new ApiResponse(new ApiProblemDetailsException("User wasn't found.", Status404NotFound), 404);
            else if (token == "Niepoprawne haslo")
                return new ApiResponse(new ApiProblemDetailsException("Wrong password.", Status404NotFound), 404);

            return new ApiResponse(new Response { data = token });
        }

        [HttpPost]
        [AutoWrapIgnore]
        [Route("signup")]
        [ProducesResponseType(typeof(bool), Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsResponse), Status400BadRequest)]
        public async Task<ApiResponse> Register([FromBody] RegisterRequest request)
        {
            if (await _workshopManager.CheckEmail(request.Email))
                return new ApiResponse(new ApiProblemDetailsException("Email already registered", Status400BadRequest), 400);
            if (await _workshopManager.CheckLogin(request.Login))
                return new ApiResponse(new ApiProblemDetailsException("Login already registered", Status400BadRequest), 400);
            var user = await _workshopManager.RegisterUser(request.Login, request.Password, request.Email);
            if (user < 0)
                return new ApiResponse(new ApiProblemDetailsException("Something went wrong", Status400BadRequest), 400);
            var client = await _workshopManager.AddClient(request.Name, request.Surname, request.PhoneNumber, request.City, request.Post, request.Address, user);
            if (client < 0)
                return new ApiResponse(new ApiProblemDetailsException("Something went wrong", Status400BadRequest), 400);


            return new ApiResponse(true);
        }

        [Authorize]
        [HttpGet]
        [AutoWrapIgnore]
        [Route("cars")]
        [ProducesResponseType(typeof(IEnumerable<CarsResponse>), Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsResponse), Status404NotFound)]
        public async Task<ApiResponse> GetCars()
        {
            var cars = await _workshopManager.GetCars();
            if (!cars.Any())
                return new ApiResponse(new ApiProblemDetailsException("Car wasn't found.", Status404NotFound), 404);

            var data = _mapper.Map<IEnumerable<CarsResponse>>(cars);
            return new ApiResponse(new Response { count = data.Count(), data = data });
        }

        [Authorize]
        [HttpPost]
        [AutoWrapIgnore]
        [Route("addCar")]
        [ProducesResponseType(typeof(IEnumerable<Cars>), Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsResponse), Status404NotFound)]
        public async Task<ApiResponse> AddCar([FromBody] AddCarRequest request)
        {
            var car = await _workshopManager.AddCar(request.Brand, request.Model, request.Registration, request.ProductionYear, request.Capacity, request.Owner.Id, request.VehicleType.Id, request.EngineType.Id);
            if (car < 0)
                return new ApiResponse(new ApiProblemDetailsException("Something went wrong", Status400BadRequest), 400);
            return new ApiResponse(true);
        }

        [Authorize]
        [HttpGet]
        [AutoWrapIgnore]
        [Route("dictionary")]
        [ProducesResponseType(typeof(IEnumerable<Dictionary>), Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsResponse), Status404NotFound)]
        public ApiResponse GetDictionary(string name)
        {
            var dict = _workshopManager.GetDictionaryValue(name);
            if (!dict.Any())
                return new ApiResponse(new ApiProblemDetailsException("Dictionary wasn't found.", Status404NotFound), 404);

            var data = _mapper.Map<IEnumerable<Dictionary>>(dict);
            return new ApiResponse(new Response { count = data.Count(), data = data });
        }

        [HttpPost]
        [AutoWrapIgnore]
        [Route("offerQuery")]
        [ProducesResponseType(typeof(Response), Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsResponse), Status404NotFound)]
        public async Task<ApiResponse> OfferQuery([FromBody] OfferRequest request)
        {
            var token = 0;
            if (request.clientId != "")
                token = await _workshopManager.AddOfferQuery(request.clientId.ToInt32(), request.query);
            else
                token = await _workshopManager.AddOfferQuery(request.phoneNumber + ' ' + request.email, request.query);
            if (token == 0)
                return new ApiResponse(new ApiProblemDetailsException("Offer query wasn't added.", Status404NotFound), 404);

            return new ApiResponse(new Response { data = token });
        }

        [Authorize]
        [HttpGet]
        [AutoWrapIgnore]
        [Route("getCars")]
        [ProducesResponseType(typeof(IEnumerable<CarsResponse>), Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsResponse), Status404NotFound)]
        public ApiResponse GetCarsByUserId(int userId)
        {
            var cars = _workshopManager.GetCarsByUserId(userId).Result;
            if (!cars.Any())
                return new ApiResponse(new ApiProblemDetailsException("Car wasn't found.", Status404NotFound), 404);

            var data = _mapper.Map<IEnumerable<CarsResponse>>(cars);
            return new ApiResponse(new Response { count = data.Count(), data = data });
        }

        [Authorize]
        [HttpGet]
        [AutoWrapIgnore]
        [Route("clients")]
        [ProducesResponseType(typeof(IEnumerable<Clients>), Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsResponse), Status404NotFound)]
        public ApiResponse GetClients()
        {
            var clients = _workshopManager.GetAllClients();
            if (!clients.Any())
                return new ApiResponse(new ApiProblemDetailsException("Client wasn't found.", Status404NotFound), 404);

            var data = _mapper.Map<IEnumerable<Clients>>(clients);
            return new ApiResponse(new Response { count = data.Count(), data = data });
        }

        [Authorize]
        [HttpGet]
        [AutoWrapIgnore]
        [Route("workers")]
        [ProducesResponseType(typeof(IEnumerable<Workers>), Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsResponse), Status404NotFound)]
        public async Task<ApiResponse> GetWorkers()
        {
            var workers = await _workshopManager.GetAllWorkers();
            if (!workers.Any())
                return new ApiResponse(new ApiProblemDetailsException("Worker wasn't found.", Status404NotFound), 404);

            var data = _mapper.Map<IEnumerable<Workers>>(workers);
            return new ApiResponse(new Response { count = data.Count(), data = data });
        }

        [Authorize]
        [HttpGet]
        [AutoWrapIgnore]
        [Route("services")]
        [ProducesResponseType(typeof(IEnumerable<ServicesResponse>), Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsResponse), Status404NotFound)]
        public async Task<ApiResponse> GetServices()
        {
            var services = await _workshopManager.GetServices();
            if (!services.Any())
                return new ApiResponse(new ApiProblemDetailsException("Service wasn't found.", Status404NotFound), 404);

            var data = _mapper.Map<IEnumerable<ServicesResponse>>(services);
            return new ApiResponse(new Response { count = data.Count(), data = data });
        }

        [Authorize]
        [HttpPost]
        [AutoWrapIgnore]
        [Route("addService")]
        [ProducesResponseType(typeof(IEnumerable<Services>), Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsResponse), Status404NotFound)]
        public async Task<ApiResponse> AddService([FromBody] AddServiceRequest request)
        {
            var service = await _workshopManager.AddService(request.ServiceType, request.Count, request.Hours, request.CostSum, request.Description, request.Registration, request.Supervisor.Id);
            if (service < 0)
                return new ApiResponse(new ApiProblemDetailsException("Something went wrong", Status400BadRequest), 400);
            return new ApiResponse(true);
        }

        [Authorize]
        [HttpGet]
        [AutoWrapIgnore]
        [Route("getServices")]
        [ProducesResponseType(typeof(IEnumerable<ServicesResponse>), Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsResponse), Status404NotFound)]
        public ApiResponse GetServicesByUserId(int userId)
        {
            var services = _workshopManager.GetServicesByUserId(userId).Result;
            if (!services.Any())
                return new ApiResponse(new ApiProblemDetailsException("Service wasn't found.", Status404NotFound), 404);

            var data = _mapper.Map<IEnumerable<ServicesResponse>>(services);
            return new ApiResponse(new Response { count = data.Count(), data = data });
        }

        [Authorize]
        [HttpGet]
        [AutoWrapIgnore]
        [Route("client")]
        [ProducesResponseType(typeof(Response), Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsResponse), Status404NotFound)]
        public ApiResponse GetClient(int userId)
        {
            var client = _workshopManager.GetClientByUserID(userId);
            if (client.Id == 0)
                return new ApiResponse(new ApiProblemDetailsException("Client wasn't found.", Status404NotFound), 404);

            return new ApiResponse(new Response { data = client.Result });
        }

        [Authorize]
        [HttpPut]
        [AutoWrapIgnore]
        [Route("client")]
        [ProducesResponseType(typeof(Response), Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsResponse), Status404NotFound)]
        public async Task<ApiResponse> UpdateClientData(UpdateClientDataRequest request)
        {
            var client = await _workshopManager.UpdateClient(request.id, request.address, request.city, request.post, request.phoneNumber);
            if (client < 0)
                return new ApiResponse(new ApiProblemDetailsException("Client wasn't found.", Status404NotFound), 404);

            return new ApiResponse(true);
        }

        [Authorize]
        [HttpPost]
        [AutoWrapIgnore]
        [Route("client")]
        [ProducesResponseType(typeof(Response), Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsResponse), Status404NotFound)]
        public async Task<ApiResponse> AddClient(AddClientRequest request)
        {
            var client = await _workshopManager.AddNewClient(request.Name, request.Surname, request.PhoneNumber, request.Address, request.City, request.Post, request.Email);
            if (client < 0)
                return new ApiResponse(new ApiProblemDetailsException("Client wasn't found.", Status404NotFound), 404);

            return new ApiResponse(true);
        }

        [Authorize]
        [HttpPut]
        [AutoWrapIgnore]
        [Route("activeClient")]
        [ProducesResponseType(typeof(Response), Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsResponse), Status404NotFound)]
        public async Task<ApiResponse> ActiveClient([Required][FromBody] int id)
        {
            var client = await _workshopManager.ActiveClient(id);
            if (client < 0)
                return new ApiResponse(new ApiProblemDetailsException("Client wasn't found.", Status404NotFound), 404);

            return new ApiResponse(true);
        }

        [Authorize]
        [HttpGet]
        [AutoWrapIgnore]
        [Route("user")]
        [ProducesResponseType(typeof(Response), Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsResponse), Status404NotFound)]
        public ApiResponse GetUser(int userId)
        {
            var user = _workshopManager.GetUserById(userId);
            if (user.Id == 0)
                return new ApiResponse(new ApiProblemDetailsException("User wasn't found.", Status404NotFound), 404);

            return new ApiResponse(new Response { data = user });
        }

        [Authorize]
        [HttpGet]
        [AutoWrapIgnore]
        [Route("clientsUser")]
        [ProducesResponseType(typeof(IEnumerable<ClientsWithUser>), Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsResponse), Status404NotFound)]
        public ApiResponse GetClientsWithUser()
        {
            var clients = _workshopManager.GetAllClientsWithUser();
            if (!clients.Any())
                return new ApiResponse(new ApiProblemDetailsException("Client wasn't found.", Status404NotFound), 404);

            var data = _mapper.Map<IEnumerable<ClientsWithUser>>(clients);
            return new ApiResponse(new Response { count = data.Count(), data = data });
        }

        [Authorize]
        [HttpPut]
        [AutoWrapIgnore]
        [Route("typeUser")]
        [ProducesResponseType(typeof(Response), Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsResponse), Status404NotFound)]
        public async Task<ApiResponse> UserType([Required] int id, [FromBody] int typeId)
        {
            var user = await _workshopManager.UserType(id, typeId);
            if (user < 0)
                return new ApiResponse(new ApiProblemDetailsException("User wasn't found.", Status404NotFound), 404);

            return new ApiResponse(true);
        }

        [Authorize]
        [HttpPost]
        [AutoWrapIgnore]
        [Route("addWorkers")]
        [ProducesResponseType(typeof(IEnumerable<Services>), Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsResponse), Status404NotFound)]
        public async Task<ApiResponse> AddWorkersToService([FromBody] AddWorkersToServiceRequest request)
        {
            var service = await _workshopManager.AddWorkersToService(request.Id, request.Workers.NewWorkers);
            if (service < 0)
                return new ApiResponse(new ApiProblemDetailsException("Something went wrong", Status400BadRequest), 400);
            return new ApiResponse(true);
        }

        [Authorize]
        [HttpPut]
        [AutoWrapIgnore]
        [Route("finishService")]
        [ProducesResponseType(typeof(Response), Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsResponse), Status404NotFound)]
        public async Task<ApiResponse> FinishService([Required][FromBody] int id)
        {
            var service = await _workshopManager.FinishService(id);
            if (service < 0)
                return new ApiResponse(new ApiProblemDetailsException("Service wasn't found.", Status404NotFound), 404);

            return new ApiResponse(true);
        }

        [Authorize]
        [HttpGet]
        [AutoWrapIgnore]
        [Route("serviceWorkers")]
        [ProducesResponseType(typeof(IEnumerable<Workers>), Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsResponse), Status404NotFound)]
        public async Task<ApiResponse> GetWorkersInService(int id)
        {
            var workers = await _workshopManager.GetWorkersInService(id);
            if (!workers.Any())
                return new ApiResponse(new ApiProblemDetailsException("Worker wasn't found.", Status404NotFound), 404);

            var data = _mapper.Map<IEnumerable<Workers>>(workers);
            return new ApiResponse(new Response { count = data.Count(), data = data });
        }

        [Authorize]
        [HttpGet]
        [AutoWrapIgnore]
        [Route("applications")]
        [ProducesResponseType(typeof(IEnumerable<ApplicationsView>), Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsResponse), Status404NotFound)]
        public async Task<ApiResponse> GetAllApplications()
        {
            var applications = await _workshopManager.GetAllApplications();
            if (!applications.Any())
                return new ApiResponse(new ApiProblemDetailsException("Application wasn't found.", Status404NotFound), 404);

            var data = _mapper.Map<IEnumerable<ApplicationsView>>(applications);
            return new ApiResponse(new Response { count = data.Count(), data = data });
        }

        [Authorize]
        [HttpPut]
        [AutoWrapIgnore]
        [Route("applications")]
        [ProducesResponseType(typeof(Response), Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsResponse), Status404NotFound)]
        public async Task<ApiResponse> ChangeApplicationStatus([FromBody] UpdateApplicationRequest request)
        {
            var application = await _workshopManager.ChangeApplicationStatus(request.IdApplication, request.Status);
            if (application < 0)
                return new ApiResponse(new ApiProblemDetailsException("Application wasn't found.", Status404NotFound), 404);

            return new ApiResponse(true);
        }

        [Authorize]
        [HttpPut]
        [AutoWrapIgnore]
        [Route("activeCar")]
        [ProducesResponseType(typeof(Response), Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsResponse), Status404NotFound)]
        public async Task<ApiResponse> ActiveCar([Required][FromBody] int id)
        {
            var car = await _workshopManager.ActiveCar(id);
            if (car < 0)
                return new ApiResponse(new ApiProblemDetailsException("Car wasn't found.", Status404NotFound), 404);

            return new ApiResponse(true);
        }

        [Authorize]
        [HttpPut]
        [AutoWrapIgnore]
        [Route("typeCar")]
        [ProducesResponseType(typeof(Response), Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsResponse), Status404NotFound)]
        public async Task<ApiResponse> CarType([Required] int id, [FromBody] int typeId)
        {
            var car = await _workshopManager.CarType(id, typeId);
            if (car < 0)
                return new ApiResponse(new ApiProblemDetailsException("Car wasn't found.", Status404NotFound), 404);

            return new ApiResponse(true);
        }

        [Authorize]
        [HttpGet]
        [AutoWrapIgnore]
        [Route("carServices")]
        [ProducesResponseType(typeof(IEnumerable<ServicesResponse>), Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsResponse), Status404NotFound)]
        public ApiResponse GetServicesByCarId(int carId)
        {
            var services = _workshopManager.GetServicesByCarId(carId).Result;
            if (!services.Any())
                return new ApiResponse(new ApiProblemDetailsException("Service wasn't found.", Status404NotFound), 404);

            var data = _mapper.Map<IEnumerable<ServicesResponse>>(services);
            return new ApiResponse(new Response { count = data.Count(), data = data });
        }

        [Authorize]
        [HttpGet]
        [AutoWrapIgnore]
        [Route("workersUser")]
        [ProducesResponseType(typeof(IEnumerable<ClientsWithUser>), Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsResponse), Status404NotFound)]
        public ApiResponse GetWorkersWithUser()
        {
            var workers = _workshopManager.GetAllWorkersWithUser();
            if (!workers.Any())
                return new ApiResponse(new ApiProblemDetailsException("Worker wasn't found.", Status404NotFound), 404);

            var data = _mapper.Map<IEnumerable<ClientsWithUser>>(workers);
            return new ApiResponse(new Response { count = data.Count(), data = data });
        }

        [Authorize]
        [HttpPut]
        [AutoWrapIgnore]
        [Route("activeWorker")]
        [ProducesResponseType(typeof(Response), Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsResponse), Status404NotFound)]
        public async Task<ApiResponse> ActiveWorker([Required][FromBody] int id)
        {
            var worker = await _workshopManager.ActiveWorker(id);
            if (worker < 0)
                return new ApiResponse(new ApiProblemDetailsException("Worker wasn't found.", Status404NotFound), 404);

            return new ApiResponse(true);
        }

        [HttpGet]
        [Route("test")]
        public string[] Test()
        {
            string[] x = { "asd", "asd" };
            return x;
        } 
    }
}

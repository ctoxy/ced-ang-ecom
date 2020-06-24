using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ClientsController : BaseApiController
    {
        private readonly IGenericRepository<Client> _clientsRepo;
        private readonly IMapper _mapper;
        public ClientsController(
            IGenericRepository<Client> clientsRepo,
            
            IMapper mapper
        )
        {
            _clientsRepo = clientsRepo;            
            _mapper = mapper;            
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ClientToReturnDto>>> GetClients([FromQuery]ClientSpecParams clientParams)
        {
            var spec = new ClientsWithSpecification(clientParams);
            var countSpec = new ClientWithFiltersForCountSpecification(clientParams);
            var totalItems = await _clientsRepo.CountAsync(countSpec);
            var clients = await _clientsRepo.ListAsync(spec);

            var data = _mapper.Map<IReadOnlyList<Client>, IReadOnlyList<ClientToReturnDto>>(clients);
            return Ok(new Pagination<ClientToReturnDto>(clientParams.PageIndex,
            clientParams.PageSize, totalItems, data));
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<ClientToReturnDto>> GetProduct(int id)
        {
            var client =  await _clientsRepo.GetByIdAsync(id);
            return _mapper.Map<Client, ClientToReturnDto>(client);
        }

    }
}
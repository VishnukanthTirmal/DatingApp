using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DatingApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IDatingRepository _repo;
        private readonly IMapper _mapper;

        public UsersController(IDatingRepository repo, IMapper mapper)
        {
            this._mapper = mapper;
            this._repo = repo;

        }
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _repo.GetUsers();
            var usersToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);
            return Ok(usersToReturn);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUSer(int id)
        {
            var user = await _repo.GetUser(id);
            var userToReturn =_mapper.Map<UserForDetailedDto>(user);
            return Ok(userToReturn);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser (int id, [FromBody] UserForUpdateDto userForUpdateDto)
        {
            if(ModelState.IsValid== false)
              return BadRequest(ModelState);

             var curentUserId= int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
             var userFromRepo= await _repo.GetUser(id);

            if(userFromRepo == null)
               return NotFound($"Cound not find user with an id if {id}");

            if(curentUserId != userFromRepo.Id)
                return Unauthorized();

                _mapper.Map(userForUpdateDto, userFromRepo);

                if(await _repo.SaveAll())
                    return NoContent();

                throw new  Exception($"Updating user {id} failed on save");
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using MongoDbExample.Dtos;
using MongoDbExample.Models;
using MongoDbExample.Repositories.Interfaces;

namespace MongoDbExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _userRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsers(string id)
        {
            return Ok(await _userRepository.GetByIdAsync(id));
        }

        [HttpGet]
        [Route("user-name")]
        public async Task<IActionResult> GetUserByName(string name)
        {
            return Ok(await _userRepository.GetByFilterAsync(x => x.FirstName == name));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserCreateDto createDto)
        {
            return Ok(await _userRepository.AddAsync(new User
            {
                FirstName = createDto.FirstName,
                LastName = createDto.LastName,
                Age = createDto.Age,
                CreatedDate = DateTime.Now,
                Books = createDto.Books.Select(x => new Book
                {
                    Author = x.Author,
                    Name = x.Name
                }).ToList()
            }));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserUpdateDto updateDto)
        {
            return Ok(await _userRepository.UpdateAsync(new User
            {
                Id = updateDto.Id,
                FirstName = updateDto.FirstName,
                LastName = updateDto.LastName,
                Age = updateDto.Age,
                Books = updateDto.Books.Select(x => new Book
                {
                    Author = x.Author,
                    Name = x.Name
                }).ToList()
            }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _userRepository.DeleteByIdAsync(id);
            return Ok();
        }

        [HttpDelete]
        [Route("delete-range")]
        public async Task<IActionResult> DeleteRange(List<string> ids)
        {
            await _userRepository.DeleteByFilterAsync(x => ids.Contains(x.Id));
            return Ok();
        }
    }
}

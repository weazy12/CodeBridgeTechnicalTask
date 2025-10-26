using Microsoft.AspNetCore.Mvc;
using TechnicalTask.BLL.DTOs.Dog;
using TechnicalTask.BLL.Mediatr.Dog.Create;
using TechnicalTask.BLL.Mediatr.Dog.GetAll;

namespace TechnicalTask.WebApi.Controllers.Dog
{
    public class DogsController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetDogs([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? attribute = null,[FromQuery] string order = "asc")
        {
            return HandleResult(await Mediator.Send(new GetAllDogsQuery(pageNumber, pageSize, attribute, order)));
        }

        [HttpPost]
        public async Task<IActionResult> CreateDog([FromBody] CreateDogDto createDogDto)
        {
            return HandleResult(await Mediator.Send(new CreateDogCommand(createDogDto)));
        }
    }
}

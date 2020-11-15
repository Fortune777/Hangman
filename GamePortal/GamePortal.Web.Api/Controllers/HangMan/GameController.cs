using APerepechko.HangMan.Logic.Model;
using APerepechko.HangMan.Logic.Services;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace GamePortal.Web.Api.Controllers.Hangman
{
    //[Authorize]
    [RoutePrefix("api/hangman")]
    public class HangmanController : ApiController
    {
        private readonly IHangmanService _hangmanService;
        public HangmanController(IHangmanService hangmanService)
        {
            _hangmanService = hangmanService;
        }

        [HttpGet]
        [Route("SelectWordsFromThemeAsync/{id:int}")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(WordDto))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public async Task<IHttpActionResult> SelectWordsFromThemeAsync(int id)
        {
            var result = await _hangmanService.SelectWordsFromThemeAsync(id);

            if (result.IsFailure)
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }

            return result.Value.HasNoValue ? (IHttpActionResult)NotFound() : Ok(result.Value.Value);
        }


        // [HttpGet, Authorize]
        [HttpGet]
        [Route("GetAllThemesAsync")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<ThemeDto>))]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> GetAllThemesAsync()
        {
            var result = await _hangmanService.GetAllThemesAsync();
            return result.IsSuccess ? Ok(result.Value) : (IHttpActionResult)StatusCode(HttpStatusCode.InternalServerError);
        }



        [HttpGet]
        [Route("GenerateRandomWordAsync")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(WordDto))]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> GenerateRandomWordAsync()
        {
            var result = await _hangmanService.GenerateRandomWordAsync();
            return result.IsSuccess ? Ok(result.Value) : (IHttpActionResult)StatusCode(HttpStatusCode.InternalServerError);
        }

        [HttpPost]
        [Route("IsLetterExistWord")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(WordDto))]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public IHttpActionResult IsLetterExistWord([FromBody] WordDto model)
        {
            var result = _hangmanService.IsLetterExistWord(model);
            return result.IsSuccess ? Ok(result.Value) : (IHttpActionResult)StatusCode(HttpStatusCode.InternalServerError);
        }
            
        //update
        [HttpPut]
        [Route("UpdateStatistics/Update/{id:int}")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(UserStatisticsDto))]
        [SwaggerResponse(HttpStatusCode.NoContent)]
        public IHttpActionResult UpdateUserStatistics(int id, [FromBody] UserStatisticsDto model)
        {
            var result = _hangmanService.UpdateStatistics(id, model);
            return result.IsSuccess ? StatusCode(HttpStatusCode.NoContent) : (IHttpActionResult)InternalServerError();
        }


        [HttpGet]
        [Route("GetAllUsers/Get")]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(UserDto))]
        [SwaggerResponse(HttpStatusCode.NoContent)]
        public IHttpActionResult GetAllUsers([FromBody] UserDto model)
        {
            //  var users = _hangmanService.GetAllUsers(model);

            //return Ok(users);
            return Ok();
        }



        //Route {id} - параметр id имя должен быть такое же, как и в сигнатуре метода 
        //[HttpGet]
        //[Route("GetUserById/Get/{id:int}")]
        //[SwaggerResponse(HttpStatusCode.OK, Type = typeof(UserDto))]
        //[SwaggerResponse(HttpStatusCode.NoContent)]
        //public async Task<IHttpActionResult> GetUserById(int id)
        //{
        //    var result = await _hangmanService.GetUserByIdAsync(id);
        //    return result.IsSuccess ? StatusCode(HttpStatusCode.NoContent) : (IHttpActionResult)InternalServerError(new Exception(result.Error));
        //}

        [HttpPost]
        [Route("AddTheme")]
        public IHttpActionResult AddTheme([FromBody] UserDto model)
        {
            return Created($"/hangman/{model.Id}", model);
        }


        //insert
        [HttpPost]
        [Route("")]
        public IHttpActionResult Add([FromBody] UserDto model)
        {
            return Created($"/hangman/{model.Id}", model);
        }
        //delete
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            //delete
            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

    }

}
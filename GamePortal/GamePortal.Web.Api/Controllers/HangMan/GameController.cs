using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using APerepechko.HangMan.Logic.Model;
using APerepechko.HangMan.Logic.Services;

 
    
namespace GamePortal.Web.Api.Controllers.Hangman
{
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
        public async Task<IHttpActionResult> SelectWordsFromThemeAsync(int id)
        {
            var result = await _hangmanService.SelectWordsFromThemeAsync(id);

            if (result.IsFailure)
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }

            return result.Value.HasNoValue ? (IHttpActionResult)NotFound() : Ok(result.Value.Value);
        }

        [HttpGet, Authorize]
        [Route("GetAllThemesAsync")]
        public async Task<IHttpActionResult> GetAllThemesAsync()
        {
            var result = await _hangmanService.GetAllThemesAsync();
            return result.IsSuccess ? Ok(result.Value) : (IHttpActionResult)StatusCode(HttpStatusCode.InternalServerError);
        }

        [HttpGet]
        [Route("GenerateRandomWordAsync")]
        public async Task<IHttpActionResult> GenerateRandomWordAsync()
        {
            var result = await _hangmanService.GenerateRandomWordAsync();
            return result.IsSuccess ? Ok(result.Value) : (IHttpActionResult)StatusCode(HttpStatusCode.InternalServerError);
        }

        [HttpPost]
        [Route("IsLetterExistWord")]
        public IHttpActionResult IsLetterExistWord([FromBody] WordDto model)
        {
            var result =  _hangmanService.IsLetterExistWord(model);
            return result.IsSuccess ? Ok(result.Value) : (IHttpActionResult)StatusCode(HttpStatusCode.InternalServerError);
        }

        //update
        [HttpPut]
        [Route("UpdateStatistics/Update/{id:int}")]
        public IHttpActionResult UpdateUserStatistics(int id, [FromBody] UserStatisticsDto model)
        {
            var result =  _hangmanService.UpdateStatistics(id, model);
            return result.IsSuccess ? StatusCode(HttpStatusCode.NoContent) : (IHttpActionResult)InternalServerError();
        }


        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllUsers([FromBody] WordDto model)
        {
            //  var users = _hangmanService.GetAllUsers(model);

            //return Ok(users);
            return Ok();
        }



        //Route {id} - параметр id имя должен быть такое же, как и в сигнатуре метода 
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetUserById(int id)
        {
            //если не нашли юзера тогда
            // return id == null ? (IHttpActionResult)NotFound() : Ok(user);
            return Ok();
        }






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
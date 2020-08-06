using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using APerepechko.HangMan.Logic.Model;
using APerepechko.HangMan.Logic.Services;
using APerepechko.HangMan.Model.Logic;
using FluentValidation;
using FluentValidation.WebApi;
using JetBrains.Annotations;
using NullGuard;

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
        [Route("SelectWordsFromTheme/{id:int}")]
        public IHttpActionResult SelectWordsFromTheme(int id)
        {
            var result = _hangmanService.SelectWordsFromTheme(id);

            if (result.IsFailure)
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }

            return result.Value.HasNoValue ? (IHttpActionResult)NotFound() : Ok(result.Value.Value);
        }

        [HttpGet]
        [Route("GetAllThemes")]
        public IHttpActionResult GetAllThemes()
        {
            var result = _hangmanService.GetAllThemes();
            return result.IsSuccess ? Ok(result.Value) : (IHttpActionResult)StatusCode(HttpStatusCode.InternalServerError);
        }

        [HttpGet]
        [Route("GenerationRandomWord")]
        public IHttpActionResult GenerationRandomWord()
        {
            var result = _hangmanService.GenerateRandomWord();
            return result.IsSuccess ? Ok(result.Value) : (IHttpActionResult)StatusCode(HttpStatusCode.InternalServerError);
        }

        [HttpPost]
        [Route("IsLetterExistWord")]
        public IHttpActionResult IsLetterExistWord([FromBody] WordDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _hangmanService.IsLetterExistWord(model);

            return result.IsSuccess ? Ok(result.Value) : (IHttpActionResult)StatusCode(HttpStatusCode.InternalServerError);
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


        //update
        [HttpPut]
        [Route("UserStatistics/Update/{id:int}")]
        public IHttpActionResult UpdateUserStatistics(int id, [FromBody] UserStatisticsDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _hangmanService.UpdateStatistics(model);
            return result.IsSuccess ? StatusCode(HttpStatusCode.NoContent) : (IHttpActionResult)InternalServerError();
        }

        //delete
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            //delete
            return StatusCode(HttpStatusCode.NoContent);
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
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

    }

}
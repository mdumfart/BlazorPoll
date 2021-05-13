using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Net;
using System.Threading.Tasks;
using BlazorPoll.Server.Models;
using BlazorPoll.Shared.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;

namespace BlazorPoll.Server.Controllers
{
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        private ErrorResponse _errorResponse = null;

        [Route("/r")]
        public async Task<IActionResult> Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context.Error;

            var exceptionType = exception.GetType();

            if (exceptionType == typeof(CustomException))
            {
                var customException = (CustomException) exception;
                _errorResponse.Message = customException.Message;
                _errorResponse.StatusDescription = customException.StatusDescription;
                _errorResponse.Status = customException.StatusCode;
            }


            if (_errorResponse != null)
                await SendError();

            // Send Problem for fallback
            return Problem(exception.Message);

        }

        private void CreateValidationError(ValidationException exception)
        {
            _errorResponse =
                new ErrorResponse(422, HttpStatusCode.UnprocessableEntity.ToString(), exception.Message);
        }

        private void CreateDuplicateEntryError(DuplicateNameException exception)
        {
            _errorResponse =
                new ErrorResponse(409, HttpStatusCode.Conflict.ToString(), exception.Message);
        }

        private void CreateArgumentError(ArgumentException exception)
        {
            _errorResponse =
                new ErrorResponse(400, HttpStatusCode.BadRequest.ToString(), exception.Message);
        }

        private Task SendError()
        {
            HttpContext.Response.ContentType = "application/json";
            HttpContext.Response.StatusCode = _errorResponse.Status;

            return HttpContext.Response.WriteAsync(_errorResponse.ToString());
        }
    }
}
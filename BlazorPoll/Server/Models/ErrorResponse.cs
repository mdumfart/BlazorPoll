using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BlazorPoll.Server.Models
{
    public class ErrorResponse
    {
        public int Status { get; set; } = 500;
        public string StatusDescription { get; set; } = HttpStatusCode.InternalServerError.ToString();
        public string Message { get; set; } = "Sorry, something went wrong";

        public ErrorResponse(int status, string statusDescription, string message)
        {
            Status = status;
            StatusDescription = statusDescription;
            Message = message;
        }

        public ErrorResponse()
        {
        }

        public override string ToString()
        {
            var serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            return JsonConvert.SerializeObject(this, serializerSettings);
        }
    }
}

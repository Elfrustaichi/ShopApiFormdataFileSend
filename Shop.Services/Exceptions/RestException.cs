using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ShopNT.Services.Exceptions
{
    public class RestException:Exception
    {

        public RestException(HttpStatusCode code,string errorKey,string errorMessage, string message=null)
        {
            Code = code;
            Message = message;
            Errors=new List<RestExceptionError> { new RestExceptionError(errorKey,errorMessage) };
        }
        public RestException(HttpStatusCode code, string message, List<RestExceptionError> errors)
        {
            Code = code;
            Message = message;
            Errors = errors;
        }
        public RestException(HttpStatusCode code, string message)
        {
            Code = code;
            Message = message;
            Errors = new List<RestExceptionError> { };
            
        }
        public RestException(HttpStatusCode code, List<RestExceptionError> errors)
        {
            Code = code;
            Errors = errors;
        }
        public HttpStatusCode Code { get; set; }
        public string Message {get;set;}

        public List<RestExceptionError> Errors { get; set; }


    }

    public class RestExceptionError
    {
        public RestExceptionError(string key,string message)
        {
            Key = key;
            Message = message;
        }
        public string Key { get; set; }

        public string Message { get; set;}
    }
}

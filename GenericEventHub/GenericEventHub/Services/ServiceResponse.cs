using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenericEventHub.Services
{
    public class ServiceResponse
    {
        private string _message;
        private bool _success;

        public ServiceResponse(string message, bool success)
        {
            _message = message;
            _success = success;
        }

        public string Message { get { return _message; } }
        public bool Success { get { return _success; } }
    }
}
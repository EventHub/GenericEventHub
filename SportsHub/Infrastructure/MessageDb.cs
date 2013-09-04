using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SportsHub.Models;

namespace SportsHub.Infrastructure
{
    public class MessageDb : DatabaseServices
    {
        public string AddMessage(Message newMessage)
        {
            var result = string.Empty;
            try
            {
                AddEntity(newMessage);
                result = "You message has been posted!";
            }
            catch (Exception)
            {
                result = "Something went terribly wrong!";
                throw;
            }
            return result;
        }
    }
}
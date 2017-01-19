using Microsoft.AspNetCore.Mvc;
using System;
using EventService.Events;
using System.Collections.Generic;

namespace EventService.Controllers
{
   [Route("event")]
   public class EventController : Controller
   {
      private readonly IDataStore ds;

      public EventController()
      {
         ds = new BufferDataStore(30);
      }

      [HttpPost("{name}")]
      public void Post(string name, string numberOfProducts)
      {
         var data = new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("numberOfProducts", numberOfProducts) };
         RouteEvent("post", name, data);
      }

      [HttpGet("{name}")]
      public IEnumerable<DataEvent> Get(string name)
      {
         return RouteEvent("get", name, null);
      }

      private IEnumerable<DataEvent> RouteEvent(string httpVerb, string eventName, IList<KeyValuePair<string, string>> data)
      {
         IEvent evnt;

         switch (eventName)
         {
            case "orderPlaced":
               evnt = new OrderEvent(ds);

               if (httpVerb == "post")
                  evnt.capture(eventName, data);
               else if (httpVerb == "get")
                  return ds.read(eventName);
               return null;

            case "paymentGatewayDown":
               evnt = new PaymentGatewayEvent(ds);
               if (httpVerb == "post")
                  evnt.capture("paymentGatewayDown");
               else if (httpVerb == "get")
                  return ds.read(eventName);
               return null;

            default:
               throw new EventNotFoundException();
         }
      }
   }

   internal class EventNotFoundException : Exception
   {
      public EventNotFoundException()
         : base(string.Empty)
      {
      }
   }
}

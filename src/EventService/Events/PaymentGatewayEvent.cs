using System;
using System.Collections.Generic;

namespace EventService.Events
{
   internal class PaymentGatewayEvent : IEvent
   {
      private readonly IDataStore ds;

      public PaymentGatewayEvent(IDataStore ds)
      {
         this.ds = ds;
      }

      public void capture(string eventName)
      {
         ds.create(eventName);
      }

      public void capture(string eventName, IList<KeyValuePair<string, string>> data)
      {
         capture(eventName);
      }
   }
}
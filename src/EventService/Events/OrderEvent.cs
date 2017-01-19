using System;
using System.Collections.Generic;

namespace EventService.Events
{
   internal class OrderEvent : IEvent
   {
      private readonly IDataStore ds;

      public OrderEvent(IDataStore ds)
      {
         this.ds = ds;
      }

      public void capture(string eventName)
      {
         ds.create(eventName);
      }

      public void capture(string eventName, IList<KeyValuePair<string, string>> data)
      {
         ds.create(eventName, data);
      }
   }
}
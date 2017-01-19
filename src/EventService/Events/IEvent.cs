using System.Collections.Generic;

namespace EventService.Events
{
   internal interface IEvent
   {
      void capture(string eventName);
      void capture(string eventName, IList<KeyValuePair<string, string>> data);
   }
}
using System;
using EventService.Events;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventService.Controllers
{
   internal class BufferDataStore : IDataStore
   {
      private int expireInDays;
      private static volatile IDictionary<string, List<DataEvent>> dictionary;

      internal BufferDataStore(int expireInDays)
      {
         this.expireInDays = expireInDays;

         if (dictionary == null)
            dictionary = new Dictionary<string, List<DataEvent>>();
      }

      public void create(string eventName)
      {
         if (!dictionary.ContainsKey(eventName))
            dictionary.Add(eventName, new List<DataEvent>());

         dictionary[eventName].Add(new DataEvent());
      }

      public void create(string eventName, IEnumerable<KeyValuePair<string, string>> data)
      {
         if (!dictionary.ContainsKey(eventName))
            dictionary.Add(eventName, new List<DataEvent>());

         dictionary[eventName].Add(new DataEvent
         {
            data = data
         });
      }

      public IEnumerable<DataEvent> read(string eventName)
      {
         CheckForExpired(eventName);

         return dictionary[eventName];
      }

      private void CheckForExpired(string eventName)
      {
         if (!dictionary.ContainsKey(eventName)) return;

         var listToRemove = new List<DataEvent>();

         foreach(var evnt in dictionary[eventName])
         {
            if (evnt.dateCreated <= DateTime.Now.AddDays(-expireInDays))
               listToRemove.Add(evnt);
            else
               break;
         }

         foreach(var evnt in listToRemove)
            dictionary[eventName].Remove(evnt);
      }
   }
}
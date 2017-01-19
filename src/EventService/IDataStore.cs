using System.Collections.Generic;

namespace EventService.Events
{
   public interface IDataStore
   {
      void create(string eventName);
      void create(string eventName, IEnumerable<KeyValuePair<string, string>> data);
      IEnumerable<DataEvent> read(string eventName);
   }
}
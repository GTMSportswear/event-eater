using System;
using System.Collections.Generic;

namespace EventService
{
   public class DataEvent
   {
      public DateTime dateCreated { get; set; } = DateTime.Now;
      public IEnumerable<KeyValuePair<string, string>> data { get; set; } = new List<KeyValuePair<string, string>>();
   }
}

using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace App1
{


    public class ScheduleInfo
    {
        public Response response { get; set; }
    }
    public class Item3
    {
        public string title { get; set; }
        public string teacher { get; set; }
        public string where { get; set; }
        public string type { get; set; }
        public object subgroup { get; set; }
        public int fraction { get; set; }
        public bool active { get; set; }
    }

    public class Item2
    {
        public string title { get; set; }
        public List<Item3> items { get; set; }
    }

    public class Item
    {
        public string title { get; set; }
        public List<Item2> items { get; set; }
    }

    public class Response
    {
        public int count { get; set; }
        public List<Item> items { get; set; }
    }


}



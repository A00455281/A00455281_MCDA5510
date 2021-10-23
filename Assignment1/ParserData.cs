using System;
using System.Collections.Generic;

namespace Asg1
{
    public class ParserData
    {
        public ParserData()
        {
            totalInvalidCount = 0;
            data = new List<string>();
        }
        public int totalInvalidCount { get; set; }
        public List<String> data { get; set; }
    }
}

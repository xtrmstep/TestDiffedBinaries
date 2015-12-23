using System;
using System.Collections.Generic;

namespace TestDiffedBinaries.Api.Models
{
    public class DiffInfo
    {
        public bool AreEqual
        {
            get;
            set;
        }

        public string StatusMessage { get; set; }

        public List<Tuple<int, int>> Mismatches
        {
            get;
            set;
        }
    }
}

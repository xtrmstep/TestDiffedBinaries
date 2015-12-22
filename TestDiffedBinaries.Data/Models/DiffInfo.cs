using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDiffedBinaries.Data.Models
{
    public class DiffInfo
    {
        public bool AreEqual
        {
            get;
            set;
        }

        public List<Tuple<int, int>> Mismatches
        {
            get;
            set;
        }
    }
}

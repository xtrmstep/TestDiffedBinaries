using System;
using System.Collections.Generic;

namespace TestDiffedBinaries.Api.Models
{
    /// <summary>
    /// Data structure to produce JSON responses
    /// </summary>
    public class DiffInfo
    {
        public EqualStatus AreEqual
        {
            get;
            set;
        }

        /// <summary>
        /// Contains a list of mismatches (position & length) of not equal sequences
        /// </summary>
        public List<Tuple<int, int>> Mismatches
        {
            get;
            set;
        }
    }
}

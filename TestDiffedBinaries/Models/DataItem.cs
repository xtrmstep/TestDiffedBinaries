using System;

namespace TestDiffedBinaries.Api.Models
{
    public class DataItem
    {
        public Guid Id
        {
            get;
            set;
        }

        public byte[] Content
        {
            get;
            set;
        }
    }
}
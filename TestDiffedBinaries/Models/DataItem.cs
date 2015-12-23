using System;

namespace TestDiffedBinaries.Api.Models
{
    public class RequestData
    {
        public Guid? Id
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
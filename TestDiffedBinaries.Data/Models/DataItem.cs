using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestDiffedBinaries.Data.Models
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
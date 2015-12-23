using System;

namespace TestDiffedBinaries.Api.Models
{
    /// <summary>
    /// Data structure which is used to produce JSON requests
    /// </summary>
    public class RequestData
    {
        /// <summary>
        /// Slot Id of compared sequences
        /// </summary>
        public Guid? Id
        {
            get;
            set;
        }

        /// <summary>
        /// Sequence to add/update in a slot with Id
        /// </summary>
        public byte[] Content
        {
            get;
            set;
        }
    }
}
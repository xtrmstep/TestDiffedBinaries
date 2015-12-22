using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;

namespace TestDiffedBinaries.Data.Repositories
{
    public class DataRepository
    {
        private static readonly Lazy<ConcurrentDictionary<Guid, Tuple<byte[], byte[]>>> storageLazySingleton = new Lazy<ConcurrentDictionary<Guid, Tuple<byte[], byte[]>>>(() => new ConcurrentDictionary<Guid, Tuple<byte[], byte[]>>());

        private static ConcurrentDictionary<Guid, Tuple<byte[], byte[]>> Storage
        {
            get { return storageLazySingleton.Value; }
        }

        private DiffSlotType type;
        private Guid slotId;
        public DataRepository(Guid slotId, DiffSlotType type)
        {
            this.type = type;
            this.slotId = slotId;
        }

        public static Guid GetFreeSlot()
        {
            return Guid.NewGuid();
        }

        public byte[] Get()
        {
            throw new NotImplementedException();
        }

        public void Create(byte[] bytes)
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }
    }

    public enum DiffSlotType
    {
        Left,
        Right
    }
}
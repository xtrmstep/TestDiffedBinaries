using System;
using System.Collections.Concurrent;

namespace TestDiffedBinaries.Api.Repositories
{
    public class DataRepository
    {
        private static readonly Lazy<ConcurrentDictionary<Guid, Tuple<byte[], byte[]>>> storageLazySingleton = new Lazy<ConcurrentDictionary<Guid, Tuple<byte[], byte[]>>>(() => new ConcurrentDictionary<Guid, Tuple<byte[], byte[]>>());

        private static ConcurrentDictionary<Guid, Tuple<byte[], byte[]>> Storage
        {
            get
            {
                return storageLazySingleton.Value;
            }
        }

        private readonly DataRepositoryType type;
        private readonly Guid slotId;

        public DataRepository(Guid slotId, DataRepositoryType type)
        {
            this.type = type;
            this.slotId = slotId;
        }

        public byte[] Get()
        {
            Tuple<byte[], byte[]> slot;
            if (Storage.TryGetValue(slotId, out slot))
            {
                return type == DataRepositoryType.Left ? slot.Item1 : slot.Item2;
            }
            return null;
        }

        public void Create(byte[] bytes)
        {
            byte[] left = type == DataRepositoryType.Left ? bytes : null;
            byte[] right = type == DataRepositoryType.Right ? bytes : null;
            Tuple<byte[], byte[]> slot = new Tuple<byte[], byte[]>(left, right);

            Storage.TryAdd(slotId, slot);
        }

        public void Delete()
        {
            if (Storage.ContainsKey(slotId))
            {
                Tuple<byte[], byte[]> slot;
                if (Storage.TryGetValue(slotId, out slot))
                {
                    byte[] left = type == DataRepositoryType.Left ? null : slot.Item1;
                    byte[] right = type == DataRepositoryType.Right ? null : slot.Item2;
                    Storage.TryUpdate(slotId, new Tuple<byte[], byte[]>(left, right), slot);
                }
            }
        }

        public void Update(byte[] bytes)
        {
            if (Storage.ContainsKey(slotId))
            {
                Tuple<byte[], byte[]> slot;
                if (Storage.TryGetValue(slotId, out slot))
                {
                    byte[] left = type == DataRepositoryType.Left ? bytes : slot.Item1;
                    byte[] right = type == DataRepositoryType.Right ? bytes : slot.Item2;
                    Storage.TryUpdate(slotId, new Tuple<byte[], byte[]>(left, right), slot);
                }
            }
        }
    }
}
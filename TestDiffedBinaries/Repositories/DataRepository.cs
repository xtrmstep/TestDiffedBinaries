using System;
using System.Collections.Concurrent;

namespace TestDiffedBinaries.Api.Repositories
{
    /// <summary>
    /// Simplified data storage which operates with chunks of paired sequences
    /// </summary>
    /// <remarks>
    /// Each instance of the repository can work only with one item from a chunk 'left' or 'right'
    /// </remarks>
    public class DataRepository : IDataRepository
    {
        #region static storage
        /*******************************************************************
         * static storage for all server clients MUST be thread safe *
         *******************************************************************/
        private static readonly Lazy<ConcurrentDictionary<Guid, Tuple<byte[], byte[]>>> storageLazySingleton = new Lazy<ConcurrentDictionary<Guid, Tuple<byte[], byte[]>>>(() => new ConcurrentDictionary<Guid, Tuple<byte[], byte[]>>());

        private static ConcurrentDictionary<Guid, Tuple<byte[], byte[]>> Storage
        {
            get
            {
                return storageLazySingleton.Value;
            }
        }

        public static Tuple<byte[], byte[]> PickSlot(Guid id)
        {
            if (Storage.ContainsKey(id))
                return Storage[id];
            return null;
        }
        #endregion

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
            var existingSlot = PickSlot(slotId);

            byte[] left = type == DataRepositoryType.Left ? bytes : existingSlot != null ? existingSlot.Item1 : null;
            byte[] right = type == DataRepositoryType.Right ? bytes : existingSlot != null ? existingSlot.Item2 : null;
            Tuple<byte[], byte[]> slot = new Tuple<byte[], byte[]>(left, right);

            // slot with Id may be already created (eg, in parallel), if so, then update it
            if (Storage.TryAdd(slotId, slot) == false)
                Storage.TryUpdate(slotId, slot, existingSlot);
        }

        public void Delete()
        {
            // deletion does not remove a key, it just sets chunk item to Null

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
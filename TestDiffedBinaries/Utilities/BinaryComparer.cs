using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using TestDiffedBinaries.Api.Models;

namespace TestDiffedBinaries.Api.Utilities
{
    public class BinaryComparer
    {
        public static DiffInfo Compare(Tuple<byte[], byte[]> data)
        {
            var left = data.Item1;
            var right = data.Item2;

            if (left == null && right == null) return new DiffInfo { AreEqual = true, StatusMessage = "equal" };

            if (left == null || right == null || left.Length != right.Length) return new DiffInfo { AreEqual = false, StatusMessage = "size is not equal" };

            if (left.SequenceEqual(right)) return new DiffInfo { AreEqual = true, StatusMessage = "equal" };

            var notEqual = new DiffInfo
            {
                AreEqual = false,
                StatusMessage = "not equal"
            };

            notEqual.Mismatches = FindAllMismatches(left, right);
            return notEqual;
        }

        private static List<Tuple<int, int>> FindAllMismatches(byte[] left, byte[] right)
        {
            Debug.Assert(left.Length == right.Length);

            var result = new List<Tuple<int, int>>();
            int length = 0;
            int pos = -1;
            for (int i = 0; i < left.Length; i++)
            {
                if (left[i] != right[i])
                {
                    if (pos == -1) pos = i;
                    length++;
                }
                else if (length > 0)
                {
                    result.Add(new Tuple<int, int>(pos, length));
                    pos = -1;
                    length = 0;
                }
            }
            if (length > 0)
            {
                result.Add(new Tuple<int, int>(pos, length));
            }
            return result;
        }
    }
}
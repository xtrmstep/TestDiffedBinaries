using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TestDiffedBinaries.Api.Models;

namespace TestDiffedBinaries.Api.Utilities
{
    public class BinaryComparer
    {
        public static DiffInfo Compare(Tuple<byte[], byte[]> data)
        {
            var left = data.Item1;
            var right = data.Item2;

            #region quick checks

            if (left == null && right == null) return new DiffInfo { AreEqual = EqualStatus.Equal };

            if (left == null || right == null || left.Length != right.Length) return new DiffInfo { AreEqual = EqualStatus.SizeNotEqual };

            if (left.SequenceEqual(right)) return new DiffInfo { AreEqual = EqualStatus.Equal }; 
            
            #endregion

            var notEqual = new DiffInfo
            {
                AreEqual = EqualStatus.NotEqual
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

            // go through the sequence and count pos & length of mismatches

            #region find mismatches

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
            
            #endregion

            // after the loop some info might not be stored, so add it
            if (length > 0)
            {
                result.Add(new Tuple<int, int>(pos, length));
            }
            return result;
        }
    }
}
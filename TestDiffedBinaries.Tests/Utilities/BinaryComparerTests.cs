using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDiffedBinaries.Api.Models;
using TestDiffedBinaries.Api.Utilities;
using Xunit;

namespace TestDiffedBinaries.Api.Tests.Utilities
{
    public class BinaryComparerTests
    {
        [Fact(DisplayName = "BinaryComparer nulls are equal")]
        public void Should_return_equal_for_nulls()
        {
            var actual = BinaryComparer.Compare(new Tuple<byte[], byte[]>(null, null));

            Assert.Equal(EqualStatus.Equal, actual.AreEqual);
        }

        [Fact(DisplayName = "BinaryComparer null and non-null are not equal")]
        public void Should_return_notEqual_for_only_oneValue()
        {
            var actual = BinaryComparer.Compare(new Tuple<byte[], byte[]>(null, new byte[] { 1, 2, 3 }));

            Assert.Equal(EqualStatus.SizeNotEqual, actual.AreEqual);
        }

        [Fact(DisplayName = "BinaryComparer items with different size are not equal")]
        public void Should_return_notEqual_for_values_with_different_size()
        {
            var actual = BinaryComparer.Compare(new Tuple<byte[], byte[]>(new byte[] { 1, 2, 3, 4 }, new byte[] { 1, 2, 3 }));

            Assert.Equal(EqualStatus.SizeNotEqual, actual.AreEqual);
        }

        [Fact(DisplayName = "BinaryComparer returns mismatch info")]
        public void Should_return_mismatch_info()
        {
            var actual = BinaryComparer.Compare(new Tuple<byte[], byte[]>(new byte[] { 1, 2, 3, 4 }, new byte[] { 1, 2, 3, 5 }));

            Assert.Equal(EqualStatus.NotEqual, actual.AreEqual);
            Assert.Equal(1, actual.Mismatches.Count);
            Assert.Equal(3, actual.Mismatches[0].Item1);
            Assert.Equal(1, actual.Mismatches[0].Item2);
        }

        [Theory(DisplayName = "BinaryComparer returns list of mismatches")]
        [MemberData("ListOfMismatchesInputs")]
        public void Should_return_list_of_mismatches(byte[] left, byte[] right, List<Tuple<int, int>> expected)
        {
            var diff = BinaryComparer.Compare(new Tuple<byte[], byte[]>(left, right));
            var actual = diff.Mismatches;

            Assert.Equal(expected.Count, actual.Count);
            for (int i = 0; i < actual.Count; i++)
            {
                Assert.True(expected[i].Item1 == actual[i].Item1, string.Format("Item1 is not equal in #{0}", i));
                Assert.True(expected[i].Item2 == actual[i].Item2, string.Format("Item2 is not equal in #{0}", i));
            }            
        }

        public static IEnumerable<object[]> ListOfMismatchesInputs()
        {
            yield return new object[]
            {
                new byte[] { 1, 2, 3, 4 },
                new byte[] { 1, 2, 3, 5 },
                new List<Tuple<int,int>>()
                {
                    new Tuple<int,int>(3,1)
                }
            };

            yield return new object[]
            {
                new byte[] { 1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20 },
                new byte[] { 1,2,3,5,4,6,7,9,8,11,10,12,13,14,15,20,17,18,19,16 },
                new List<Tuple<int,int>>()
                {
                    new Tuple<int,int>(3,2),
                    new Tuple<int,int>(7,4),
                    new Tuple<int,int>(15,1),
                    new Tuple<int,int>(19,1)
                }
            };
        }
    }
}

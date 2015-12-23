using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDiffedBinaries.Api.Repositories;
using Xunit;

namespace TestDiffedBinaries.Api.Tests.Repositories
{
    public class DataRepositoryTests
    {
        [Fact(DisplayName = "Data Rep should create left & right items")]
        public void Should_create_left_and_right_for_same_id()
        {
            var id = Guid.NewGuid();
            var leftRep = new DataRepository(id, DataRepositoryType.Left);
            var rightRep = new DataRepository(id, DataRepositoryType.Right);

            var leftExpected = new byte[] { 1, 2, 3 };
            var rightExpected = new byte[] { 4, 5, 6 };
            leftRep.Create(leftExpected);
            rightRep.Create(rightExpected);

            var leftActual = leftRep.Get();
            var rightActual = rightRep.Get();

            Assert.True(leftExpected.SequenceEqual(leftActual));
            Assert.True(rightExpected.SequenceEqual(rightActual));
        }

        [Fact(DisplayName = "Data Rep PickSlot returns null")]
        public void PickSlot_should_return_null_when_nod_data()
        {
            Assert.Null(DataRepository.PickSlot(Guid.NewGuid()));
        }

        [Fact(DisplayName = "Data Rep PickSlot returns data")]
        public void PickSlot_should_return_slot_data()
        {
            var id = Guid.NewGuid();
            var leftRep = new DataRepository(id, DataRepositoryType.Left);
            var rightRep = new DataRepository(id, DataRepositoryType.Right);

            var leftExpected = new byte[] { 1, 2, 3 };
            var rightExpected = new byte[] { 4, 5, 6 };
            leftRep.Create(leftExpected);
            rightRep.Create(rightExpected);

            var actual = DataRepository.PickSlot(id);

            Assert.True(leftExpected.SequenceEqual(actual.Item1));
            Assert.True(rightExpected.SequenceEqual(actual.Item2));
        }
    }
}

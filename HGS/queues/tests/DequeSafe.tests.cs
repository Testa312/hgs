using System;
using Queues;
using FluentAssertions;
using Xunit;

namespace Queues.Tests
{
    public class DequeSafeTests
    {
        [Fact]
        public void Prepend_SingleItem()
        {
            // given
            int number = 5;
            var d = new DequeSafe<int>();

            // when
            d.Prepend(number);

            // then
            d.Count.Should().Be(1);
        }

        [Fact]
        public void Prepend_RangeItems()
        {
            // given
            int[] arr = {1, 2, 3, 4, 5};
            var d = new DequeSafe<int>();

            // when
            d.Prepend(arr);

            // then
            d.Count.Should().Be(arr.Length);
        }

        [Fact]
        public void Push_SingleItem()
        {
            // given
            int number = 5;
            var d = new DequeSafe<int>();

            // when
            d.Push(number);

            // then
            d.Count.Should().Be(1);
        }

        [Fact]
        public void Push_RangeItems()
        {
            // given
            int[] arr = {1, 2, 3, 4, 5};
            var d = new DequeSafe<int>();

            // when
            d.Push(arr);

            // then
            d.Count.Should().Be(arr.Length);
        }
    }   
}
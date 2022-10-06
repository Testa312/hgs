using System;
using Queues;
using FluentAssertions;
using Xunit;

namespace Queues.Tests
{
    public class DequeTests
    {
        [Fact]
        public void QueueCount_EmptyCollection()
        {
            // given
            int[] arr = null;

            // when
            Action d = () => new Deque<int>(arr);
            
            // then
            d.Should().Throw<ArgumentException>()
                .WithMessage("Collections cannot both be null");
        }

        [Fact]
        public void QueueCount_SingleItem()
        {
            // given
            int[] arr = { 1 };

            // when
            var d = new Deque<int>(arr);

            // then
            d.Count.Should().Be(1);
        }

        [Fact]
        public void QueueCount_FirstRangeItems()
        {
            // given
            int[] arr = {1, 2, 3, 4, 5};

            // when
            var d = new Deque<int>(arr);

            // then
            d.Count.Should().Be(5);
        }

        [Fact]
        public void QueueCount_RangeItems()
        {
            // given
            int[] arrFront = {5, 4, 3, 2, 1};
            int[] arrBack = {6, 7, 8, 9, 10};

            // when
            var d = new Deque<int>(arrBack, arrFront);

            // then
            d.Count.Should().Be(10);
        }

        [Fact]
        public void Prepend_SingleItem()
        {
            // given
            int number = 5;
            var d = new Deque<int>();

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
            var d = new Deque<int>();

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
            var d = new Deque<int>();

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
            var d = new Deque<int>();

            // when
            d.Push(arr);

            // then
            d.Count.Should().Be(arr.Length);
        }

        [Fact]
        public void PopFirst_SingleItemInArray()
        {
            // given
            int number = 5;
            var d = new Deque<int>();
            d.Prepend(number);

            // when
            d.PopFirst();

            // then
            d.Count.Should().Be(0);
        }

        [Fact]
        public void PopFirst_MultipleItemInArray()
        {
            // given
            int[] arr = { 1, 2, 3, 4, 5 };
            var d = new Deque<int>(arr);

            // when
            d.PopFirst();

            // then
            d.Count.Should().Be(arr.Length - 1);
            d.PeekFirst().Should().Be(arr[1]);
        }

        [Fact]
        public void PopLast_SingleItemInArray()
        {
            // given
            int number = 5;
            var d = new Deque<int>();
            d.Push(number);

            // when
            d.PopFirst();

            // then
            d.Count.Should().Be(0);
        }

        [Fact]
        public void PopLast_MultipleItemInArray()
        {
            // given
            int[] arr = { 1, 2, 3, 4, 5 };
            var d = new Deque<int>(arr);

            // when
            d.PopLast();

            // then
            d.Count.Should().Be(arr.Length - 1);
            d.PeekLast().Should().Be(arr[3]);
        }
    }   
}
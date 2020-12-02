using Microsoft.VisualStudio.TestTools.UnitTesting;
using Skynet;
using Skynet.common;
using System.Collections.Generic;

namespace Test_Skynet
{
    [TestClass]
    public class TestDijkstra
    {
        [TestMethod]
        public void TestOneWay()
        {
            Graph graph = new Graph(5);
            graph.AddBidirectionnalLink(0, 1);
            graph.AddBidirectionnalLink(1, 2);
            graph.AddBidirectionnalLink(2, 3);
            graph.AddBidirectionnalLink(3, 4);
            var result = Dijkstra.Calculate(graph, 0, 4);
            List<int> expectedResult = new List<int>();

            expectedResult.Add(1);
            expectedResult.Add(2);
            expectedResult.Add(3);
            expectedResult.Add(4);

            Assert.AreEqual(4, result.Dist);

            for (int i = 0; i < expectedResult.Count; i++)
            {
                Assert.AreEqual(expectedResult[i], result.Path[i]);
            }
        }

        [TestMethod]
        public void TestOneWayHeavy()
        {
            Graph graph = new Graph(5);
            graph.AddBidirectionnalLink(0, 1, 2);
            graph.AddBidirectionnalLink(1, 2, 3);
            graph.AddBidirectionnalLink(2, 3, 5);
            graph.AddBidirectionnalLink(3, 4, 10);
            var result = Dijkstra.Calculate(graph, 0, 4);
            List<int> expectedResult = new List<int>();

            expectedResult.Add(1);
            expectedResult.Add(2);
            expectedResult.Add(3);
            expectedResult.Add(4);

            Assert.AreEqual(20, result.Dist);

            for (int i = 0; i < expectedResult.Count; i++)
            {
                Assert.AreEqual(expectedResult[i], result.Path[i]);
            }
        }

        [TestMethod]
        public void TestShortestWay()
        {
            Graph graph = new Graph(5);
            graph.AddBidirectionnalLink(0, 1);
            graph.AddBidirectionnalLink(1, 2);
            graph.AddBidirectionnalLink(2, 3);
            graph.AddBidirectionnalLink(3, 4);
            graph.AddBidirectionnalLink(0, 4);
            int dist;
            var result = Dijkstra.Calculate(graph, 0, 4);
            List<int> expectedResult = new List<int>();

            expectedResult.Add(4);

            Assert.AreEqual(1, result.Dist);

            for (int i = 0; i < expectedResult.Count; i++)
            {
                Assert.AreEqual(expectedResult[i], result.Path[i]);
            }
        }

        [TestMethod]
        public void TestShortestWayHeavy()
        {
            Graph graph = new Graph(5);
            graph.AddBidirectionnalLink(0, 1, 2);
            graph.AddBidirectionnalLink(1, 2, 3);
            graph.AddBidirectionnalLink(2, 3, 5);
            graph.AddBidirectionnalLink(3, 4, 10);
            graph.AddBidirectionnalLink(0, 4, 21);
            int dist;
            var result = Dijkstra.Calculate(graph, 0, 4);
            List<int> expectedResult = new List<int>();

            expectedResult.Add(1);
            expectedResult.Add(2);
            expectedResult.Add(3);
            expectedResult.Add(4);

            Assert.AreEqual(20, result.Dist);

            for (int i = 0; i < expectedResult.Count; i++)
            {
                Assert.AreEqual(expectedResult[i], result.Path[i]);
            }
        }

        [TestMethod]
        public void TestNoWay()
        {
            Graph graph = new Graph(5);
            graph.AddBidirectionnalLink(0, 1);
            graph.AddBidirectionnalLink(1, 2);
            graph.AddBidirectionnalLink(2, 3);
            var result = Dijkstra.Calculate(graph, 0, 4);
            Assert.AreEqual(-1, result.Dist);
            Assert.AreEqual(null, result.Path);
        }
    }
}

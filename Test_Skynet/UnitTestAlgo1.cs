using Microsoft.VisualStudio.TestTools.UnitTesting;
using Skynet;
using Skynet.common;
using System.Collections.Generic;

namespace Test_Skynet
{
    [TestClass]
    public class TestAlgo1
    {
        [TestMethod]
        public void TestOneExit()
        {
            Graph graph = new Graph(5);
            graph.AddBidirectionnalLink(0, 1);
            graph.AddBidirectionnalLink(1, 2);
            graph.AddBidirectionnalLink(2, 3);
            graph.AddBidirectionnalLink(3, 4);

            List<int> exits = new List<int>();
            exits.Add(4);

            Context context = new Context();
            context.Graph = graph;
            context.Exits = exits;
            context.SkynetNode = 1;

            var result = Algo1.Play(context);

            Assert.AreEqual("1 2", result);
        }

        [TestMethod]
        public void TestTwoExitOneRound()
        {
            Graph graph = new Graph(5);
            graph.AddBidirectionnalLink(0, 1);
            graph.AddBidirectionnalLink(1, 2);
            graph.AddBidirectionnalLink(2, 3);
            graph.AddBidirectionnalLink(3, 4);

            List<int> exits = new List<int>();
            exits.Add(4);
            exits.Add(0);

            Context context = new Context();
            context.Graph = graph;
            context.Exits = exits;
            context.SkynetNode = 1;

            var result = Algo1.Play(context);

            Assert.AreEqual("1 0", result);
        }

        [TestMethod]
        public void TestTwoExitTwoRound()
        {
            Graph graph = new Graph(5);
            graph.AddBidirectionnalLink(0, 1);
            graph.AddBidirectionnalLink(1, 2);
            graph.AddBidirectionnalLink(2, 3);
            graph.AddBidirectionnalLink(3, 4);

            List<int> exits = new List<int>();
            exits.Add(4);
            exits.Add(0);

            Context context = new Context();
            context.Graph = graph;
            context.Exits = exits;
            context.SkynetNode = 1;

            var result = Algo1.Play(context);
            Assert.AreEqual("1 0", result);

            context.SkynetNode = 2;
            result = Algo1.Play(context);
            Assert.AreEqual("2 3", result);

        }
    }
}

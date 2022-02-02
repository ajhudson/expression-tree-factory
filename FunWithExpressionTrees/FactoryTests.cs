using Moq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace FunWithExpressionTrees
{
    [TestFixture]
    public class FactoryTests
    {
        const string LogFileName = "C://temp//FactoryTestResults.txt";

        private IRepository _repo;

        [OneTimeSetUp]
        public void StartTest()
        {
            Trace.Listeners.Add(new ConsoleTraceListener());
        }

        [OneTimeTearDown]
        public void EndTest()
        {
            Trace.Flush();
        }

        [SetUp]
        public void Setup()
        {
            if (!File.Exists(LogFileName))
            {
                File.Create(LogFileName);
            }

            this._repo = new ConcreteRepository();
        }

        [TearDown]
        public void TearDown()
        {
            this._repo = null;
        }

        [Test]
        [TestCaseSource(nameof(TestDataProvider))]
        public void ShouldCreateInstanceWithReflection(int instanceCount)
        {
            var sw = new Stopwatch();
            sw.Start();
            var factory = new ConsumerReflectionFactory(this._repo);
            var inst = factory.CreateInstance(nameof(Consumer));
            sw.Stop();

            var consumerInstance = (Consumer)inst;
            var repoId = consumerInstance.GetRepoId();
            string reportTxt = $"Reflection ({instanceCount}) ({inst.UniqueId}) ({repoId}) : {sw.ElapsedMilliseconds} milliseconds";
            Console.WriteLine(reportTxt);
            File.AppendAllLines(LogFileName, new string[] { reportTxt });

            Assert.NotNull(inst);
            Assert.IsInstanceOf<Consumer>(inst);
        }

        [Test]
        [TestCaseSource(nameof(TestDataProvider))]
        public void ShouldCreateInstanceWithExpression(int instanceCount)
        {
            var sw = new Stopwatch();
            sw.Start();
            var factory = new ConsumerExpressionFactory(this._repo);
            var inst = factory.CreateInstance(nameof(Consumer));
            sw.Stop();

            var consumerInstance = (Consumer)inst;
            var repoId = consumerInstance.GetRepoId();
            File.AppendAllLines(LogFileName, new string[] { $"Expression ({instanceCount}) ({inst.UniqueId}) ({repoId}): {sw.ElapsedMilliseconds} milliseconds" });

            Assert.NotNull(inst);
            Assert.IsInstanceOf<Consumer>(inst);
        }

        public static IEnumerable<TestCaseData> TestDataProvider() => Enumerable.Range(1, 10).Select(idx => new TestCaseData(idx));
    }
}
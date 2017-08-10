using NUnit.Framework;
using ProgFrog.Core.Model;
using ProgFrog.Core.TaskRunning;
using ProgFrog.Core.TaskRunning.ResultsChecking;
using System.Collections.Generic;

namespace ProgFrog.Tests
{
    [TestFixture]
    public class ResultsCheckerTests
    {
        private IResultsChecker _checker;

        [SetUp]
        public void Setup()
        {
            _checker = new ResultsChecker();
        }

        [Test]
        public void TestSuccess()
        {
            var firstTestRes = "right answer";
            var prmsAndRes1 = new ParamsAndResults
            {
                Params = "test",
                Results = firstTestRes
            };
            var results = new List<RunnedTestResult>();
            var rtr1 = new RunnedTestResult
            {
                IsError = false,
                Results = firstTestRes,
                ParamsAndResults = prmsAndRes1
            };
            results.Add(rtr1);

            var checkRes = _checker.Check(results);
            var expectedCheckRes = new CheckResult
            {
                ErrorType = null,
                IsSuccessfull = true
            };

            Assert.AreEqual(expectedCheckRes, checkRes);
        }

        [Test]
        public void TestFailure()
        {
            var firstTestRes = "right answer";
            var prmsAndRes1 = new ParamsAndResults
            {
                Params = "test",
                Results = firstTestRes
            };
            var results = new List<RunnedTestResult>();
            var rtr1 = new RunnedTestResult
            {
                IsError = false,
                Results = "wrong answer",
                ParamsAndResults = prmsAndRes1
            };
            results.Add(rtr1);

            var checkRes = _checker.Check(results);
            var expectedCheckRes = new CheckResult
            {
                ErrorType = ResultFailureType.WrongResults,
                IsSuccessfull = false
            };

            Assert.AreEqual(expectedCheckRes, checkRes);
        }

        [Test]
        public void TestMultipleSuccess()
        {
            var firstTestRes = "right answer";
            var prmsAndRes1 = new ParamsAndResults
            {
                Params = "test",
                Results = firstTestRes
            };
            var secondTestRes = "right answer 2";
            var prmsAndRes2 = new ParamsAndResults
            {
                Params = "test 2",
                Results = secondTestRes
            };
            var results = new List<RunnedTestResult>();
            var rtr1 = new RunnedTestResult
            {
                IsError = false,
                Results = firstTestRes,
                ParamsAndResults = prmsAndRes1
            };
            var rtr2 = new RunnedTestResult
            {
                IsError = false,
                Results = secondTestRes,
                ParamsAndResults = prmsAndRes2
            };
            results.Add(rtr1);
            results.Add(rtr2);

            var checkRes = _checker.Check(results);
            var expectedCheckRes = new CheckResult
            {
                ErrorType = null,
                IsSuccessfull = true
            };

            Assert.AreEqual(expectedCheckRes, checkRes);
        }

        [Test]
        public void TestMultipleFaulureSingle()
        {
            var firstTestRes = "right answer";
            var prmsAndRes1 = new ParamsAndResults
            {
                Params = "test",
                Results = firstTestRes
            };
            var secondTestRes = "right answer 2";
            var prmsAndRes2 = new ParamsAndResults
            {
                Params = "test 2",
                Results = secondTestRes
            };
            var results = new List<RunnedTestResult>();
            var rtr1 = new RunnedTestResult
            {
                IsError = false,
                Results = firstTestRes,
                ParamsAndResults = prmsAndRes1
            };
            var rtr2 = new RunnedTestResult
            {
                IsError = false,
                Results = "wrong results",
                ParamsAndResults = prmsAndRes2
            };
            results.Add(rtr1);
            results.Add(rtr2);

            var checkRes = _checker.Check(results);
            var expectedCheckRes = new CheckResult
            {
                ErrorType = ResultFailureType.WrongResults,
                IsSuccessfull = false
            };

            Assert.AreEqual(expectedCheckRes, checkRes);
        }
    }
}

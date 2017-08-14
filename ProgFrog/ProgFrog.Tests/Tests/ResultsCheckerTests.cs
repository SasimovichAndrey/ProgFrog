using NUnit.Framework;
using ProgFrog.Core.TaskRunning.ResultsChecking;
using ProgFrog.Interface.Model;
using ProgFrog.Interface.TaskRunning;
using ProgFrog.Interface.TaskRunning.ResultsChecking;
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
                Params = new List<string>() { "test" },
                Results = firstTestRes
            };
            var results = new List<RunnedTestResult>();
            var runnedTestsResult = new RunnedTestResult
            {
                IsError = false,
                Results = firstTestRes,
                ParamsAndResults = prmsAndRes1
            };
            results.Add(runnedTestsResult);
            var expectedCheckRes = new CheckResult
            {
                ErrorType = null,
                IsSuccessfull = true
            };

            var checkRes = _checker.Check(results);

            Assert.AreEqual(expectedCheckRes, checkRes);
        }

        [Test]
        public void TestFailure()
        {
            var firstTestRes = "right answer";
            var prmsAndRes1 = new ParamsAndResults
            {
                Params = new List<string>() { "test" },
                Results = firstTestRes
            };
            var results = new List<RunnedTestResult>();
            var firstRunnedTestResult = new RunnedTestResult
            {
                IsError = false,
                Results = "wrong answer",
                ParamsAndResults = prmsAndRes1
            };
            results.Add(firstRunnedTestResult);

            var expectedCheckRes = new CheckResult
            {
                ErrorType = ResultFailureType.WrongResults,
                IsSuccessfull = false
            };

            var checkRes = _checker.Check(results);

            Assert.AreEqual(expectedCheckRes, checkRes);
        }

        [Test]
        public void TestMultipleSuccess()
        {
            var firstTestRes = "right answer";
            var prmsAndRes1 = new ParamsAndResults
            {
                Params = new List<string>() { "test" },
                Results = firstTestRes
            };
            var secondTestRes = "right answer 2";
            var prmsAndRes2 = new ParamsAndResults
            {
                Params = new List<string>() { "test 2" },
                Results = secondTestRes
            };
            var results = new List<RunnedTestResult>();
            var firstRunnedTestResults = new RunnedTestResult
            {
                IsError = false,
                Results = firstTestRes,
                ParamsAndResults = prmsAndRes1
            };
            var secondRunnedTestResult = new RunnedTestResult
            {
                IsError = false,
                Results = secondTestRes,
                ParamsAndResults = prmsAndRes2
            };
            results.Add(firstRunnedTestResults);
            results.Add(secondRunnedTestResult);

            var expectedCheckRes = new CheckResult
            {
                ErrorType = null,
                IsSuccessfull = true
            };

            var checkRes = _checker.Check(results);
            
            Assert.AreEqual(expectedCheckRes, checkRes);
        }

        [Test]
        public void TestMultipleFaulureSingle()
        {
            var firstTestRes = "right answer";
            var prmsAndRes1 = new ParamsAndResults
            {
                Params = new List<string>() { "test" },
                Results = firstTestRes
            };
            var secondTestRes = "right answer 2";
            var prmsAndRes2 = new ParamsAndResults
            {
                Params = new List<string>() { "test 2" },
                Results = secondTestRes
            };
            var results = new List<RunnedTestResult>();
            var firstRunnedTestResult = new RunnedTestResult
            {
                IsError = false,
                Results = firstTestRes,
                ParamsAndResults = prmsAndRes1
            };
            var secondRunnedTestResult = new RunnedTestResult
            {
                IsError = false,
                Results = "wrong results",
                ParamsAndResults = prmsAndRes2
            };
            results.Add(firstRunnedTestResult);
            results.Add(secondRunnedTestResult);

            var expectedCheckRes = new CheckResult
            {
                ErrorType = ResultFailureType.WrongResults,
                IsSuccessfull = false
            };

            var checkRes = _checker.Check(results);

            Assert.AreEqual(expectedCheckRes, checkRes);
        }
    }
}

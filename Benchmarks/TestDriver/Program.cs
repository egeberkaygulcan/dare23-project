﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Coyote;
using Microsoft.Coyote.Actors;
using Microsoft.Coyote.Logging;
using Microsoft.Coyote.Runtime;
using Microsoft.Coyote.SystematicTesting;

namespace TestDriver
{
    public static class Program
    {
        public static void Main()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            var configuration = Configuration.Create().WithTestingIterations(10000)
                 .WithMaxSchedulingSteps(500);
            // configuration.WithQLearningStrategy();
            // configuration.WithPrioritizationStrategy();
            configuration.WithTestIterationsRunToCompletion();
            configuration.WithSystematicFuzzingEnabled();
            configuration.WithVerbosityEnabled(VerbosityLevel.Info);
            configuration.WithTelemetryEnabled(false);
            configuration.WithConsoleLoggingEnabled(false);

            RunTest(ReliableBroadcast.Program.Execute, configuration, "ReliableBroadcast", new[] { "DuplicateException", "MessageMatchException" });

            // RunTest(TwoPhaseCommit.Program.Execute, configuration,
            //      "TwoPhaseCommit");

            // RunTest(Microsoft.Coyote.Samples.CloudMessaging.Raft.Mocking.Program.Execute, configuration,
            //    "CloudMessaging.TestWithMocking");

            // RunTest(Microsoft.Coyote.Samples.CloudMessaging.Raft.Nondeterminism.Program.Execute, configuration,
            //    "CloudMessaging.RaftTestScenarioWithFailure");

            stopWatch.Stop();
            Console.WriteLine($"Done testing in {stopWatch.ElapsedMilliseconds}ms. All expected bugs found.");
        }

        private static void RunTest(Action test, Configuration configuration, string testName,
            params string[] expectedBugs)
        {
            var engine = TestingEngine.Create(configuration, test);
            RunTest(engine, testName, expectedBugs);
        }

        private static void RunTest(Func<Task> test, Configuration configuration, string testName,
            params string[] expectedBugs)
        {
            var engine = TestingEngine.Create(configuration, test);
            RunTest(engine, testName, expectedBugs);
        }

        private static void RunTest(Func<ICoyoteRuntime, Task> test, Configuration configuration, string testName,
            params string[] expectedBugs)
        {
            var engine = TestingEngine.Create(configuration, test);
            RunTest(engine, testName, expectedBugs);
        }

        private static void RunTest(Action<IActorRuntime> test, Configuration configuration, string testName,
            params string[] expectedBugs)
        {
            var engine = TestingEngine.Create(configuration, test);
            RunTest(engine, testName, expectedBugs);
        }

        private static void RunTest(TestingEngine engine, string testName, string[] expectedBugs)
        {
            Console.WriteLine($"Starting to test '{testName}'.");
            engine.Run();
            Console.WriteLine($"Done testing '{testName}'. Found {engine.TestReport.NumOfFoundBugs} bugs.");

            foreach (var key in engine.TestReport.BugMap.Keys)
            {
                Console.WriteLine($"Found {key}, {engine.TestReport.BugMap[key]} times.");
            }

            if (expectedBugs.Length > 0 && engine.TestReport.NumOfFoundBugs == 0)
            {
                foreach (var expectedBug in expectedBugs)
                {
                    Console.WriteLine($"Expected bug '{expectedBug}' not found.");
                }

                // Environment.Exit(1);
            }
            else if (expectedBugs.Length > 0 && engine.TestReport.NumOfFoundBugs > 0)
            {
                foreach (var actualBug in engine.TestReport.BugReports)
                {
                    bool isFound = false;
                    // var actualBug = engine.TestReport.BugReports.First();
                    foreach (var expectedBug in expectedBugs)
                    {
                        if (actualBug.Contains(expectedBug))
                        {
                            isFound = true;
                            break;
                        }
                    }

                    if (!isFound)
                    {
                        foreach (var expectedBug in expectedBugs)
                        {
                            Console.WriteLine($"Found '{actualBug}' bug instead of the expected bug '{expectedBug}'.");
                        }

                        // Environment.Exit(1);
                    }

                    Console.WriteLine($"Found expected '{actualBug}' bug.");
                }
            }
            else if (engine.TestReport.NumOfFoundBugs > 0)
            {
                Console.WriteLine($"Unexpected '{engine.TestReport.BugReports.First()}' bug found.");
                // Environment.Exit(1);
            }

            Console.WriteLine(engine.GetReport());
            Environment.Exit(1);
        }
    }
}

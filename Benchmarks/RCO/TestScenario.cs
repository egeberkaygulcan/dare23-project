// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

// Coyote benchmark: TwoPhase Commit

using System;
using System.Collections.Generic;
using Microsoft.Coyote.Actors;

namespace ReliableBroadcast;

public class TestScenario
{
    #pragma warning disable CA1822
    public void RunTest(IActorRuntime runtime, int numP, int numBroadcasts)
    #pragma warning restore CA1822
    {
        // Create processes
        List<ActorId> processes = new List<ActorId>();
        for (int processId = 0; processId < numP; processId++)
        {
            ActorId actorId = runtime.CreateActor(typeof(Process));
            processes.Add(actorId);
        }

        // Pass the peers to processes
        for (int i = 0; i < processes.Count; i++)
        {
            for (int j = 0; j < processes.Count; j++)
            {
                runtime.SendEvent(processes[i], new RegisterProcessEvent() { ProcessId = processes[j] });
            }
        }

        runtime.CreateActor(typeof(Client), new Client.SetupEvent(processes, numBroadcasts));

        // var random = new Random();
        // for (int i = 0; i < numBroadcasts; i++)
        // {
        //     int index = random.Next(processes.Count);
        //     runtime.SendEvent(processes[index], new BroadcastRequestEvent() { Message = $"message-{i}" });
        // }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Coyote.Actors;

namespace ReliableBroadcast;

public class Process : StateMachine
{
    private List<int> delivered = new List<int>();

    private List<ActorId> Processes;

    private static int _counter = 0;

    [Start]
    private class Init : State { }
    private class Working : State { }

    private class Crashed : State { }

    private void BebBroadcast(Event e) {
        var msg = e as MessageEvent;
        msg.Sender = this.Id;
        foreach (var process in Processes)
        {
            this.SendEvent(process, msg)
        }
    }
    private void RbBroadcast() {
        int MsgId = Interlocked.Increment(ref _counter);

    }

    public virtual async void RegisterProcess(Event e) {
        var reg = e as RegisterProcessEvent;
        if (!this.Processes.Contains(reg.ProcessId))
        {
            this.Processes.Add(reg.ProcessId);
            this.Logger.WriteLine("Registered process: " + reg.ProcessId);
        }
    }
}
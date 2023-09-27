// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

// Coyote benchmark: Reliable Broadcast

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Coyote.Actors;

namespace ReliableBroadcast;

public class Process : StateMachine
{
    private List<int> delivered = new List<int>();

    private List<ActorId> Processes = new List<ActorId>();

    public List<string> Messages = new List<string>();

    private static int Counter = 0;

    [Start]
    [OnEventDoAction(typeof(RegisterProcessEvent), nameof(RegisterProcess))]
    [OnEventDoAction(typeof(BroadcastRequestEvent), nameof(RbBroadcast))]
    [OnEventDoAction(typeof(RbBroadcastEvent), nameof(BebBroadcast))]
    [OnEventDoAction(typeof(SingleMessageEvent), nameof(SendSingleEvent))]
    [OnEventDoAction(typeof(MessageEvent), nameof(BebDeliver))]
    [OnEventDoAction(typeof(CrashEvent), nameof(HandleCrash))]
    [OnEventDoAction(typeof(GetMessagesEvent), nameof(HandleGetMessages))]
    private class Working : State { }

    [OnEventDoAction(typeof(GetMessagesEvent), nameof(HandleGetMessages))]
    private class Crashed : State { }

    private void BebBroadcast(Event e)
    {
        var msg = e as RbBroadcastEvent;
        msg.MsgEvent.Sender = this.Id;
        foreach (var process in this.Processes)
        {
            msg.MsgEvent.Receiver = process;
            this.SendEvent(this.Id, new SingleMessageEvent(msg.MsgEvent));
        }
    }

    private void SendSingleEvent(Event e)
    {
        var msg = e as SingleMessageEvent;
        this.SendEvent(msg.MsgEvent.Receiver, msg.MsgEvent);
    }

    public void RbBroadcast(Event e)
    {
        var msgEvent = e as BroadcastRequestEvent;
        int msgId = Interlocked.Increment(ref Counter);
        MessageEvent msg = new MessageEvent(msgId, this.Id, msgEvent.Message, null);
        this.SendEvent(this.Id, new RbBroadcastEvent(msg));
    }

    private void BebDeliver(Event e)
    {
        var msg = e as MessageEvent;
        if (!this.delivered.Contains(msg.MessageId))
        {
            this.delivered.Add(msg.MessageId);
            this.RbDeliver(msg.Message);

            msg.Sender = this.Id;
            this.BebBroadcast(msg);
        }
    }

    private void RbDeliver(string msg)
    {
        this.Messages.Add(msg);
    }

    public void RegisterProcess(Event e)
    {
        var reg = e as RegisterProcessEvent;
        if (!this.Processes.Contains(reg.ProcessId))
        {
            this.Processes.Add(reg.ProcessId);
            this.Logger.WriteLine("Registered process: " + reg.ProcessId);
        }
    }

    private void HandleCrash ()
    {
        this.RaiseGotoStateEvent<Crashed>();
    }

    private void HandleGetMessages(Event e)
    {
        var req = e as GetMessagesEvent;
        this.SendEvent(req.Sender, new GetMessagesResponseEvent(this.Messages));
    }
}

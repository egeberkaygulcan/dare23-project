﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.Coyote.Actors.Tests
{
    public class OnExceptionTests : BaseActorTest
    {
        public OnExceptionTests(ITestOutputHelper output)
            : base(output)
        {
        }

        private class E : Event
        {
            public int X;
            public TaskCompletionSource<bool> Tcs;

            public E(TaskCompletionSource<bool> tcs)
            {
                this.X = 0;
                this.Tcs = tcs;
            }
        }

        private class F : Event
        {
        }

        private class M1a : StateMachine
        {
            private E Event;

            [Start]
            [OnEntry(nameof(InitOnEntry))]
            [OnEventDoAction(typeof(F), nameof(OnF))]
            private class Init : State
            {
            }

            private void InitOnEntry(Event e)
            {
                this.Event = e as E;
                throw new NotImplementedException();
            }

            private void OnF()
            {
                this.Event.Tcs.SetResult(true);
            }

            protected override OnExceptionOutcome OnException(Exception ex, string methodName, Event e)
            {
                this.Event.X++;
                return OnExceptionOutcome.HandledException;
            }
        }

        [Fact(Timeout = 5000)]
        public async Task TestOnExceptionCalledOnce1()
        {
            await this.RunAsync(async r =>
            {
                var failed = false;
                var tcs = new TaskCompletionSource<bool>();
                r.OnFailure += (ex) =>
                {
                    // This should not be called because M1a returns OnExceptionOutcome.HandledException
                    failed = true;
                };

                var e = new E(tcs);
                var m = r.CreateActor(typeof(M1a), e);
                r.SendEvent(m, new F());

                await this.WaitAsync(tcs.Task);
                Assert.False(failed);
                Assert.True(e.X is 1);
            },
            handleFailures: false);
        }

        private class M1b : StateMachine
        {
            [Start]
            [OnEntry(nameof(InitOnEntry))]
            private class Init : State
            {
            }

            private void InitOnEntry()
            {
                throw new NotImplementedException();
            }

            protected override OnExceptionOutcome OnException(Exception ex, string methodName, Event e)
            {
                (e as E).X++;
                return OnExceptionOutcome.ThrowException;
            }
        }

        [Fact(Timeout = 5000)]
        public async Task TestOnExceptionCalledOnce2()
        {
            await this.RunAsync(async r =>
            {
                var failed = false;
                var tcs = new TaskCompletionSource<bool>();
                r.OnFailure += (ex) =>
                {
                    failed = true;
                    tcs.SetResult(true);
                };

                var e = new E(tcs);
                r.CreateActor(typeof(M1b), e);

                await this.WaitAsync(tcs.Task);
                Assert.True(failed);
                Assert.True(e.X is 1);
            },
            handleFailures: false);
        }

        private class M2a : StateMachine
        {
            private E Event;

            [Start]
            [OnEntry(nameof(InitOnEntry))]
            [OnEventDoAction(typeof(F), nameof(OnF))]
            private class Init : State
            {
            }

            private async Task InitOnEntry(Event e)
            {
                await Task.CompletedTask;
                this.Event = e as E;
                throw new NotImplementedException();
            }

            private void OnF()
            {
                this.Event.Tcs.SetResult(true);
            }

            protected override OnExceptionOutcome OnException(Exception ex, string methodName, Event e)
            {
                (e as E).X++;
                return OnExceptionOutcome.HandledException;
            }
        }

        [Fact(Timeout = 5000)]
        public async Task TestOnExceptionCalledOnceAsync1()
        {
            await this.RunAsync(async r =>
            {
                var failed = false;
                var tcs = new TaskCompletionSource<bool>();
                r.OnFailure += (ex) =>
                {
                    // This should not be called, because M2a returns OnExceptionOutcome.HandledException.
                    failed = true;
                };

                var e = new E(tcs);
                var m = r.CreateActor(typeof(M2a), e);
                r.SendEvent(m, new F());

                await this.WaitAsync(tcs.Task);
                Assert.False(failed);
                Assert.True(e.X is 1);
            },
            handleFailures: false);
        }

        private class M2b : StateMachine
        {
            [Start]
            [OnEntry(nameof(InitOnEntry))]
            private class Init : State
            {
            }

#pragma warning disable CA1822 // Mark members as static
            private async Task InitOnEntry()
#pragma warning restore CA1822 // Mark members as static
            {
                await Task.CompletedTask;
                throw new NotImplementedException();
            }

            protected override OnExceptionOutcome OnException(Exception ex, string methodName, Event e)
            {
                (e as E).X++;
                return OnExceptionOutcome.ThrowException;
            }
        }

        [Fact(Timeout = 5000)]
        public async Task TestOnExceptionCalledOnceAsync2()
        {
            await this.RunAsync(async r =>
            {
                var failed = false;
                var tcs = new TaskCompletionSource<bool>();
                r.OnFailure += (ex) =>
                {
                    failed = true;
                    tcs.SetResult(true);
                };

                var e = new E(tcs);
                r.CreateActor(typeof(M2b), e);

                await this.WaitAsync(tcs.Task);
                Assert.True(failed);
                Assert.True(e.X is 1);
            },
            handleFailures: false);
        }

        private class M3 : StateMachine
        {
            [Start]
            [OnEntry(nameof(InitOnEntry))]
            private class Init : State
            {
            }

            private void InitOnEntry()
            {
                throw new NotImplementedException();
            }

            protected override OnExceptionOutcome OnException(Exception ex, string methodName, Event e)
            {
                return OnExceptionOutcome.Halt;
            }

            protected override Task OnHaltAsync(Event e)
            {
                (e as E).Tcs.TrySetResult(true);
                return Task.CompletedTask;
            }
        }

        [Fact(Timeout = 5000)]
        public async Task TestOnExceptionCanHalt()
        {
            await this.RunAsync(async r =>
            {
                var failed = false;
                var tcs = new TaskCompletionSource<bool>();
                r.OnFailure += (ex) =>
                {
                    failed = true;
                    tcs.TrySetResult(false);
                };

                var e = new E(tcs);
                r.CreateActor(typeof(M3), e);

                var result = await this.GetResultAsync(tcs);
                Assert.True(result);
                Assert.False(failed);
            },
            handleFailures: false);
        }

        private class M4 : StateMachine
        {
            private E Event;

            [Start]
            [OnEntry(nameof(InitOnEntry))]
            private class Init : State
            {
            }

            private void InitOnEntry(Event e)
            {
                this.Event = e as E;
            }

            protected override OnExceptionOutcome OnException(Exception ex, string methodName, Event e)
            {
                if (ex is UnhandledEventException)
                {
                    return OnExceptionOutcome.Halt;
                }

                return OnExceptionOutcome.ThrowException;
            }

            protected override Task OnHaltAsync(Event e)
            {
                this.Assert(e is F);
                this.Event.Tcs.TrySetResult(true);
                return Task.CompletedTask;
            }
        }

        [Fact(Timeout = 5000)]
        public async Task TestUnhandledEventCanHalt()
        {
            await this.RunAsync(async r =>
            {
                var failed = false;
                var tcs = new TaskCompletionSource<bool>();
                r.OnFailure += (ex) =>
                {
                    failed = true;
                    tcs.TrySetResult(false);
                };

                var e = new E(tcs);
                var m = r.CreateActor(typeof(M4), e);
                r.SendEvent(m, new F());

                var result = await this.GetResultAsync(tcs);
                Assert.True(result);
                Assert.False(failed);
            },
            handleFailures: false);
        }
    }
}

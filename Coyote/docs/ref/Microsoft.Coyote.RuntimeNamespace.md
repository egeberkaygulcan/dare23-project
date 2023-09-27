## Microsoft.Coyote.Runtime namespace

| public type | description |
| --- | --- |
| class [AssertionFailureException](./Microsoft.Coyote.Runtime/AssertionFailureException.md) | The exception that is thrown by the Coyote runtime upon assertion failure. |
| interface [ICoyoteRuntime](./Microsoft.Coyote.Runtime/ICoyoteRuntime.md) | Interface that exposes base runtime methods for Coyote. |
| interface [IOperationBuilder](./Microsoft.Coyote.Runtime/IOperationBuilder.md) | Interface of a controlled operation builder. |
| interface [IRuntimeExtension](./Microsoft.Coyote.Runtime/IRuntimeExtension.md) | Interface for a Coyote runtime extension. |
| interface [IRuntimeLog](./Microsoft.Coyote.Runtime/IRuntimeLog.md) | Interface that allows an external module to track what is happening in the [`ICoyoteRuntime`](./Microsoft.Coyote.Runtime/ICoyoteRuntime.md). |
| delegate [OnFailureHandler](./Microsoft.Coyote.Runtime/OnFailureHandler.md) | Handles the [`OnFailure`](./Microsoft.Coyote.Runtime/ICoyoteRuntime/OnFailure.md) event. |
| static class [Operation](./Microsoft.Coyote.Runtime/Operation.md) | Provides a set of static methods for instrumenting concurrency primitives that can then be controlled during testing. |
| class [RuntimeException](./Microsoft.Coyote.Runtime/RuntimeException.md) | An exception that is thrown by the Coyote runtime. |
| class [RuntimeLogTextFormatter](./Microsoft.Coyote.Runtime/RuntimeLogTextFormatter.md) | This class implements [`IRuntimeLog`](./Microsoft.Coyote.Runtime/IRuntimeLog.md) and generates output in a a human readable text format. |
| static class [RuntimeProvider](./Microsoft.Coyote.Runtime/RuntimeProvider.md) | Provides methods for creating or accessing a [`ICoyoteRuntime`](./Microsoft.Coyote.Runtime/ICoyoteRuntime.md) runtime. |
| static class [SchedulingPoint](./Microsoft.Coyote.Runtime/SchedulingPoint.md) | Provides a set of static methods for declaring points in the execution where interleavings between operations should be explored during testing. |
| static class [TaskServices](./Microsoft.Coyote.Runtime/TaskServices.md) | Provides methods for interacting with tasks using the runtime. |

<!-- DO NOT EDIT: generated by xmldocmd for Microsoft.Coyote.dll -->
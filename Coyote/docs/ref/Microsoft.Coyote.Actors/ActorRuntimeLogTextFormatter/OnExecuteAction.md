# ActorRuntimeLogTextFormatter.OnExecuteAction method

Invoked when the specified actor executes an action.

```csharp
public virtual void OnExecuteAction(ActorId id, string handlingStateName, string currentStateName, 
    string actionName)
```

| parameter | description |
| --- | --- |
| id | The id of the actor executing the action. |
| handlingStateName | The state that declared this action (can be different from currentStateName in the case of pushed states. |
| currentStateName | The state name, if the actor is a state machine and a state exists, else null. |
| actionName | The name of the action being executed. |

## See Also

* class [ActorId](../ActorId.md)
* class [ActorRuntimeLogTextFormatter](../ActorRuntimeLogTextFormatter.md)
* namespace [Microsoft.Coyote.Actors](../ActorRuntimeLogTextFormatter.md)
* assembly [Microsoft.Coyote.Actors](../../Microsoft.Coyote.Actors.md)

<!-- DO NOT EDIT: generated by xmldocmd for Microsoft.Coyote.Actors.dll -->

# ActorRuntimeLogTextFormatter.OnStateTransition method

Invoked when the specified state machine enters or exits a state.

```csharp
public virtual void OnStateTransition(ActorId id, string stateName, bool isEntry)
```

| parameter | description |
| --- | --- |
| id | The id of the actor entering or exiting the state. |
| stateName | The name of the state being entered or exited. |
| isEntry | If true, this is called for a state entry; otherwise, exit. |

## See Also

* class [ActorId](../ActorId.md)
* class [ActorRuntimeLogTextFormatter](../ActorRuntimeLogTextFormatter.md)
* namespace [Microsoft.Coyote.Actors](../ActorRuntimeLogTextFormatter.md)
* assembly [Microsoft.Coyote.Actors](../../Microsoft.Coyote.Actors.md)

<!-- DO NOT EDIT: generated by xmldocmd for Microsoft.Coyote.Actors.dll -->
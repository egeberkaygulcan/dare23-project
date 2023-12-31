# IActorRuntimeLog.OnCreateStateMachine method

Invoked when the specified state machine has been created.

```csharp
public void OnCreateStateMachine(ActorId id, string creatorName, string creatorType)
```

| parameter | description |
| --- | --- |
| id | The id of the state machine that has been created. |
| creatorName | The name of the creator, or null. |
| creatorType | The type of the creator, or null. |

## See Also

* class [ActorId](../ActorId.md)
* interface [IActorRuntimeLog](../IActorRuntimeLog.md)
* namespace [Microsoft.Coyote.Actors](../IActorRuntimeLog.md)
* assembly [Microsoft.Coyote.Actors](../../Microsoft.Coyote.Actors.md)

<!-- DO NOT EDIT: generated by xmldocmd for Microsoft.Coyote.Actors.dll -->

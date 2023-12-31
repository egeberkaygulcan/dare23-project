# Specification.RegisterStateHashingFunction method

Registers a new state hashing function that contributes to computing a representation of the program state in each scheduling step.

```csharp
public static void RegisterStateHashingFunction(Func<int> func)
```

| parameter | description |
| --- | --- |
| func | The state hashing function. |

## Remarks

If you register more than one state hashing function per iteration, the runtime will aggregate the hashes computed from each function.

## See Also

* class [Specification](../Specification.md)
* namespace [Microsoft.Coyote.Specifications](../Specification.md)
* assembly [Microsoft.Coyote](../../Microsoft.Coyote.md)

<!-- DO NOT EDIT: generated by xmldocmd for Microsoft.Coyote.dll -->

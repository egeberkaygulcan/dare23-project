# IOperationBuilder.HashedStateCallback property

Optional callback that returns the hashed state of the operation being built. If provided, it can be used by the test engine to optimize exploration.

```csharp
public Func<int> HashedStateCallback { get; }
```

## See Also

* interface [IOperationBuilder](../IOperationBuilder.md)
* namespace [Microsoft.Coyote.Runtime](../IOperationBuilder.md)
* assembly [Microsoft.Coyote](../../Microsoft.Coyote.md)

<!-- DO NOT EDIT: generated by xmldocmd for Microsoft.Coyote.dll -->

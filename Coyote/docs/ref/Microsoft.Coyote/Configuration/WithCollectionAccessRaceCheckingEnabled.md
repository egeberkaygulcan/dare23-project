# Configuration.WithCollectionAccessRaceCheckingEnabled method

Updates the configuration with race checking for collection accesses enabled or disabled. If this race checking strategy is enabled, then the runtime will explore interleavings when concurrent operations try to access collections.

```csharp
public Configuration WithCollectionAccessRaceCheckingEnabled(bool isEnabled = true)
```

| parameter | description |
| --- | --- |
| isEnabled | If true, then checking races at collection accesses is enabled. |

## See Also

* class [Configuration](../Configuration.md)
* namespace [Microsoft.Coyote](../Configuration.md)
* assembly [Microsoft.Coyote](../../Microsoft.Coyote.md)

<!-- DO NOT EDIT: generated by xmldocmd for Microsoft.Coyote.dll -->

namespace Common;

public class InstanceIdentifierProvider : IInstanceIdentifierProvider
{
    private static readonly Guid _guid = Guid.NewGuid();

    public Guid GetIdentifier() => _guid;
}

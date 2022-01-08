namespace Common;

public class InstanceIdentifierProvider : IInstanceIdentifierProvider
{
    private static readonly Guid _guid = new Guid();

    public Guid GetIdentifier() => _guid;
}

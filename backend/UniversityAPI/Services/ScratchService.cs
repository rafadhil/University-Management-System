public class ScratchService
{
    public enum Permission
    {
        None = 0,
        Read = 1 << 0, //0010
        Create = 1 << 1,
        Update = 1 << 2,
        Delete = 1 << 3
    }
}
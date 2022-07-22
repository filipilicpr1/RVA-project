namespace Server.Interfaces.Prototype
{
    public interface IDeepCloneable<T> where T : class
    {
        T DeepCopy();
    }
}

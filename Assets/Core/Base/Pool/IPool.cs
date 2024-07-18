namespace Core.Base.Pool
{
    public interface IPool<T>
    {
        void Push(T value);
        T Pop();
    }
}
namespace Core.Base.Factory
{
    public interface IFactory<out T>
    {
        T Create();
    }
}
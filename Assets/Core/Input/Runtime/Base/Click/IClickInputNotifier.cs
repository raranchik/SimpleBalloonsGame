namespace Core.Input.Base.Click
{
    public interface IClickInputNotifier
    {
        public void AddClickListener(IClickInputListener listener);
        public void RemoveClickListener(IClickInputListener listener);
    }
}
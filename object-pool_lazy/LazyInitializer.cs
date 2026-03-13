namespace object_pool_lazy;

public class LazyInitializer<T>
{
    private T _value;
    private bool _isInitialized = false;
    private Func<T> _factory;

    public LazyInitializer(Func<T> factory)
    {
        _factory = factory;
    }

    public T Value
    {
        get
        {
            if (!_isInitialized)
            {
                _value = _factory();
                _isInitialized = true;
            }

            return _value;
        }
    }
    
    public bool IsInitialized =>  _isInitialized;
}
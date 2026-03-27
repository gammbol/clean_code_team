namespace object_pool_lazy;

public class ConnectionPool
{
    private ObjectPool<Connection> _pool;
    private int _counter = 0;

    public ConnectionPool(int size)
    {
        _pool = new ObjectPool<Connection>(() =>
        {
            _counter++;
            return new Connection(_counter);
        }, size, size);
    }

    public Connection Acquire()
    {
        var connection = _pool.GetObject();
        connection.IsBusy = true;
        connection.Open();
        return connection;
    }

    public void Release(Connection connection)
    {
        connection.IsBusy = false;
        connection.Close();
        _pool.ReturnObject(connection);
    }
}
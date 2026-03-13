namespace object_pool_lazy;

public class RequestHandler
{
    private readonly ConnectionPool _pool;
    
    public  RequestHandler(ConnectionPool pool)
    {
        _pool = pool;
    }

    public void HandleRequest(string query)
    {
        var connection = _pool.Acquire();

        try
        {
            connection.ExecuteQuery(query);
        }
        finally
        {
            _pool.Release(connection);
        }
    }
}
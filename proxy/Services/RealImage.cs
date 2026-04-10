using proxy.Infrastructure;

namespace proxy.Services;

public class RealImage : IImage
{
    private readonly string _filePath;
    private readonly byte[] _data;
    private readonly Logger _logger;

    public RealImage(string filePath)
    {
        _filePath = filePath;
        _logger = new Logger();

        var loader = new ImageLoader();
        _data = loader.Load(filePath);
    }

    public void Display()
    {
        _logger.Info($"Отображение изображения: {_filePath}");
    }
}
using proxy.Infrastructure;

namespace proxy.Services;

public class ImageProxy : IImage
{
    private readonly string _filePath;
    private RealImage? _realImage;
    private readonly Logger _logger;

    public ImageProxy(string filePath)
    {
        _filePath = filePath;
        _logger = new Logger();
    }

    public void Display()
    {
        if (_realImage == null)
        {
            _logger.Info($"Lazy загрузка изображения: {_filePath}");
            _realImage = new RealImage(_filePath);
        }

        _realImage.Display();
    }
}
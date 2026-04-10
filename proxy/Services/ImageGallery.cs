using proxy.Models;

namespace proxy.Services;

public class ImageGallery
{
    private readonly List<IImage> _images = new();

    public void AddImage(ImageInfo info)
    {
        _images.Add(new ImageProxy(info.FilePath));
    }

    public void ShowAll()
    {
        Console.WriteLine("\n=== Галерея (ленивая загрузка) ===");

        foreach (var image in _images)
        {
            image.Display();
        }
    }
}
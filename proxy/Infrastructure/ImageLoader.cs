using System;

namespace proxy.Infrastructure;

public class ImageLoader
{
    public byte[] Load(string path)
    {
        Console.WriteLine($"[Loader] Загрузка изображения с диска: {path}");

        System.Threading.Thread.Sleep(500);

        return new byte[1024];
    }
}
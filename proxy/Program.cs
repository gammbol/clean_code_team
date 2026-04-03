using System;
using proxy.Models;
using proxy.Services;

namespace proxy;

public class Program
{
    public static void Main(string[] args)
    {
        var gallery = new ImageGallery();

        // Добавляем изображения (но НЕ загружаем их)
        gallery.AddImage(new ImageInfo { FilePath = "img1.jpg", Title = "Mountains" });
        gallery.AddImage(new ImageInfo { FilePath = "img2.jpg", Title = "Ocean" });
        gallery.AddImage(new ImageInfo { FilePath = "img3.jpg", Title = "Forest" });

        Console.WriteLine("Галерея создана. Изображения еще не загружены.\n");

        // Загрузка происходит ТОЛЬКО здесь
        gallery.ShowAll();

        Console.WriteLine("\nПовторный показ (без повторной загрузки):");
        gallery.ShowAll();
    }
}
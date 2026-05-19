using SkiaSharp;

namespace dotnet_store.Services;

public class ImageService
{
    private readonly string _uploadPath;

    public ImageService(IWebHostEnvironment env)
    {
        _uploadPath = Path.Combine(env.WebRootPath, "img");
    }

    public async Task<string> SaveAsync(IFormFile file, int maxWidth, int maxHeight)
    {
        var fileName = Path.GetRandomFileName() + ".webp";
        var filePath = Path.Combine(_uploadPath, fileName);

        using var ms = new MemoryStream();
        await file.CopyToAsync(ms);
        ms.Position = 0;

        using var original = SKBitmap.Decode(ms);

        var (width, height) = CalculateSize(original.Width, original.Height, maxWidth, maxHeight);

        using var resized = original.Resize(new SKImageInfo(width, height), SKFilterQuality.High);
        using var image = SKImage.FromBitmap(resized);
        using var data = image.Encode(SKEncodedImageFormat.Webp, 82);
        await File.WriteAllBytesAsync(filePath, data.ToArray());

        return fileName;
    }

    public void Delete(string? fileName)
    {
        if (string.IsNullOrEmpty(fileName)) return;
        var path = Path.Combine(_uploadPath, fileName);
        if (File.Exists(path)) File.Delete(path);
    }

    private static (int width, int height) CalculateSize(int srcW, int srcH, int maxW, int maxH)
    {
        if (srcW <= maxW && srcH <= maxH) return (srcW, srcH);

        var ratioW = (double)maxW / srcW;
        var ratioH = (double)maxH / srcH;
        var ratio = Math.Min(ratioW, ratioH);

        return ((int)(srcW * ratio), (int)(srcH * ratio));
    }
}

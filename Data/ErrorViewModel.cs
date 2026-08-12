namespace dotnet_store.Models;

// Views/Shared/Error.cshtml sayfasına gönderilen basit hata görüntüleme modeli.
// Veritabanı tablosuyla ilgisi yoktur, sadece hata sayfasında request id göstermek için kullanılır.
public class ErrorViewModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}

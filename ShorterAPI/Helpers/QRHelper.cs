using QRCoder;

namespace ShorterAPI.Helpers;

public static class QrHelper
{
    public static string GenerateQrBase64(string url)
    {
        var qrGenerator = new QRCodeGenerator();
        var qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
        var pngQrCode = new PngByteQRCode(qrCodeData);
        byte[] qrCodeBytes = pngQrCode.GetGraphic(20);

        return Convert.ToBase64String(qrCodeBytes);
    }
}

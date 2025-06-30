// ProgressMessageHandler.cs
using System.Net;
using System.Net.Http;

public class ProgressMessageHandler : DelegatingHandler
{
    public event EventHandler<HttpProgressEventArgs>? HttpSendProgress;

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var content = request.Content;
        if (content != null)
        {
            var progressContent = new ProgressStreamContent(content, cancellationToken);
            progressContent.ProgressChanged += (sender, args) =>
            {
                HttpSendProgress?.Invoke(this, new HttpProgressEventArgs(
                    args.BytesTransferred, args.TotalBytes)); // Убрано ?? 0
            };
            request.Content = progressContent;
        }

        return await base.SendAsync(request, cancellationToken);
    }
}

public class HttpProgressEventArgs : EventArgs
{
    public long BytesTransferred { get; }
    public long? TotalBytes { get; } // Изменили на nullable long

    public HttpProgressEventArgs(long bytesTransferred, long? totalBytes)
    {
        BytesTransferred = bytesTransferred;
        TotalBytes = totalBytes;
    }
}



public class ProgressStreamContent : StreamContent
{
    private readonly CancellationToken _cancellationToken;
    public event EventHandler<HttpProgressEventArgs>? ProgressChanged;

    public ProgressStreamContent(HttpContent content, CancellationToken cancellationToken)
        : base(content.ReadAsStream())
    {
        _cancellationToken = cancellationToken;
    }

    protected override async Task SerializeToStreamAsync(
        Stream stream, TransportContext? context)
    {
        var buffer = new byte[8192];
        var totalLength = Headers.ContentLength ?? -1;
        var totalBytesRead = 0L;

        using var input = await ReadAsStreamAsync();
        int bytesRead;
        while ((bytesRead = await input.ReadAsync(buffer, _cancellationToken)) > 0)
        {
            await stream.WriteAsync(buffer.AsMemory(0, bytesRead), _cancellationToken);
            totalBytesRead += bytesRead;
            ProgressChanged?.Invoke(this, new HttpProgressEventArgs(
                totalBytesRead, totalLength));
        }
    }
}
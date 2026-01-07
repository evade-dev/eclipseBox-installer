using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using EclipseBoxInstaller.Core;

namespace EclipseBoxInstaller.Services
{  
    public class DownloaderService
    {
        // Simple, reusable HttpClient
        private static readonly HttpClient _httpClient = CreateClient();

        public DownloaderService() { }

        private static HttpClient CreateClient()
        {
            var client = new HttpClient
            {
                Timeout = TimeSpan.FromMinutes(30)
            };
            client.DefaultRequestHeaders.UserAgent.ParseAdd("eclipseBoxInstaller/1.0");
            return client;
        }

        public async Task DownloadAllFilesAsync(string installPath, Action<string, int> onProgress)
        {
            Directory.CreateDirectory(installPath);

            await DownloadFileAsync(Constants.PrismDownloadUrl, Path.Combine(installPath, Constants.PrismZipName), "Downloading Launcher...", 0, 33, onProgress);
            await DownloadFileAsync(Constants.JavaDownloadUrl, Path.Combine(installPath, Constants.JavaZipName), "Downloading Java...", 33, 66, onProgress);
            await DownloadFileAsync(Constants.ModpackUrl, Path.Combine(installPath, Constants.ModpackFileName), "Downloading Modpack...", 66, 100, onProgress);
        }

        private async Task DownloadFileAsync(string url, string destinationPath, string statusText, int startProgress, int endProgress, Action<string, int> onProgress)
        {
            onProgress(statusText, startProgress);

            if (!Uri.TryCreate(url, UriKind.Absolute, out var uri) || !uri.Scheme.Equals(Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Download URL must be an absolute HTTPS URL.");

            using (var response = await _httpClient.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();

                long? totalBytes = response.Content.Headers.ContentLength;

                using (var contentStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                using (var fileStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None, 81920, useAsync: true))
                {
                    var buffer = new byte[81920];
                    long totalRead = 0;
                    int bytesRead;

                    while ((bytesRead = await contentStream.ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false)) > 0)
                    {
                        await fileStream.WriteAsync(buffer, 0, bytesRead).ConfigureAwait(false);
                        totalRead += bytesRead;

                        if (totalBytes.HasValue && totalBytes.Value > 0)
                        {
                            double filePercentage = (double)totalRead / totalBytes.Value;
                            int totalProgress = startProgress + (int)(filePercentage * (endProgress - startProgress));
                            onProgress(statusText, totalProgress);
                        }
                    }
                }
            }

            onProgress(statusText, endProgress);
        }
    }
}
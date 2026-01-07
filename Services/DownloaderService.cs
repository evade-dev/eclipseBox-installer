using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using EclipseBoxInstaller.Core;

namespace EclipseBoxInstaller.Services
{
    public class DownloaderService
    {
        private readonly HttpClient _httpClient;

        public DownloaderService()
        {
            _httpClient = new HttpClient();
            // Fake user-agent so strict servers don't block us
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "eclipseBoxInstaller/1.0");
        }

        // We use a callback (Action) to update the progress bar on the UI
        public async Task DownloadAllFilesAsync(string installPath, Action<string, int> onProgress)
        {
            // Ensure the folder exists
            Directory.CreateDirectory(installPath);

            // 1. Download Prism Launcher
            await DownloadFileAsync(
                Constants.PrismDownloadUrl,
                Path.Combine(installPath, Constants.PrismZipName),
                "Downloading Launcher...",
                0, 33, // 0% to 33% total progress
                onProgress);

            // 2. Download Java
            await DownloadFileAsync(
                Constants.JavaDownloadUrl,
                Path.Combine(installPath, Constants.JavaZipName),
                "Downloading Java...",
                33, 66, // 33% to 66% total progress
                onProgress);

            // 3. Download Modpack
            await DownloadFileAsync(
                Constants.ModpackUrl,
                Path.Combine(installPath, Constants.ModpackFileName),
                "Downloading Modpack...",
                66, 100, // 66% to 100% total progress
                onProgress);
        }

        private async Task DownloadFileAsync(string url, string destinationPath, string statusText, int startProgress, int endProgress, Action<string, int> onProgress)
        {
            onProgress(statusText, startProgress);

            using (var response = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
            {
                response.EnsureSuccessStatusCode();

                // Get file size (if available) to calculate smooth progress
                long? totalBytes = response.Content.Headers.ContentLength;

                using (var contentStream = await response.Content.ReadAsStreamAsync())
                using (var fileStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    var buffer = new byte[8192];
                    long totalRead = 0;
                    int bytesRead;

                    while ((bytesRead = await contentStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        await fileStream.WriteAsync(buffer, 0, bytesRead);
                        totalRead += bytesRead;

                        if (totalBytes.HasValue)
                        {
                            // Calculate percentage for JUST this file
                            double filePercentage = (double)totalRead / totalBytes.Value;

                            // Map that to the total "slice" of progress (e.g. 0 to 33)
                            int totalProgress = startProgress + (int)(filePercentage * (endProgress - startProgress));

                            onProgress(statusText, totalProgress);
                        }
                    }
                }
            }

            // Ensure we hit the exact end number for this section
            onProgress(statusText, endProgress);
        }
    }
}
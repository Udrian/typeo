using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading.Tasks;
using TypeD.Models;

namespace TypeD.Commands.Module
{
    public partial class ModuleCommand
    {
        public async Task<bool> Download(string name, string version)
        {
            var modulePath = $"{ModuleModel.ModuleCachePath}/{name}/{version}";
            if (Directory.Exists($"{modulePath}")) return false;

            Directory.CreateDirectory(modulePath);
            var moduleUrl = new Uri($"https://typedeaf.nyc3.cdn.digitaloceanspaces.com/typeo/releases/modules/{name}/{name}-{version}.zip");
            var downloadZipPath = $"{modulePath}/{name}-{version}.zip";

            using (var client = new WebClient())
            {
                await client.DownloadFileTaskAsync(moduleUrl, downloadZipPath);
            }
            await Task.Run(() =>
            {
                ZipFile.ExtractToDirectory(downloadZipPath, modulePath);
            });

            await Task.Run(() =>
            {
                File.Delete(downloadZipPath);
            });
            return true;
            //TODO progress bar
        }
    }
}

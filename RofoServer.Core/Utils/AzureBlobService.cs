using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RofoServer.Core.Utils;

public class AzureBlobService : IBlobService
{
    private IConfiguration _config { get; set; }

    public AzureBlobService(IConfiguration config) {
        _config = config;
    }

    public async Task<string> CreateDirectory() {
        var containerClient =
            await new BlobServiceClient(_config["ConnectionStrings:BlobStore"])
                .CreateBlobContainerAsync($"rofo-group-{Guid.NewGuid()}");
        return containerClient.Value.Name;
    }

    public async Task<string> UploadPhoto(Stream photo, string container) {
        var fileName = "rofo-" + Guid.NewGuid();
        var blobInfo = await new BlobServiceClient(_config["ConnectionStrings:BlobStore"])
            .GetBlobContainerClient(container)
            .GetBlobClient(fileName)
            .UploadAsync(photo);
        return fileName;
    }

    public async Task DownloadPhoto(string container, string photo, Stream dest) {
        var blobber = new BlobServiceClient(_config["ConnectionStrings:BlobStore"])
            .GetBlobContainerClient(container)
            .GetBlobClient(photo);
        
        await blobber.DownloadToAsync(dest);
    }
}

public interface IBlobService
{
    Task<string> CreateDirectory();
    Task<string> UploadPhoto(Stream photo, string container);
    Task DownloadPhoto(string container, string photo, Stream dest);
}
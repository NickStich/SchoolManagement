using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SchoolManagement.Storage;

public class BlobStorageHelper
{
    private readonly BlobSettings _blobSettings;

    public BlobStorageHelper(IOptions<BlobSettings> blobSettings)
    {
        _blobSettings = blobSettings?.Value ?? throw new ArgumentNullException(nameof(blobSettings));
    }

    public async Task<string> UploadFile(IFormFile file)
    {
        try
        {
            var blob = await GetBlobReference(file.FileName); ;

            StreamReader streamReader = new(file.OpenReadStream());

            await blob.UploadFromStreamAsync(streamReader.BaseStream);

            return file.FileName;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<File?> GetFileByName(string fileName)
    {
        try
        {
            var blob = await GetBlobReference(fileName);

            if (blob == null || !await blob.ExistsAsync())
            {
                return null;
            }

            using var memoryStream = new MemoryStream();
            await blob.DownloadToStreamAsync(memoryStream);

            MemoryStream returnedStream = new();
            memoryStream.Position = 0;

            memoryStream.CopyTo(returnedStream);

            memoryStream.Position = 0;
            returnedStream.Position = 0;

            return new File(fileName, returnedStream);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while getting the file.", ex);
        }
    }

    private async Task<CloudBlockBlob> GetBlobReference(string fileName)
    {
        var storageAccount = CloudStorageAccount.Parse(_blobSettings.StorageConnectionString);
        var blobClient = storageAccount.CreateCloudBlobClient();
        var container = blobClient.GetContainerReference(_blobSettings.BlobName);

        return container.GetBlockBlobReference(fileName);
    }
}

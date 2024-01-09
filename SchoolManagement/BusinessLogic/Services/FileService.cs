using SchoolManagement.Storage;
using System;
using System.Threading.Tasks;

namespace SchoolManagement.BusinessLogic.Services;

public class FileService
{
    private readonly BlobStorageHelper _blobStorageHelper;

    public FileService(BlobStorageHelper blobStorageHelper)
    {
        _blobStorageHelper = blobStorageHelper;
    }

    public async Task<Storage.File?> GetFileByNameAsync(string fileName)
    {
        try
        {
            return await _blobStorageHelper.GetFileByName(fileName);
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while getting the file: {ex.Message}", ex);
        }
    }
}

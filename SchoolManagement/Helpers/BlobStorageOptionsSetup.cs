using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SchoolManagement.Storage;

namespace SchoolManagement.Helpers;

public class BlobStorageOptionsSetup : IOptions<BlobSettings>
{
    private const string SectionName = "BlobSettings";

    private readonly IConfiguration _configuration;

    public BlobStorageOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public BlobSettings Value => throw new System.NotImplementedException();

    public void Configure(BlobSettings options)
    {
        _configuration.GetSection(SectionName).Bind(options);
    }
}
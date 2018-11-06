using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SALON_HAIR_API.Extension
{
    public class Extension
    {
      
        public async Task uploadToS3Async(IFormFile file)
        {
            using (var client = new AmazonS3Client("AKIAJA5VZOI4FR4I6AWA", "VvnrpYaq31dSowt6xop2XLt+G1Ye4otMmFB1RlxL", RegionEndpoint.USEast1))
            {
                using (var newMemoryStream = new MemoryStream())
                {
                    file.CopyTo(newMemoryStream);

                    var uploadRequest = new TransferUtilityUploadRequest
                    {
                        InputStream = newMemoryStream,
                        Key = file.FileName,
                        BucketName = "yourBucketName",
                        CannedACL = S3CannedACL.PublicRead,
                        
                    };

                    var fileTransferUtility = new TransferUtility(client);
                   await fileTransferUtility.UploadAsync(uploadRequest);
                }
            }
        }
    }
}

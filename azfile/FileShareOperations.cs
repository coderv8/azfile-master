using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.File;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace azfile
{
    class FileShareOperations
    {
        public static void UploadtoFileShare(string shareReference,byte[] fileStream,string fileName)
        {
            // Parse the connection string and return a reference to the storage account.
            CloudStorageAccount storageAccount = Helper.storageAccount;



            // Create a reference to the file client.
            CloudFileClient fileClient = storageAccount.CreateCloudFileClient();



            CloudFileShare share = fileClient.GetShareReference(shareReference);
            share.CreateIfNotExists();

            if (share.Exists())
            {
                CloudFileDirectory rootDir = share.GetRootDirectoryReference();
                //Create a reference to the filename that you will be uploading
                CloudFile cloudFile = rootDir.GetFileReference(fileName);


                FileStream fStream = File.OpenRead("C:\\myprojs\\images\\krishna.jpg");





                cloudFile.UploadFromByteArray(fileStream,0,fileStream.Length);
                    Console.WriteLine($"File uploaded successfully   {fileName}");
                }
            }

        }

    
}

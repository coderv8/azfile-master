using System;
using Microsoft.Azure; // Namespace for Azure Configuration Manager
using Microsoft.Azure.Storage; // Namespace for Storage Client Library
using Microsoft.Azure.Storage.Blob; // Namespace for Azure Blobs
using Microsoft.Azure.Storage.File; // Namespace for Azure Files
using System.Windows.Forms;
using System.IO;

namespace azfile
{
    class Program
    {
        static void Main(string[] args)
        {
            //var file=SharepointOperations.getFileStream();
            SharepointOperations.getFileStream();
            FileShareOperations fso = new FileShareOperations();

       //    fso.UploadtoFileShare("myfiles",file.fileStream,file.fileName);


        }
    }
}

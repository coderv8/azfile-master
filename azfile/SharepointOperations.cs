using Microsoft.Azure.Storage.File;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace azfile
{
    class SharepointOperations
    {
        public static void getFileStream()
        {
            List list = Helper.SPClientContext.Web.Lists.GetByTitle("MyLib");
            Helper.SPClientContext.Load(list);
            Helper.SPClientContext.Load(list.RootFolder);
            Helper.SPClientContext.Load(list.RootFolder.Folders);
            Helper.SPClientContext.Load(list.RootFolder.Files);
            Helper.SPClientContext.ExecuteQuery();
            FolderCollection fcol = list.RootFolder.Folders;
            List<string> lstFile = new List<string>();
            byte[] dataArray;
            foreach (Folder f in fcol)
            {
                if (f.Name == "myfolder")
                {
                    Helper.SPClientContext.Load(f);
                    Helper.SPClientContext.Load(f.Files);
                    Helper.SPClientContext.ExecuteQuery();
                    FileCollection fileCol = f.Files;
                    foreach (Microsoft.SharePoint.Client.File file in fileCol)
                    {
                        Helper.SPClientContext.Load(file);
                        Helper.SPClientContext.ExecuteQuery();
                        ClientResult<System.IO.Stream> data = file.OpenBinaryStream();
                        Helper.SPClientContext.Load(file);
                        Helper.SPClientContext.ExecuteQuery();

                        using (System.IO.MemoryStream mStream = new System.IO.MemoryStream())
                        {
                            if (data != null)
                            {
                                data.Value.CopyTo(mStream);
                                dataArray = mStream.ToArray();
                                string b64String = Convert.ToBase64String(dataArray);

                                FileShareOperations.UploadtoFileShare("myfiles", dataArray, file.Name);
                            }
                        }
                    }




                }
                }
            }





















            // return file;

        }
    }


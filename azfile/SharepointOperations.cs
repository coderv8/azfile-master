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
          
           // var filterDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ");
            List list = Helper.SPClientContext.Web.Lists.GetByTitle("MyLib");
           // CamlQuery cml = new CamlQuery();
           // cml.ViewXml = "<View><Query><Where><eq><FieldRef Name='Modified'/><Value Type='DateTime' IncludeTimeValue='TRUE'>" + filterDate + "</Value></eq></Where>><OrderBy><FieldRef Name='Modified' Ascending = 'true' /></OrderBy></Query></View>";
            // Helper.SPClientContext.Load(list);
           // list.GetItems(cml);
            Helper.SPClientContext.Load(list.RootFolder);
            Helper.SPClientContext.Load(list.RootFolder.Folders);
           // Helper.SPClientContext.Load(list.RootFolder.Files);
            Helper.SPClientContext.ExecuteQuery();
            FolderCollection fcol = list.RootFolder.Folders;
            
            byte[] dataArray;
            foreach (Folder f in fcol)
            {
                if (f.Name == "myfolder")
                {
                    Helper.SPClientContext.Load(f.Files);
                    Helper.SPClientContext.ExecuteQuery();
                    FileCollection fileCol = f.Files;
                    foreach (Microsoft.SharePoint.Client.File file in fileCol)
                    {
                        if (file.TimeLastModified >= DateTime.Now.AddDays(-7))
                        {
                            ClientResult<System.IO.Stream> data = file.OpenBinaryStream();
                            Helper.SPClientContext.Load(file);
                            Helper.SPClientContext.ExecuteQuery();

                            using (System.IO.MemoryStream mStream = new System.IO.MemoryStream())
                            {
                                if (data != null)
                                {
                                    data.Value.CopyTo(mStream);
                                    dataArray = mStream.ToArray();
                                   
                                    FileShareOperations.UploadtoFileShare("myfiles", dataArray, file.Name);
                                }
                            }
                        }
                    }




                }
                }
            }





















            // return file;

        }
    }


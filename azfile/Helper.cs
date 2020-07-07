using Microsoft.Azure.Storage;
using System.Configuration;
using Microsoft.Azure.Storage.Blob.Protocol;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using Microsoft.Azure;

namespace azfile
{

    public class Helper
    {
        
        static  Helper()
        {
            GetConfiguration();
            GetSPClientContext();
            CreateStorageAccountFromConnectionString();
        }

        public static ClientContext SPClientContext;
        private static string SPSiteURL;
        private static string userName;
        private static string Password;
        private static string storageConnectionString;
        public static CloudStorageAccount storageAccount;
        public static void CreateStorageAccountFromConnectionString()
        {
            
            try
            {
                storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the sample.");
                Console.WriteLine("Press any key to exit");
                Console.ReadLine();
                throw;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the sample.");
                Console.WriteLine("Press any key to exit");
                Console.ReadLine();
                throw;
            }

            
        }

        
        public static void GetSPClientContext()
        {
            try
            {
                SPClientContext = new ClientContext(SPSiteURL);

                var securePassword = new SecureString();
                foreach (char c in Password)
                {
                    securePassword.AppendChar(c);
                }

                var onlineCredentials = new SharePointOnlineCredentials(userName, securePassword);

                SPClientContext = new ClientContext(SPSiteURL);
                SPClientContext.Credentials = onlineCredentials;
                SPClientContext.RequestTimeout = 100000;



                // The SharePoint web at the URL.
                Web web = SPClientContext.Web;

                // We want to retrieve the web's properties.
                SPClientContext.Load(web);

                // Execute the query to the server.
                SPClientContext.ExecuteQuery();
                Console.WriteLine($"SP Client context created for { web.Title}");
            }
            catch(Exception e)
            {
                Console.WriteLine("Failed to create SP Client Context");
                Console.ReadLine();

            }
        }


        private static void GetConfiguration()
        {
            SPSiteURL = ConfigurationManager.AppSettings["SPSiteURL"];
            userName = ConfigurationManager.AppSettings["UserName"];
            Password= ConfigurationManager.AppSettings["Password"];
            storageConnectionString = CloudConfigurationManager.GetSetting("StorageConnectionString");

        }
        public static void WriteException(Exception ex)
        {
            Console.WriteLine("Exception thrown. {0}, msg = {1}", ex.Source, ex.Message);
        }



    }
}


using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace ContosoAdsCommon
{
    public class ContosoAdsContext
    {
        public CloudStorageAccount StorageAccount { get; set; }
  
        public CloudTableClient TableClient { get; set; }
 
        public CloudTable Table { get; set; }

        public ContosoAdsContext()
        {
            //StorageAccount = CloudStorageAccount.Parse(
            //    CloudConfigurationManager.GetSetting("StorageConnectionString"));
            StorageAccount = CloudStorageAccount.Parse(
                ConfigurationManager.ConnectionStrings["AzureWebJobsStorage"].ToString());

            // Create the table client.
            TableClient = StorageAccount.CreateCloudTableClient();

            // Create the table if it doesn't exist.
            Table = TableClient.GetTableReference("imageTable");
            Table.CreateIfNotExists();


        }

        public IEnumerable<Ad> GetAds()
        {
            TableQuery<Ad> query = new TableQuery<Ad>();

            return Table.ExecuteQuery(query);
        }

        public Ad GetAd(Guid id)
        {
            return this.GetAds().FirstOrDefault(x => x.AdId == id);
        }

        public void Add(Ad ad)
        {
            TableOperation insertOperation = TableOperation.Insert(ad);
            ad.AdId = Guid.NewGuid();
            // Execute the operation.
            this.Table.Execute(insertOperation);
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Table.DataServices;

namespace ContosoAdsCommon
{
    public enum Category
    {
        Cars,
        [Display(Name = "Real Estate")]
        RealEstate,
        [Display(Name = "Free Stuff")]
        FreeStuff
    }
    public class Ad : TableEntity
    {

        public Ad()
        {
            PartitionKey = Title;
            RowKey = Guid.NewGuid().ToString();
        }
        [StringLength(100)]
        public string Title { get { return PartitionKey; } set { PartitionKey = value; } }


        [StringLength(2083)]
        [DisplayName("Full-size Image")]
        public string ImageURL { get; set; }
        public Guid AdId { get; set; }


        public int Price { get; set; }
        [StringLength(1000)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }



        [StringLength(2083)]
        [DisplayName("Thumbnail")]
        public string ThumbnailURL { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PostedDate { get; set; }
        public Category? Category { get; set; }
        [StringLength(12)]
        public string Phone { get; set; }
    }

}


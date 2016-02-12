using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using System.Configuration;

namespace KonstProjektetV2.Models
{
    public class TableModel : TableEntity
    {
        public TableModel(string Artist, string Title)
        {
            this.PartitionKey = Artist;
            this.RowKey = Title;
        }

        public string Location { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using System.Configuration;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KonstProjektetV2.Models
{
    public class TableModel : TableEntity
    {
        private string _author;
        private string _title;
        public TableModel(string artist, string title)
        {
            Title = title;
            Author = artist;
        }

        public TableModel() {
        }

        public string Author { get { return _author; } set { this.PartitionKey = value; _author = value; } }

        public string Title { get { return _title; } set { this.RowKey = value; _title = value; } }


        public string Location { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }

        public string FileName { get; set; }
    }

    public class TableInsertModel
    {
        [DisplayName("Artist")]
        [Required]
        public string Author { get; set; }

        [DisplayName("Titel")]
        [Required]
        public string Title { get; set; }


        [DisplayName("Plats")]
        public string Location { get; set; }

        [DisplayName("Typ")]
        public string Type { get; set; }

        [DisplayName("Beskrivning")]
        public string Description { get; set; }

        [DisplayName("Bild")]
        public HttpPostedFileBase File { get; set; }

        public string FileName { get; set; }
    }
}
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
        public TableModel(string artistKey, string titleKey)
        {
            TitleKey = titleKey;
            AuthorKey = artistKey;
        }

        public TableModel() {
        }

        public string AuthorKey { get { return _author; } set { this.PartitionKey = value; _author = value; } }

        public string TitleKey { get { return _title; } set { this.RowKey = value; _title = value; } }

        [DisplayName("Artist")]
        public string Author { get; set; }

        [DisplayName("Titel")]
        public string Title { get; set; }

        [DisplayName("Plats")]
        public string Location { get; set; }

        [DisplayName("Typ")]
        public string Type { get; set; }

        [DisplayName("Beskrivning")]
        public string Description { get; set; }

        [DisplayName("Bild")]
        public string FileName { get; set; }
    }

    public class TableInsertModel
    {
        [Required]
        public string AuthorKey { get; set; }

        [Required]
        public string TitleKey { get; set; }
        
        [DisplayName("Artist")]
        public string Author { get; set; }

        [DisplayName("Titel")]
        public string Title { get; set; }

        [DisplayName("Plats")]
        public string Location { get; set; }

        [DisplayName("Typ")]
        public string Type { get; set; }

        [Required]
        [DisplayName("Beskrivning")]
        public string Description { get; set; }

        [DisplayName("Bild")]
        public HttpPostedFileBase File { get; set; }

        public string FileName { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using System.Configuration;
using KonstProjektetV2.Models;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;

namespace KonstProjektetV2.Controllers
{
    public class HomeController : Controller
    {
        CloudStorageAccount storageAccount;
        CloudTableClient tableClient;
        CloudBlobClient blobClient;
        CloudTable table;
        CloudBlobContainer container;

        public HomeController()
        {
            storageAccount = CloudStorageAccount.Parse(
            ConfigurationManager.AppSettings["StorageConnectionString"]);

            tableClient = storageAccount.CreateCloudTableClient();

            table = tableClient.GetTableReference("fouramigos");

            table.CreateIfNotExists();

            blobClient = storageAccount.CreateCloudBlobClient();

            container = blobClient.GetContainerReference("fouramigos");

            container.CreateIfNotExists();

            BlobContainerPermissions permissions = container.GetPermissions();
            permissions.PublicAccess = BlobContainerPublicAccessType.Container;
            container.SetPermissions(permissions);


            //lägga till nya
            //var tablemodels = new TableModel("Brutus", "Uggla") { Location = "T4", Description="Uggla i träd", Type="Foto" };
            //var tablemodels1 = new TableModel("brutus", "Örn") { Location = "T4", Description="Örn som flyger", Type = "Foto" };

            //var opreation = TableOperation.Insert(tablemodels);
            //var operation2 = TableOperation.Insert(tablemodels1);

            //table.Execute(opreation);
            //table.Execute(operation2);
        }


        // GET: Home
        public ActionResult Index()
        {
            var query = new TableQuery<TableModel>();

            var tableModels = table.ExecuteQuery(query);

            return View(tableModels);
        }

        //Vissa info om konstverk(ej obligatorisk)
        public ActionResult Table(string partitionKey, string rowKey)
        {
            var operation = TableOperation.Retrieve<TableModel>(partitionKey, rowKey);

            var tablemodel = (TableModel)table.Execute(operation).Result;


            var name = tablemodel.Author + "-" + tablemodel.Title + "." + tablemodel.FileEnding;

            var blob = container.GetBlockBlobReference(name);


            return View(tablemodel);
        }

        //lägg till nytt konstverk med bild
        public ActionResult AddNew()
        {
            return View(new TableInsertModel());
        }
        [HttpPost]
        public async Task<ActionResult> AddNew(TableInsertModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }


            string fileEnding = string.Empty;
            if(model.File != null)
            {
                fileEnding = model.File.FileName.Split('.').Last();

                var name = model.Author + "-" + model.Title + "." + fileEnding;

                var blob = container.GetBlockBlobReference(name);

                await blob.UploadFromStreamAsync(model.File.InputStream);
            }

            var operation = TableOperation.Insert(new TableModel(model.Author, model.Title) { Description = model.Description, Type = model.Type, Location = model.Location, FileEnding = fileEnding });

            await table.ExecuteAsync(operation);

            return RedirectToAction("Index");
        }

        //Ta bort konstverk med bild
        //public ActionResult Delete()
        //{
        //    return View();
        //}
        //[HttpGet]

        public ActionResult Delete(string partitionKey, string rowKey)
        {
            var operation = TableOperation.Retrieve<TableModel>(partitionKey, rowKey);

            var tablemodel = (TableModel)table.Execute(operation).Result;

            var name = tablemodel.Author + "-" + tablemodel.Title + "." + tablemodel.FileEnding;
            var blob = container.GetBlockBlobReference(name);
            blob.Delete();

            var list = container.ListBlobs();

            var deleteoperation = TableOperation.Delete(tablemodel);

            table.Execute(deleteoperation);

            return RedirectToAction("Index");

            //return View(tablemodel);
        }

        //[HttpPost]
        //public ActionResult Delete(string partitionKey)
        //{
        //    var deleteoperation = TableOperation.Delete(tablemodel);

        //    table.Execute(deleteoperation);

        //    return RedirectToAction("Index");
        //}
    }
}
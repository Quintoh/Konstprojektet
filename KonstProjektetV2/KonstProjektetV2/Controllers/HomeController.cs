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
        CloudTable tableAdmin;
        CloudBlobContainer container;

        public HomeController()
        {
            storageAccount = CloudStorageAccount.Parse(
            ConfigurationManager.AppSettings["StorageConnectionString"]);

            tableClient = storageAccount.CreateCloudTableClient();

            table = tableClient.GetTableReference("fouramigos");

            table.CreateIfNotExists();

            tableAdmin = tableClient.GetTableReference("fouramigosAdmin");

            tableAdmin.CreateIfNotExists();

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
            return View();
        }

        //Vissa info om konstverk(ej obligatorisk)
        public ActionResult Table(string partitionKey, string rowKey)
        {
            var operation = TableOperation.Retrieve<TableModel>(partitionKey, rowKey);

            var tablemodel = (TableModel)table.Execute(operation).Result;

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
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            string name = string.Empty;
            if (model.File != null)
            {
                var fileEnding = model.File.FileName.Split('.').Last();

                name = Guid.NewGuid().ToString() + "." + fileEnding;

                var blob = container.GetBlockBlobReference(name);

                await blob.UploadFromStreamAsync(model.File.InputStream);
            }

            var operation = TableOperation.Insert(new TableModel(model.AuthorKey, model.TitleKey) { Author = model.AuthorKey, Title = model.TitleKey, Description = model.Description, Type = model.Type, Location = model.Location, FileName = name });

            await table.ExecuteAsync(operation);

            return RedirectToAction("Gallery");
        }

        /// <summary>
        /// Ta bort konstverk med bild
        /// </summary>
        /// <param name="partitionKey">Artistnamn</param>
        /// <param name="rowKey">Title</param>
        /// <returns>Redirect to index</returns>
        public ActionResult Delete(string partitionKey, string rowKey)
        {
            var operation = TableOperation.Retrieve<TableModel>(partitionKey, rowKey);

            var tablemodel = (TableModel)table.Execute(operation).Result;

            var name = tablemodel.FileName;
            if (!string.IsNullOrEmpty(name))
            {
                var blob = container.GetBlockBlobReference(name);
                blob.Delete();
            }

            var deleteoperation = TableOperation.Delete(tablemodel);

            table.Execute(deleteoperation);
         
            return RedirectToAction("Gallery");
        }

        //Redigera existerande konstverk
        [HttpGet]
        public ActionResult Edit(string partitionKey, string rowKey)
        {
            var operation = TableOperation.Retrieve<TableModel>(partitionKey, rowKey);

            var tablemodel = (TableModel)table.Execute(operation).Result;

            var model = new TableInsertModel()
            {
                AuthorKey = tablemodel.PartitionKey,
                TitleKey = tablemodel.RowKey,
                Author = tablemodel.Author,
                Title = tablemodel.Title,
                Location = tablemodel.Location,
                Type = tablemodel.Type,
                Description = tablemodel.Description,
                FileName = tablemodel.FileName,
            };

            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(TableInsertModel model)
        {
            var operation = TableOperation.Retrieve<TableModel>(model.AuthorKey, model.TitleKey);

            var tableModel = (TableModel)table.Execute(operation).Result;

            string name = tableModel.FileName;
            if (model.File != null)
            {
                if (!string.IsNullOrEmpty(name))
                {
                    var oldBlob = container.GetBlockBlobReference(name);
                    oldBlob.Delete();
                }

                var fileEnding = model.File.FileName.Split('.').Last();

                name = Guid.NewGuid().ToString() + "." + fileEnding;

                var blob = container.GetBlockBlobReference(name);

                await blob.UploadFromStreamAsync(model.File.InputStream);
            }


            tableModel.Author = model.Author;
            tableModel.Title = model.Title;
            tableModel.Location = model.Location;
            tableModel.Type = model.Type;
            tableModel.Description = model.Description;
            tableModel.FileName = name;

            var replaceOperation = TableOperation.Replace(tableModel);

            table.Execute(replaceOperation);

            return RedirectToAction("Gallery");
        }

        public ActionResult Gallery(string search)
        { 

            var query = new TableQuery<TableModel>();
            var tableModels = table.ExecuteQuery(query);
            

            if (!string.IsNullOrEmpty(search))
            {
                tableModels = tableModels.Where(x => x.Author.ToLower().Contains(search.ToLower()) || x.Title.ToLower().Contains(search.ToLower()) || x.Description.ToLower().Contains(search.ToLower()));
                ViewBag.SearchWord ="Filter: " +  search;
                if (!tableModels.Any())
                {
                    ViewBag.SearchWord = "Inga konstverk hittades...";
                }
            }
 
            return View(tableModels);
        }

        public ActionResult LogIn()
        {
            var query = new TableQuery<TableAdminModel>();
            var tableModels = table.ExecuteQuery(query);
            return View(tableModels);
        }

        [HttpPost]
        public ActionResult LogIn(string username, string password)
        {
            var query = new TableQuery<TableAdminModel>();
            var tableModels = table.ExecuteQuery(query);
            var list = tableModels.Select(i => new { i.Username, i.Password }).ToArray();
            foreach (var item in list)
            {
                if ("admin" == username && "1234" == password)
                {
                    Session["user"] = new Admin() { Username = username };
                    return RedirectToAction("Gallery");
                }
            }
            return View(tableModels);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            return View();
        }

    }
}
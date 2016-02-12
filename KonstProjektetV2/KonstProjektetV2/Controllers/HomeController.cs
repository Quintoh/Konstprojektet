using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using System.Configuration;

namespace KonstProjektetV2.Controllers
{
    public class HomeController : Controller
    {
        CloudStorageAccount storageAccount;
        CloudTableClient tableClient;
        CloudTable table;

        public HomeController()
        {
            storageAccount = CloudStorageAccount.Parse(
            ConfigurationManager.AppSettings["StorageConnectionString"]);

            tableClient = storageAccount.CreateCloudTableClient();

            table = tableClient.GetTableReference("fouramigos");
        }


        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}
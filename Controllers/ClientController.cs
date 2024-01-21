using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using TravelApp.Data;
using TravelApp.Models;

namespace TravelApp.Controllers
{
    public class ClientController : Controller
    {
        // GET: Client
        public ViewResult Index()
        {
            List<Clients> clients = new List<Clients>();

            ClientsDAO clientDAO = new ClientsDAO();

            clients = clientDAO.FetchAll();

            return View("Index", clients);
        }
        
        public ActionResult Details(int id)
        {
            ClientsDAO clientDAO = new ClientsDAO();
            Clients client = clientDAO.FetchOne(id);

            return View("Details", client);
        }

        public ActionResult Create()
        {
            return View("ClientForm", new Clients());
        }

        public ActionResult Edit(int id)
        {
            ClientsDAO clientDAO = new ClientsDAO();
            Clients client = clientDAO.FetchOne(id);

            return View("ClientForm", client);
        }

        public ActionResult Delete(int id)
        {
            ClientsDAO clientDAO = new ClientsDAO();
            clientDAO.Delete(id);

            List<Clients> client = clientDAO.FetchAll();

            return View("Index", client);
        }

        [HttpPost]
        public ActionResult CreateProcess(Clients newClient)
        {
            //sacuva u bazu
            ClientsDAO clientDAO = new ClientsDAO();

            clientDAO.CreateOrUpdate(newClient);

            return View("Details", newClient);
        }
    }
}

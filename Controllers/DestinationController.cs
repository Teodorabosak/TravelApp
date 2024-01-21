using TravelApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using TravelApp.Data;

namespace TravelApp.Controllers
{
    public class DestinationController : Controller
    {

        //// GET: Destination
        public ViewResult Index()
        {
            List<Destination> destination = new List<Destination>();

            DestinationDAO destinationDAO = new DestinationDAO();

            destination = destinationDAO.FetchAll();

            return View("Index", destination);
        }
        public ActionResult Details(int id)
        {
            DestinationDAO destinationDAO = new DestinationDAO();
            Destination destination = destinationDAO.FetchOne(id);

            return View("Details", destination);
        }

        public ActionResult Create()
        {
            return View("DestinationForm", new Destination());
        }

        public ActionResult Edit(int id)
        {
            DestinationDAO destinationDAO = new DestinationDAO();
            Destination destination = destinationDAO.FetchOne(id);

            return View("DestinationForm", destination);
        }

        public ActionResult Delete(int id)
        {
            DestinationDAO destinationDAO = new DestinationDAO();
            destinationDAO.Delete(id);

            List<Destination> destination = destinationDAO.FetchAll();

            return View("Index", destination);
        }

        [HttpPost]
        public ActionResult CreateProcess(Destination newDestination)
        {
            //sacuva u bazu
            DestinationDAO destinationDAO = new DestinationDAO();

            destinationDAO.CreateOrUpdate(newDestination);

            return View("Details", newDestination);
        }
    }
}
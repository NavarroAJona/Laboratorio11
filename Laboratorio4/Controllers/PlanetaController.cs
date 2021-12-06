using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Laboratorio4.Handlers; //Codigo necesario para acceder a handler y model.
using Laboratorio4.Models;


namespace Laboratorio4.Controllers
{
    public class PlanetaController : Controller
    {

        public ActionResult listadoDePlanetas()
        {
            PlanetaHandler accesoDatos = new PlanetaHandler();
            ViewBag.planeta = accesoDatos.obtenerTodoslosPlanetas();
            return View();
        }

        public ActionResult crearPlaneta()
        {
            return View("crearPlaneta");
        }


        [HttpPost]
        public ActionResult crearPlaneta(PlanetaModel planeta)
        {
            ViewBag.ExitoAlCrear = false;
            try
            {
                if (ModelState.IsValid)
                {
                    PlanetaHandler accesoDatos = new PlanetaHandler();
                    ViewBag.ExitoAlCrear = accesoDatos.crearPlaneta(planeta); // recuerde que este método devuelve un booleano
                    if (ViewBag.ExitoAlCrear)
                    {
                        ViewBag.Message = "El planeta" + " " + planeta.nombre + " fue creado con éxito:)";
                        ModelState.Clear();
                    }
                }
                return View();
            }
            catch
            {
                ViewBag.Message = "Algo salió mal y no fue posible crear el planeta: (";
                return View(); // si falla se regresa a la vista original pero sin el mensaje de
            }
        }


        [HttpGet]
        public ActionResult editarPlaneta(int? identificador)
        {

            ActionResult vista;
            try
            {
                PlanetaHandler accesoDatos = new PlanetaHandler();
                PlanetaModel planetaModificar = accesoDatos.obtenerTodoslosPlanetas().Find(smodel => smodel.id == identificador);
                if (planetaModificar == null)
                {
                    vista = RedirectToAction("listadoDePlanetas");
                }
                else
                {
                    vista = View(planetaModificar);
                }
            }
            catch
            {
                vista = RedirectToAction("listadoDePlanetas");
            }
            return vista;
        }

        [HttpPost]
        public ActionResult editarPlaneta(PlanetaModel planeta)
        {
            try
            {
                PlanetaHandler accesoDatos = new PlanetaHandler();
                accesoDatos.modificarPlaneta(planeta);
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }
    }



}
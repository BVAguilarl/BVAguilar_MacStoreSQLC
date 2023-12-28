using ML;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult IniciarSesion() 
        { 
            return View(); 
        }


        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult IniciarSesion(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            string url = System.Configuration.ConfigurationManager.AppSettings["UrlApi"];

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(url);

                var task = cliente.PostAsJsonAsync<ML.Usuario>("usuario/UsuarioGetByCorreo", usuario);
                task.Wait();

                var response = task.Result;

                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    result.Object = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(readTask.Result.Object.ToString());
                    Session["SessionUsuarionNombre"] = ((ML.Usuario)result.Object).Nombre;
                    Session["SessionUsuarioEmail"] = ((ML.Usuario)result.Object).Email;

                    return RedirectToAction("GetAll", "Usuario");
                }
                else
                {
                    ViewBag.Message = String.Format("El e-mail o contraseña son incorrectos, intente nuevamente");
                    return View();
                }
            }
        }

        [HttpPost]
        public ActionResult Registrar(ML.Usuario usuario)
        {
            string url = System.Configuration.ConfigurationManager.AppSettings["UrlApi"];

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(url);

                var task = cliente.PostAsJsonAsync<ML.Usuario>("usuario/add", usuario);
                task.Wait();

                var response = task.Result;

                if (response.IsSuccessStatusCode)
                {
                    ViewBag.Message = String.Format("Se ha ingresado correctamente  el usuario");
                    return View();
                }
                else
                {
                    ViewBag.Mensaje = String.Format("No se ha ingresado correctamente  el usuario.");
                }

            }
            return RedirectToAction("IniciarSesion");
        }

        public ActionResult CerrarSesion()
        {
            Session["SessionUsuarionNombre"] = null;
            Session["SessionUsuarioEmail"] = null;

            return RedirectToAction("IniciarSesion");
        }
    }
}
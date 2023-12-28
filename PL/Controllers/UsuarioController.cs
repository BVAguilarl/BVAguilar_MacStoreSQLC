using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {

        public ActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Usuarios = new List<Object>();

            string url = System.Configuration.ConfigurationManager.AppSettings["UrlApi"];

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(url);

                var task = cliente.GetAsync("usuario/getall");
                task.Wait();

                var response = task.Result;

                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var ResultItem in readTask.Result.Objects)
                    {
                        ML.Usuario ResultUsuariosList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(ResultItem.ToString());
                        usuario.Usuarios.Add(ResultUsuariosList);
                    }
                }
            }
            return View(usuario);
        }

        public JsonResult Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            string url = System.Configuration.ConfigurationManager.AppSettings["UrlApi"];

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(url);

                var task = cliente.PutAsJsonAsync<ML.Usuario>("usuario/update", usuario);
                task.Wait();

                var response = task.Result;

                if (response.IsSuccessStatusCode)
                {
                    result.Object = usuario;
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                }

            }
            return Json(result);
        }

        public JsonResult GetById(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            string url = System.Configuration.ConfigurationManager.AppSettings["UrlApi"];

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(url);

                var task = cliente.GetAsync("usuario/getbyid/" + IdUsuario);
                task.Wait();

                var response = task.Result;

                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    result.Object = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(readTask.Result.Object.ToString());
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                }

            }
            return Json(result);
        }

        public JsonResult DeleteUsuario(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            string url = System.Configuration.ConfigurationManager.AppSettings["UrlApi"];

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(url);

                var task = cliente.DeleteAsync("usuario/delete/" + IdUsuario);
                task.Wait();

                var response = task.Result;

                if (response.IsSuccessStatusCode)
                {
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                }
            }
            return Json(result);
        }
    }
}
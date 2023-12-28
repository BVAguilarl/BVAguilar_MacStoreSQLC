using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL_API.Controllers
{
    public class UsuarioController : ApiController
    {
        [HttpGet]
        [Route("api/usuario/getall")]
        public IHttpActionResult GetAll()
        {
            ML.Result result = BL.Usuario.GetAllUsuario();

            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }

        [HttpPost]
        [Route("api/usuario/add")]
        public IHttpActionResult Add(ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.AddUsuario(usuario);

            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }

        [HttpPut]
        [Route("api/usuario/update")]
        public IHttpActionResult Update(ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.UpdateUsuario(usuario);

            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }


        [HttpGet]
        [Route("api/usuario/getbyid/{IdUsuario}")]
        public IHttpActionResult GetById(int IdUsuario)
        {
            ML.Result result = BL.Usuario.GetByIdUsuarios(IdUsuario);

            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }

        [HttpDelete]
        [Route("api/usuario/delete/{IdUsuario}")]
        public IHttpActionResult Delete(int IdUsuario)
        {
            ML.Result result = BL.Usuario.DeleteUsuario(IdUsuario);

            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }

        [HttpPost]
        [Route("api/usuario/UsuarioGetByCorreo")]
        public IHttpActionResult UsuarioGetByCorreo(ML.Usuario usuarioCP)
        {
            ML.Result result = BL.Usuario.UsuarioGetByCorreo(usuarioCP);

            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ValidacionPorCookie.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Cookie : Controller
    {
        [HttpGet]
        public IActionResult GET()
        {
            return Ok("Cookie seteada");
        }


        [HttpPost]
        public IActionResult POST()
        {
            return Ok("Acceso Concedido");
        }

    }
}

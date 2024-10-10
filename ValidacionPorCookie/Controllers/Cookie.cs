using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ValidacionPorCookie.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Cookie : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GET()
        {
            await HttpContext.SignInAsync("EsquemaDefault",new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim[] 
                    {
                        new Claim(ClaimTypes.NameIdentifier,"158354"),
                        new Claim("IdUAT","Esto seria el ID de mi UAT")
                    },
                    "EsquemaDefault"
                )
            ));
            return Ok("Cookie seteada");
        }


        [HttpPost]
        [Authorize]
        public IActionResult POST()
        {
            var c = HttpContext.User.FindFirst("IdUAT");
            return Ok("Acceso Concedido: " + c.Value.ToString());
        }


        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DELETE()
        {
            await HttpContext.SignOutAsync("EsquemaDefault");

            return Ok("Saliste");
        }

    }
}

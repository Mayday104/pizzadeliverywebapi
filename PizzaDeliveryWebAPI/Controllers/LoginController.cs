using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaDeliveryWebAPI.Context;
using PizzaDeliveryWebAPI.Models;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace PizzaDeliveryWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        PizzaDeliveryDBContext db = new PizzaDeliveryDBContext();

        [HttpPost]
        public Response Post(UsuarioViewModel usuario)
        {
            Usuario usuarioBuscar = new Usuario();
            usuarioBuscar.Email = usuario.Email;


            using (MD5 md5Hash = MD5.Create())
            {
                usuarioBuscar.Contrasenia = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(usuario.Contrasenia));                 
            }

            var usuarioEncontrado = (from usuarioDB in db.Usuario 
                                    where usuarioDB.Email.Equals(usuarioBuscar.Email) && usuarioDB.Contrasenia == usuarioBuscar.Contrasenia
                                    select usuarioDB).FirstOrDefault();

            if (usuarioEncontrado != null)
            {

                ClienteUsuario clienteUsuario = new ClienteUsuario();
                clienteUsuario.UsuarioViewModel = new UsuarioViewModel();

                clienteUsuario.UsuarioViewModel.Email = usuarioEncontrado.Email;

                Cliente cliente = (from clienteDB in db.Cliente
                                   where clienteDB.IdUsuario == usuarioEncontrado.IdUsuario
                                   select clienteDB).FirstOrDefault();

                clienteUsuario.Cliente = cliente;

                return new Response { Success=true, Data= clienteUsuario };
            }
            else
            {
                return new Response { Success = false, Data = null , Message="Usuario o contraseña incorrecto" };
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaDeliveryWebAPI.Context;
using PizzaDeliveryWebAPI.Models;
using System.Linq;

namespace PizzaDeliveryWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {

        PizzaDeliveryDBContext db = new PizzaDeliveryDBContext();

        [HttpGet]
        public Response Get()
        {
            var data = (from cliente in db.Cliente
                        select cliente).ToList();

            return new Response { Success = true, Data = data };

        }

        [HttpGet("{id}")]
        public Response Get(int id)
        {
            Cliente clienteDB = (from cliente in db.Cliente
                                 where cliente.IdCliente == id
                                 select cliente).FirstOrDefault();

            if (clienteDB != null)
            {
                return new Response { Success = true, Data = clienteDB };
            }
            else
            {
                return new Response { Success = false, Data = null, Message= "Registro no encontrado" };
            }
        }

        [HttpPost]
        public Response Post(Cliente cliente)
        {
            db.Cliente.Add(cliente);
            db.SaveChanges();

            return new Response { Success = true, Data = cliente };
        }

        [HttpPut("{id}")]
        public Response Put(int id,Cliente cliente)
        {
            Cliente clienteDB= (from clienteBusca in db.Cliente
                                where clienteBusca.IdCliente==id
                                select clienteBusca).FirstOrDefault();

            if (clienteDB == null)
            {
                return new Response { Success = false, Data = null, Message = "Registro no encontrado" }; 
            }
            else
            {
                cliente.IdCliente = id;

                db.Entry(clienteDB).State = Microsoft.EntityFrameworkCore.EntityState.Detached;

                db.Entry(cliente).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                db.SaveChanges();

                return new Response { Success = true, Data = cliente };
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaDeliveryWebAPI.Context;
using PizzaDeliveryWebAPI.Models;

namespace PizzaDeliveryWebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {

        pizzaDeliveryDBContext db = new pizzaDeliveryDBContext();

        [HttpGet]
        public Respuesta Get()
        {
            var listadoClientes = (from cliente in db.Clientes
                                   select cliente).ToList();

            return new Respuesta { Success = true, Data = listadoClientes };
        }


        [HttpGet("{id}")]
        public Respuesta Get(int id)
        {
            Cliente clienteDB = (from cliente in db.Clientes
                                 where cliente.IdCliente == id
                                 select cliente).FirstOrDefault();

            return new Respuesta
            {
                Success = true,
                Data = clienteDB
            };
        }

        [HttpPost]
        public Respuesta Post(Cliente cliente)
        {
            try
            {
                db.Clientes.Add(cliente);

                db.SaveChanges();

                return new Respuesta { Success = true, Data = cliente };
            }
            catch (Exception ex)
            {
                return new Respuesta { Success = false, Message = "Error: " + ex.Message };
            }
        }


        [HttpPut("{id}")]
        public Respuesta Put(int id, Cliente cliente)
        {
            Respuesta respuesta = new Respuesta();

            Cliente clienteDB = (from clienteB in db.Clientes
                                 where clienteB.IdCliente == id
                                 select clienteB).FirstOrDefault();

            if (clienteDB == null)
            {
                respuesta.Success = false;
                respuesta.Message = "Cliente no encontrado";
            }
            else
            {
                db.Entry(clienteDB).State = Microsoft.EntityFrameworkCore.EntityState.Detached;

                db.Entry(cliente).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                try
                {
                    db.SaveChanges();

                    respuesta.Success = true;
                    respuesta.Data = cliente;
                }
                catch (Exception ex)
                {
                    respuesta.Success = false;
                    respuesta.Message = "Error: " + ex.Message;
                }
            }

            return respuesta;
        }

        [HttpDelete("{id}")]
        public Respuesta Delete(int id)
        {
            Respuesta respuesta=new Respuesta();

            Cliente clienteDb = db.Clientes.Find(id);

            if (clienteDb == null)
            {
                respuesta.Success = false;
                respuesta.Message = "Registro no encontrado";
            }
            else
            {
                db.Clientes.Remove(clienteDb);
                db.SaveChanges();

                respuesta.Success = true;
                respuesta.Message = "Registro eliminado";
            }

            return respuesta;
        }
       
    }
}

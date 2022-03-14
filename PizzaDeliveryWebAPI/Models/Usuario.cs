using System;
using System.Collections.Generic;

namespace PizzaDeliveryWebAPI.Models
{
    public partial class Usuario
    {
        public int IdUsuario { get; set; }
        public string Email { get; set; } = null!;
        public byte[] Contrasenia { get; set; } = null!;
        public DateTime? FechaUltimoAcceso { get; set; }
    }
}

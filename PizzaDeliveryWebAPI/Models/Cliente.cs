using System;
using System.Collections.Generic;

namespace PizzaDeliveryWebAPI.Models
{
    public partial class Cliente
    {
        public int IdCliente { get; set; }
        public string PrimerNombre { get; set; } = null!;
        public string? SegundoNombre { get; set; }
        public string PrimerApellido { get; set; } = null!;
        public string? SegundoApellido { get; set; }
        public string NoTelefono { get; set; } = null!;
    }
}

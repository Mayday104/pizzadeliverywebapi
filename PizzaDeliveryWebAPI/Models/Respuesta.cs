namespace PizzaDeliveryWebAPI.Models
{
    public class Respuesta
    {
        public bool Success { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }
    }
}

namespace CarsApi.Model
{
    public class Coche
    {
        public int Id { get; set; }
        public required string Marca { get; set; }
        public required string Modelo { get; set; }
        public required string Potencia { get; set; }
        
    }
}

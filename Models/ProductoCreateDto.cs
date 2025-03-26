namespace MiProyectoApi.Models
{
    public class ProductoCreateDto
    {
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
    }
}
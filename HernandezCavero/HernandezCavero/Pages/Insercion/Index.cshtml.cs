using HernadezCavero.Services;
using HernandezCavero.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace HernandezCavero.Pages.Insercion
{
    public class IndexModel : PageModel
    {
        private readonly ExamenDbContext _context;

        [BindProperty(SupportsGet = true)]
        public int? CategoriaVentaId { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Debe seleccionar un producto.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un producto válido.")]
        public int ProductoId { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Debe introducir la cantidad.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que 0.")]
        public int Cantidad { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Debe introducir el nombre del cliente.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre del cliente debe tener entre 2 y 100 caracteres.")]
        public string Cliente { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Debe seleccionar una fecha.")]
        public DateTime FechaVenta { get; set; }

        public string MensajeExito { get; set; }

        public IndexModel(ExamenDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            // La página solo se carga, el ViewComponent maneja el filtrado
        }

        public IActionResult OnPostInsertarVenta()
        {
            // Validar ModelState
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Validación adicional: verificar que la categoría esté seleccionada
            if (!CategoriaVentaId.HasValue)
            {
                ModelState.AddModelError(string.Empty, "Debe seleccionar una categoría primero.");
                return Page();
            }

            // Obtener el producto para validar que exista y pertenezca a la categoría
            var producto = _context.Productos.Find(ProductoId);
            if (producto == null)
            {
                ModelState.AddModelError(nameof(ProductoId), "El producto seleccionado no existe.");
                return Page();
            }

            if (producto.CategoriaId != CategoriaVentaId.Value)
            {
                ModelState.AddModelError(nameof(ProductoId), "El producto no pertenece a la categoría seleccionada.");
                return Page();
            }

            // Crear y guardar la venta
            var venta = new Venta
            {
                ProductoId = ProductoId,
                Cantidad = Cantidad,
                Cliente = Cliente.Trim(),
                FechaVenta = FechaVenta
            };

            _context.Ventas.Add(venta);
            _context.SaveChanges();

            MensajeExito = $"Venta insertada correctamente. Producto: {producto.Nombre}, Cliente: {Cliente}, Cantidad: {Cantidad}, Total: {(producto.Precio * Cantidad):C}";

            // Redirigir para limpiar el formulario
            return RedirectToPage(new { CategoriaVentaId = producto.CategoriaId });
        }
    }
}
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPages.Modelos;

namespace RazorPages25.Pages.ConsultasIA
{
    public class GeneralModel : PageModel
    {
        private readonly IAService _iaService;

        public GeneralModel(IAService iaService)
        {
            _iaService = iaService;
        }

        [BindProperty]
        public string Pregunta { get; set; }

        public string Respuesta { get; set; }

        public TablaDto? Tabla { get; set; }

        public async Task OnPostAsync()
        {
            if (!string.IsNullOrEmpty(Pregunta))
            {
                Respuesta = await _iaService.PreguntarAsync(Pregunta);
                if (!string.IsNullOrWhiteSpace(Respuesta))
                {
                    Tabla = JsonSerializer.Deserialize<TablaDto>(
                        Respuesta,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                }
            }
        }
    }
}
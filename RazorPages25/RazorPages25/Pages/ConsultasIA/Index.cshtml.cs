using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPages25.Pages.ConsultasIA
{
    public class IndexModel : PageModel
    {
        private readonly IAService _iaService;

        public IndexModel(IAService iaService)
        {
            _iaService = iaService;
        }

        [BindProperty]
        public string Pregunta { get; set; }

        public string Respuesta { get; set; }

        public async Task OnPostAsync()
        {
            if (!string.IsNullOrEmpty(Pregunta))
            {
                Respuesta = await _iaService.PreguntarAsync(Pregunta);
            }
        }
    }
}
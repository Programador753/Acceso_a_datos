using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPages25.Pages.Alumnos
{

    public class DetailsModel : PageModel
    {
        public int Id { get; set; }

        public void OnGet()
        {
            Id = int.Parse(RouteData.Values["id"].ToString());
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Platillos
{
    [Authorize (Roles ="2")]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using WMG.DVDCentral.BL;

namespace WMG.DVDCentral.UI.ViewComponents
{
    public class Sidebar : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(GenreManager.Load().OrderBy(p => p.Description));
        }
    }
}

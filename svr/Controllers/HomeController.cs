using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace image_storage.Controllers {

    [Route(Program.WebRoot)]
    public class HomeController : Controller {
        public ActionResult Get() {
            var text = System.IO.File.ReadAllText(Path.Combine(Program.AppDir, "../web/src-html/index.html"));
            text = text.Replace("{webPath}", Program.WebRoot);
            return Content(text, "text/html");
            if (false) return PhysicalFile(Path.Combine(Program.AppDir, "../web/src-html/index.html"), "text/html");
        }

        [HttpGet("CheckIfAdmin")]
        public ActionResult CheckIfAdmin() {
            return Json(this.User.IsInRole(Role.Admin.ToString()));
        }
    }
    
}
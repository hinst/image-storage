using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace image_storage.Controllers {

    [Route(Program.WebRoot)]
    public class HomeController : Controller {
        public FileResult Get() {
            return PhysicalFile(Path.Combine(Program.AppDir, "../web/src-html/index.html"), "text/html");
        }
    }
    
}
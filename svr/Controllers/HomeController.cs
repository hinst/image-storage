using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

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
        public JsonResult CheckIfAdmin() {
            return Json(this.User.IsInRole(Role.Admin.ToString()));
        }

        [HttpGet("GetImages")]
        public JsonResult GetImages() {
            var images = new ImageDB().Headers;
            images = images.OrderBy(image => image.Id.CreationTime);
            var ids = images.Select(image => image.Id.ToString());
            return Json(ids);
        }

        [HttpGet("GetImage")]
        public JsonResult GetImage(string id) {
            var image = new ImageDB().GetImageByIdString(id);
            if (image != null) {
                var webImage = new WebImage(image);
                return Json(webImage);
            }
            return Json(false);
        }
    }
    
}
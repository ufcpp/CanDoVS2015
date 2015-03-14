using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace KabeDon.Server.Controllers
{
    public class LevelController : Controller
    {
        //todo: 認証付ける
        //todo: ?name=... じゃない URL ルーティング書く

        // GET: Level
        public ActionResult Index(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                var path = Server.MapPath("~/App_Data/");
                var folders = Directory.GetFiles(path).Select(fullpath => Path.GetFileName(fullpath).Replace(".zip", ""));
                var content = string.Join(",", folders);
                return File(Encoding.UTF8.GetBytes(content), "text/plain");
            }
            else
            {
                // ファイル読み込み非同期にした方がいいかも。
                var path = Server.MapPath("~/App_Data/" + name + ".zip");
                return File(path, "application/zip");
            }
        }
    }
}
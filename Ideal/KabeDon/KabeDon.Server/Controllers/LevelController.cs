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
            // ファイル読み込み非同期にした方がいいかも。
            var path = Server.MapPath("~/App_Data/" + name + ".zip");
            return File(path, "application/zip");
        }
    }
}
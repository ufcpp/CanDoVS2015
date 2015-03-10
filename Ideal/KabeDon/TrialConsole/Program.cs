using KabeDon.Packaging;
using System.Threading.Tasks;

namespace TrialConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal));

            //CreateLevelData();
            //Load().Wait();
        }

        private static async Task Load()
        {
            var s = new Storage();
            var m = new PackageManager();
            await m.LoadFrom(s);
        }


        /// <summary>
        /// ゲームデータを仮に1個作ってみる。
        /// </summary>
        private static void CreateLevelData()
        {
            var data = MockData.Data;
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);

            //var deserialized = Newtonsoft.Json.JsonConvert.DeserializeObject<Level>(json);
            //var json2 = Newtonsoft.Json.JsonConvert.SerializeObject(deserialized);
            //System.Console.WriteLine(json == json2);

            System.IO.File.WriteAllText("level.json", json);
        }
    }
}

using KabeDon.Packaging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace TrialConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //CreateLevelData();
            Load().Wait();
        }

        private static async Task Load()
        {
            var s = new FileStorage("SampleData");
            var m = new PackageManager();
            await m.LoadFrom(s);

            var level = m.Level;
            Console.WriteLine(level.InitialState);
            foreach (var state in level.States) Console.WriteLine(state.Id);

            foreach (var x in m.ImageFiles) Console.WriteLine(x);
            foreach (var x in m.SoundFiles) Console.WriteLine(x);

            using (var stream = File.Open("test.zip", FileMode.Create))
                await m.Pack(s, stream);

            var s2 = new FileStorage("SampleData2");
            using (var stream = File.OpenRead("test.zip"))
                await m.Unpack(s2, stream);
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

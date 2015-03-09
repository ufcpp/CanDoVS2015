using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace KabeDon.XamarinForms.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            //var s = System.IO.File.OpenRead("./"); // これで、アプリのコンテンツ
            //var s = System.IO.File.OpenRead(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)); // これでアプリのローカル ストレージ

            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}

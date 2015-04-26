// For an introduction to the Blank template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkID=397705
// To debug code on page load in Ripple or on Android devices/emulators: launch your app, set breakpoints, 
// and then run "window.location.reload()" in the JavaScript Console.
var KabeDon;
(function (KabeDon) {
    "use strict";
    var Application;
    (function (Application) {
        function initialize() {
            document.addEventListener('deviceready', onDeviceReady, false);
            var image = document.getElementById("image");
            var i = 1;
            image.onmousedown = function (ev) {
                i++;
                if (i > 4)
                    i = 1;
                image.src = "images/Cloudia" + i + ".png";
            };
        }
        Application.initialize = initialize;
        function onDeviceReady() {
            // Handle the Cordova pause and resume events
            document.addEventListener('pause', onPause, false);
            document.addEventListener('resume', onResume, false);
            // TODO: Cordova has been loaded. Perform any initialization that requires Cordova here.
        }
        function onPause() {
            // TODO: This application has been suspended. Save application state here.
        }
        function onResume() {
            // TODO: This application has been reactivated. Restore application state here.
        }
    })(Application = KabeDon.Application || (KabeDon.Application = {}));
    window.onload = function () {
        Application.initialize();
    };
})(KabeDon || (KabeDon = {}));
// Platform specific overrides will be placed in the merges folder versions of this file 
//# sourceMappingURL=appBundle.js.map
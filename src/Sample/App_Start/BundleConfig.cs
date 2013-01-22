using System.Web.Optimization;

namespace Sample.App_Start
{
    public static class BundleConfig
    {
        public static void RegisterBundles()
        {
            RegisterScriptBundles(BundleTable.Bundles);
            RegisterStyleBundles(BundleTable.Bundles);
        }

        static void RegisterStyleBundles(BundleCollection bundles)
        {
            var toDo = new StyleBundle("~/css/todo")
                .Include(
                    "~/Content/reset.css",
                    "~/Content/toastr.css",
                    "~/Content/todo.css"
                )
                ;
            bundles.Add(toDo);
        }

        static void RegisterScriptBundles(BundleCollection bundles)
        {
            var jQuery = new ScriptBundle("~/resources/jquery")
                .Include(
                    "~/Scripts/jQuery-{version}.js"
                );
            bundles.Add(jQuery);
            var breeze = new ScriptBundle("~/resources/breeze")
                .Include(
                    "~/Scripts/q.js",
                    "~/Scripts/breeze.debug.js",
                    "~/Scripts/toastr.js"
                );
            bundles.Add(breeze);

            var angular = new ScriptBundle("~/resources/angular")
                .Include(
                    "~/Scripts/angular.js",
                    "~/Scripts/i18n/angular-locale_de-de.js"
                );
            bundles.Add(angular);

            var knockout = new ScriptBundle("~/resources/knockout")
                .Include(
                    "~/Scripts/knockout.debug.js",
                    "~/Scripts/knockout.mapping-latest.debug.js"
                );

            bundles.Add(knockout);
            var signalr = new ScriptBundle("~/resources/signalr")
                .Include(
                    "~/Scripts/jquery.signalr*"
                );
            bundles.Add(signalr);
            var toDo = new ScriptBundle("~/resources/todo")
                .Include(
                    "~/js/todo/logger.js",
                    "~/js/todo/dataservice.js",
                    "~/js/todo/controller.js"
                );
            bundles.Add(toDo);
        }
    }
}
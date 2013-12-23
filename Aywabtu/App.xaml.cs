using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Aywabtu.Command;
using Aywabtu.Properties;
using Aywabtu.ViewModel;

namespace Aywabtu {
    public partial class App : Application {

        protected override void OnStartup(StartupEventArgs e) {
            if (e.Args.Any()) {
                var file = e.Args[0];
                if (System.IO.File.Exists(file)) {
                    Settings.Default.LastFile = file;
                }

                if (e.Args.Count() > 1) {
                    var cmd = e.Args[1];
                    if (cmd.Equals("/arrange", StringComparison.OrdinalIgnoreCase)) {
                        var m = new MainViewModel();
                        var c = m.ArrangeCommand as ArrangeCommand;
                        if (c != null) {
                            c.ExecuteBlocking();
                        }
                        Shutdown();
                    }
                }
            }

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e) {
            Settings.Default.Save();
            base.OnExit(e);
        }

    }
}

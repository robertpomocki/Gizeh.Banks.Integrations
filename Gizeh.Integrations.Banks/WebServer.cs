using Microsoft.Owin.Hosting;
using System;

namespace Gizeh.Integrations.Banks
{
    public class WebServer
    {
        private IDisposable _webapp;

        public void Start()
        {
            _webapp = WebApp.Start<Startup>("http://localhost:9100");
        }

        public void Stop()
        {
            _webapp?.Dispose();
        }
    }
}

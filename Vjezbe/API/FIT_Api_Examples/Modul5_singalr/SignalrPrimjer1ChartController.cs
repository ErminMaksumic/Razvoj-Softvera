using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace FIT_Api_Examples.Modul5_singalr
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SignalrPrimjer1ChartController : ControllerBase
    {
        private IHubContext<ChartHub> _hub;

        public SignalrPrimjer1ChartController(IHubContext<ChartHub> hub)
        {
            _hub = hub;
        }

        [HttpPost]
        public IActionResult ApiPosaljiPodatke(int a, int b, int c, int d)
        {
            posaljiPorukuPremaKlijentuWS(a, b, c, d);

            return Ok();
        }

        [HttpPost]
        public IActionResult TimerPokreni()
        {
            var timer = new Timer((state) =>
            {
                Random r = new Random();
                posaljiPorukuPremaKlijentuWS(r.Next()%100, r.Next() % 100, r.Next() % 100, r.Next() % 100);
            });
            timer.Change(0, 1000);
            return Ok();
        }
        private void posaljiPorukuPremaKlijentuWS(int a, int b, int c, int d)
        {
            var podaci= new List<ChartModel>()
            {
                new ChartModel { Data = new List<int> { a }, Label = "Data1" },
                new ChartModel { Data = new List<int> { b }, Label = "Data2" },
                new ChartModel { Data = new List<int> { c }, Label = "Data3" },
                new ChartModel { Data = new List<int> { d }, Label = "Data4" }
            };
            _hub.Clients.All.SendAsync("transferchartdata", podaci);
        }
    }
}

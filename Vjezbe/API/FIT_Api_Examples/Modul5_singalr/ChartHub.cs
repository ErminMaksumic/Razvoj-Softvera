using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace FIT_Api_Examples.Modul5_singalr
{
    public class ChartHub : Hub
    {
        public async Task MetodaHub1(object data)
        {
            //zamjena za controler/akciju
            Console.WriteLine("poruka MetodaHub1: " + data);
        }
    }
}

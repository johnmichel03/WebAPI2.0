﻿using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JM.SCI.SalesPromo.Api
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://localhost:8500/";
            WebApp.Start<Startup>(url: baseAddress);
            Console.WriteLine("Press any key to stop running katana self hosting service");
            Console.ReadLine();
        }
    }
}

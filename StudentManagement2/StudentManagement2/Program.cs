using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace StudentManagement2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }
        /*
         控制台应用程序通常有一个Main方法
         为什么我们在ASP.NET Core Web应用程序中有一个Main方法
         ASP.NET Core应用程序在大部分情况下作为控制台应用程序
         启动Main()方法配置ASP并启动它到那时，它就变成了一个ASP.NET Core 网络应用程序
         通过配置Main()方法，然后启动ASP.NET Core，这时它就变成了一个ASP.NET Core web应用程序
         */

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args).ConfigureLogging(
                (hostingContext, logging) => 
                {
                    //logging.ClearProviders();
                    
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                    logging.AddDebug();
                    logging.AddEventSourceLogger();
                    //启动Nlog作为记录程序日志之一
                    logging.AddNLog();
                })
                .UseStartup<Startup>();

        /*
         1.在InProcess托管的情况下，CreateDefaultBuilder()方法调用UseIIS()
         方法并在IIS工作进程(w3wp.exe或iisexpress.exe)内托管应用程序
         2.从性能的角度看，InProcess托管比OutOfProcess托管提供了更高的请求吞吐量
         */

        /*
         什么是OutOfProcess托管
            有2个Web服务器-内部Web服务器和外部Web服务器
            内部Web服务器是Kestrel
            外部Web服务器可以是IIS，Nginx或Apache

        什么是Kestrel Web Server
            Kestrel是ASP.NET Core的跨平台Web服务器
            Kestrel本身可以用作边缘服务器
            Kestrel中用于托管应用程序的进程是dotnet.exe
         */
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StudentManagement2.Models;

namespace StudentManagement2
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration; 
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDbContext>(
                optionsAction:options=>options.UseSqlServer(_configuration.GetConnectionString("StudentDBConnection"))
                );
            //services.AddMvc().AddXmlSerializerFormatters();
            services.AddMvc().AddXmlSerializerFormatters();
            services.AddScoped<IStudentRepository, SQLStudentRepository>();

            //mvc core只包含了核心的mvc功能，
            //mvc 包含了依赖于mvc core以及相关的第三方常用的服务与方法
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else 
            {
                app.UseExceptionHandler("/Error");//拦截异常

                app.UseStatusCodePagesWithReExecute("/Error/{0}");//拦截404找不到的页面信息
                //app.UseStatusCodePagesWithRedirects("/Error/{0}");
            }

            //DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();

            //defaultFilesOptions.DefaultFileNames.Clear();
            //defaultFilesOptions.DefaultFileNames.Add("test.html");

            //app.UseDefaultFiles(defaultFilesOptions);

            //app.UseDefaultFiles();

            //FileServerOptions fileServerOptions = new FileServerOptions();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Clear();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Add("test.html");

            //app.UseFileServer(fileServerOptions);

            //app.UseFileServer();
            //默认index.html index.htm default.html default.htm

            //添加静态文件
            app.UseStaticFiles();

            //app.UseMvcWithDefaultRoute();

            app.UseMvc(routes =>
            {
                routes.MapRoute("default","{controller=Home}/{action=Index}/{id?}");
            });

            //app.UseMvc();

            //app.Use(async (context,next) =>
            //{
            //    context.Response.ContentType = "text/plain;charset=utf-8";

            //    await context.Response.WriteAsync("lalala");
            //    await next();
            //});

            app.Run(async (context) =>
            {
                //获取当前进程名
                //var processName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
                //获取appsettings.json中的值
                //var configVal = _configuration["MyKey"];

                //throw new Exception("您的请求在管道中发生了一些错误，请检查");
                await context.Response.WriteAsync("hahaha");
            });
        }
    }
}

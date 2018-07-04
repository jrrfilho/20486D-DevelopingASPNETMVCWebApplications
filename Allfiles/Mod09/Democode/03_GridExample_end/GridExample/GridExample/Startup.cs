﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GridExample.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GridExample
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ChessLeagueContext>(options =>
                   options.UseInMemoryDatabase("ChessLeagueDB"));

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, ChessLeagueContext chessLeagueContext)
        {
            chessLeagueContext.Database.EnsureDeleted();
            chessLeagueContext.Database.EnsureCreated();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "ChessRoute",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Chess", action = "Index" },
                    constraints: new { id = "[0-9]+" });
            });
        }
    }
}

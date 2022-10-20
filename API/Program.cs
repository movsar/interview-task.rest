using Data;
using Microsoft.AspNetCore.Diagnostics;
using System;

namespace API {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.RegisterServices();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            app.UseExceptionHandler(config => {
                config.Run(async context => {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";

                    var error = context.Features.Get<IExceptionHandlerFeature>();
                    if (error != null) {
                        var ex = error.Error;
                        await context.Response.WriteAsync("Something went wrong :(");
                    }

                });
            });

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
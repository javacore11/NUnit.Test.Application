
using Microsoft.AspNetCore.Http.Features;
using NUnit.Test.Application.Configuration;
using Serilog;

namespace NUnit.Test.Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //SQl server
            //Services to the Container
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            if (connectionString != null)
            {
                builder.Services.RegisterDataContext(connectionString);
            }
            builder.Services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });
            //Add support to logging with SERILOG
            //builder.Host.UseSerilog((context, configuration) =>
            //    configuration.ReadFrom.Configuration(context.Configuration));
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("Log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            //Add support to logging request with SERILOG
          //  app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
using Audit.Core;
using Audit.WebApi;

namespace AuditNetTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers().AddMvcOptions(options =>
                {
                    options.AddAuditFilter(auditConfig => auditConfig
                        .LogAllActions()
                        .WithEventType("API:{verb} {controller}/{action}")
                    );
                }
            );


            Audit.Core.Configuration.AddCustomAction(ActionType.OnEventSaving, scope =>
            {
                //scope.Event.GetWebApiAuditAction().ActionParameters.Clear();
            });

            Audit.Core.Configuration.JsonAdapter = new JsonNewtonsoftAdapter();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapControllers();

            app.Run();
        }
    }
}
using Swashbuckle.AspNetCore.SwaggerUI;

namespace EvaluacionTecnica.Presentation.WebApi.Extensions
{
    public static class AppExtensions
    {
        public static void UseSwaggerExtensions(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Evaluacion Tecnica API");
                opt.DefaultModelRendering(ModelRendering.Model);
            });
        }
    }
}

namespace multi_hosp_demo.MultiHosp
{
    public static class MultiHospExtension
    {
        public static void AddMultiHosp(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IMultiHospProvider, MultiHospProvider>();
        }

        public static IApplicationBuilder UseMultiHosp(this IApplicationBuilder app)
        {
            return app.UseMiddleware<HTTPMultiHospMiddleware>();
        }
    }
}
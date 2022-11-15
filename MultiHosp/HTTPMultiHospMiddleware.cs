namespace multi_hosp_demo.MultiHosp
{
    public class HTTPMultiHospMiddleware
    {
        private readonly RequestDelegate _next;
        public HTTPMultiHospMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IMultiHospProvider _multiHospProvider)
        {
            context.Request.Headers.TryGetValue("HospCode", out var hospCode);
            _multiHospProvider.SetHospCode(hospCode);

            await _next.Invoke(context);
        }
    }
}
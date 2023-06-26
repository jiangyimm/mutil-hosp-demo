using Microsoft.AspNetCore.Mvc;
using multi_hosp_demo.Entities;
using multi_hosp_demo.MultiHosp;
using Microsoft.EntityFrameworkCore;

namespace multi_hosp_demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MultiHospController : ControllerBase
    {
        QcContext _context;
        IServiceProvider _serviceProvider;

        IService _service;

        public MultiHospController(QcContext context,
         IServiceProvider serviceProvider,
         IMultiHospProvider multiHospProvider,
         IEnumerable<IService> services)
        {
            _context = context;
            _serviceProvider = serviceProvider;
            //_service = services.First(p => p.Key == multiHospProvider.GetHospCode());
        }

        [HttpGet("dynamic-service")]
        public async Task<IActionResult> DynamicService()
        {
            var result = _service.GetKey();
            return Ok(result);
        }

        /// <summary>
        /// 这时查询的是请求头传入的院区
        /// </summary>
        /// <returns></returns>
        [HttpGet("depts")]
        public async Task<IActionResult> GetDepts()
        {
            var depts = await _context.ExtDepts.AsNoTracking().ToListAsync();
            return Ok(depts);
        }

        /// <summary>
        /// 这时查询的是30001的院区
        /// </summary>
        /// <returns></returns> 
        [HttpGet("depts-task")]
        public async Task<IActionResult> GetDeptsByTask()
        {
            var depts = await Task.Run(async () =>
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var multiHospProvider = scope.ServiceProvider.GetRequiredService<IMultiHospProvider>();
                    multiHospProvider.SetHospCode("30001");

                    var context = scope.ServiceProvider.GetRequiredService<QcContext>();

                    var depts = await context.ExtDepts.AsNoTracking().ToListAsync();
                    return depts;
                }
            });
            return Ok(depts);
        }
    }
}
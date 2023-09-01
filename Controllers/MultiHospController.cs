using Microsoft.AspNetCore.Mvc;
using multi_hosp_demo.Entities;
using multi_hosp_demo.MultiHosp;
using Microsoft.EntityFrameworkCore;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using multi_hosp_demo.JobQueues;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;

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
         IService service
         //IEnumerable<IService> services
         )
        {
            _context = context;
            _serviceProvider = serviceProvider;
            _service = service;
            //_service = services.First(p => p.Key == multiHospProvider.GetHospCode());
        }
        [HttpPost("fast-job")]
        public async Task<IActionResult> AddFastEndpointJobs()
        {
            var job = new JobA
            {
                InpatId = "1234567890",
                RecordId = this.GetHashCode().ToString()
            };
            await job.QueueJobAsync();
            return Ok(job);
        }


        [HttpPost("todo-job")]
        public async Task<IActionResult> AddTodoJob()
        {
            var jobTodo = new JobTodo
            {
                VisitId = "ZY002323123",
                VisitType = 2,
                RecordId = "102223",
                CreateTime = DateTime.Now,
                OrgCode = "0101"
            };
            await _context.JobTodos.AddAsync(jobTodo);
            await _context.SaveChangesAsync();
            return Ok(jobTodo);
        }
        [HttpGet("todo-jobs")]
        public async Task<IActionResult> GetTodoJob()
        {
            var todoJobs = await _context.JobTodos.ToListAsync();
            return Ok(todoJobs);
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
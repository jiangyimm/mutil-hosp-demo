# 多院区方案

## 前端

request header&#x20;

HospCode:0001

## 后端

### 多院区中间件

#### 1.定义多院区provider

```c#
 public interface IMultiHospProvider
    {
        string GetHospCode();
        void SetHospCode(string hospCode);
    }
 class MultiHospProvider : IMultiHospProvider
    {
        private string _hospCode;
        public string GetHospCode()
        {
            return _hospCode;
        }

        public void SetHospCode(string hospCode)
        {
            _hospCode = hospCode;
        }
    }
```

#### 2.数据库上下文设置多院区过滤

```c#
private string _hospCode;
public QcContext(DbContextOptions<QcContext> options, IMultiHospProvider multiHospProvider) : base(options)
{
    _hospCode = multiHospProvider.GetHospCode();
}
```

```c#
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
  //在这里设置院区化的表,需要院区的entity请继承EntityBase；这里可以反射批量设置
   modelBuilder.Entity<ExternalEmpl>().HasQueryFilter(e => e.HospCode == _hospCode);
   //...
}
```

#### 3.HTTP多院区中间件

```c#
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
```

#### 4.在startup时AddMultiHosp和UseMultiHosp

```c#
public static class MultiHospExtension
{
   public static void AddMultiHosp(this IServiceCollection serviceCollection)
   {
      serviceCollection.AddScoped<IMultiHospProvider,MultiHospProvider>();
   }
        
   public static IApplicationBuilder UseMultiHosp(this IApplicationBuilder app)
   {
      return app.UseMiddleware<HTTPMultiHospMiddleware>();
   }
}
```

#### 5.使用说明

*   若是HTTP请求的业务，则上述中间件默认做了院区化；

*   若是后台任务类，在获取数据库的DbContext之前，需要根据业务数据来拿到院区，设置IMultiHospProvider；（考虑到获取患者的院区需要DbContext，两者自相矛盾，所以可以后台任务队列增加院区字段，分院区执行）

```c#
QcContext _context;
IServiceProvider _serviceProvider;

public MultiHospController(QcContext context, IServiceProvider serviceProvider)
{
 _context = context;
 _serviceProvider = serviceProvider;
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
```

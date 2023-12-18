using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using WebAPI.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

///

/*Trong cấu hình API Explorer, có thể có một số điều kiện mà bạn 
có thể cần kiểm tra để đảm bảo tính rõ ràng của các endpoint.
-->*/
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opts =>
    opts.ResolveConflictingActions(apiDec => apiDec.First())
);
//<--

///

//cấu hình DbContext kết nối với database SqlServer
//builder.Configuration.GetConnectionString("DefaultConnection")): 
// Lấy chuỗi kết nối từ cấu hình ứng dụng. 
// Thường thì bạn sẽ đặt chuỗi kết nối trong 
// file cấu hình (ví dụ: appsettings.json) 
// và sử dụng tên ("DefaultConnection" trong trường hợp này) 
// để lấy chuỗi kết nối từ file cấu hình.
//-->
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer( 
        builder.Configuration.GetConnectionString("DefaultConnection")) 
    );
//<--

///

// Thêm dịch vụ IUrlHelperFactory
builder.Services.AddSingleton<IUrlHelperFactory>(new UrlHelperFactory());

//Enable CORS
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});


//JSON Serializer
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
    .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver
    = new DefaultContractResolver());

builder.Services.AddControllers();

var app = builder.Build();

//Enable CORS
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// cấu hình thông báo lỗi 
if(app.Configuration.GetValue<bool>("UseDeveloperExceptionPage"))
    app.UseDeveloperExceptionPage();
else
    app.UseExceptionHandler("/error");


// Xác nhận rằng chỉ có một đoạn mã đăng ký cho mỗi hành động hoặc controller
app.MapGet("/error/test", () => { throw new Exception("test"); });

app.MapControllers();

app.MapControllerRoute(
    name: "authors",
    pattern: "api/authors/{action=Index}/{id?}",
    defaults: new { controller = "Authors" }
);

app.MapControllerRoute(
    name: "categories",
    pattern: "api/categories/{action=Index}/{id?}",
    defaults: new { controller = "Categories" }
);

app.MapControllers();

app.UseHttpsRedirection();
app.UseAuthorization();
app.Run();

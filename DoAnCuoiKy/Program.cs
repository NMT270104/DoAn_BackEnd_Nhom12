using Microsoft.EntityFrameworkCore;
using DoAnCuoiKy.Models;
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

var app = builder.Build();

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

app.UseHttpsRedirection();
app.UseAuthorization();
app.Run();

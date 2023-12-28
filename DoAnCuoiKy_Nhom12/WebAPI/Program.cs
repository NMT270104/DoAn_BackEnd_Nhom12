using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

///

/*Trong cấu hình API Explorer, có thể có một số điều kiện mà bạn 
có thể cần kiểm tra để đảm bảo tính rõ ràng của các endpoint.
-->*/
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opts =>{
    //opts.ParameterFilter<SortColumnFilter>();
    //opts.ParameterFilter<SortOrderFilter>();
    opts.ResolveConflictingActions(apiDec => apiDec.First());
    opts.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme{
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    opts.AddSecurityRequirement(new OpenApiSecurityRequirement{
        {
        new OpenApiSecurityScheme{
            Reference = new OpenApiReference{
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        Array.Empty<string>()
        }
    });
});

//<--

// Thêm định nghĩa cho NoCache trong cấu hình MvcOptions
builder.Services.AddMvc(options =>
{
    options.CacheProfiles.Add("NoCache", new CacheProfile
    {
        NoStore = true,
        Location = ResponseCacheLocation.None
    });
});

// Thêm dịch vụ Mvc
builder.Services.AddControllers();


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


builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
})
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = 
    options.DefaultChallengeScheme = 
    options.DefaultForbidScheme =
    options.DefaultScheme =
    options.DefaultSignInScheme =
    options.DefaultSignOutScheme =
    JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"]))
    };
});

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

builder.Services.AddSingleton<List<ShoppingCartItem>>();



var app = builder.Build();

// Enable CORS
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure error handling
if (app.Configuration.GetValue<bool>("UseDeveloperExceptionPage"))
    app.UseDeveloperExceptionPage();
else
    app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500; // or another error status code
        context.Response.ContentType = "application/json";

        var exception = context.Features.Get<IExceptionHandlerFeature>();
        if (exception != null)
        {
            // Log your exception here
            // var logger = context.RequestServices.GetRequiredService<ILogger<Startup>>();
            // logger.LogError($"Unexpected error: {exception.Error}");
        }

        await context.Response.WriteAsync("An unexpected fault happened. Try again later.").ConfigureAwait(false);
    });
});

app.UseStatusCodePagesWithReExecute("/error/{0}");


// Add authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

// Confirm that there is only one registration for each action or controller
app.MapGet("/error/test", () => { throw new Exception("test"); });

app.MapGet("/auth/test/1",
    [Authorize]
    [EnableCors("AllowOrigin")]
    [ResponseCache(NoStore = true)] () =>
    {
        return Results.Ok("You are authorized!");
    });

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

app.UseHttpsRedirection();

app.Run();


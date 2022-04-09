using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using WebAppMM;
using WebAppMM.Data;

var builder = WebApplication.CreateBuilder(args);

string domain = "https://miq3l.eu.auth0.com/";

builder.Services.AddControllers();

builder.Services.AddDbContext<ContactContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WebApiDb")));

builder.Services.AddMvc(options => options.EnableEndpointRouting = false);


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = "https://miq3l.eu.auth0.com/";
    options.Audience = "https://localhost:7215/";
    options.TokenValidationParameters = new TokenValidationParameters
    {
        NameClaimType = ClaimTypes.NameIdentifier
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("edit:contacts",
        policy => policy.Requirements.Add(new HasScopeRequirement("edit:contacts", domain)));
    options.AddPolicy("write:contacts",
        policy => policy.Requirements.Add(new HasScopeRequirement("write:contacts", domain)));
    options.AddPolicy("delete:contacts",
        policy => policy.Requirements.Add(new HasScopeRequirement("delete:contacts", domain)));
});

builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();

// 2. Enable authentication middleware
app.UseAuthentication();

app.UseCors(x => x.AllowAnyMethod()
                  .AllowAnyHeader()
                  .SetIsOriginAllowed(origin => true) // allow any origin
                  .AllowCredentials());

app.UseMvc(routes =>
{
    routes.MapRoute(
        name: "default",
        template: "{controller=Home}/{action=Index}/{id?}");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

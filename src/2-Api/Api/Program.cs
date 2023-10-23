using Api.IoC.Auth;
using Api.IoC.Blog;
using Auth.Data;
using Auth.Data.Seed;
using Auth.Models;
using Blog.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureIdentityDbContextJwtServices(builder.Configuration);
builder.Services.ConfigureCustomAuthServices();
builder.Services.ConfigureBlogDbContextServices(builder.Configuration);
builder.Services.ConfigureCustomBlogServices();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var dbContextIdentity = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
var dbContextBlog = scope.ServiceProvider.GetRequiredService<BlogDbContext>();
if (dbContextIdentity.Database.GetPendingMigrations().Any())
{
    dbContextIdentity.Database.Migrate();
}
if (dbContextBlog.Database.GetPendingMigrations().Any())
{
    dbContextBlog.Database.Migrate();
}
var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUserEntity>>();

new ConfigureInitialSeed(dbContextIdentity, userManager!).StartConfig();
        
app.Run();

using jean_edwards.Database;
using jean_edwards.Interfaces;
using jean_edwards.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<IMovieServices, MovieServices>(client=>{
    client.BaseAddress = new Uri(builder.Configuration["BaseAddress"]);
});

builder.Services.AddDbContext<AppDbContext>(context=>{
    context.UseInMemoryDatabase("MovieSearch");
});

builder.Services.AddCors (options => {
    options.AddDefaultPolicy (cors =>
        cors
        .WithOrigins (builder.Configuration["FrontendOrigin"])
        .WithHeaders ("Content-Type", "Authorization")
        .AllowAnyMethod ()
        .AllowCredentials ()
    );

    options.AddPolicy ("UnsafeCORSAllow", builder =>
        builder
        .WithHeaders ("Content-Type", "Authorization")
        .AllowAnyMethod ()
        .AllowAnyOrigin ()
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors ("UnsafeCORSAllow");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");;

app.Run();

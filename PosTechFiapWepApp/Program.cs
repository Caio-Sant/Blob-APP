using PosTechFiapWepApp.Services;
using PosTechFiapWepApp.Util;

#nullable disable

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddHttpClient<ImagensService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["BaseUrl"]);
});

builder.Services.Configure<ApiImagensConfig>(configuration.GetSection("ApiImagensConfig"));

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

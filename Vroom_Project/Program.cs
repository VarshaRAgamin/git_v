using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Vroom_Project.AppDbContext;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddDbContext<VroomDbContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString(("Default"))));
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.WebHost.UseUrls("http://0.0.0.0:80");
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<VroomDbContext>();
    dbContext.Database.Migrate();
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
// Define rewrite options
var rewriteOptions = new RewriteOptions()
    .AddRewrite(@"^bankmvc/(.*)", "$1", skipRemainingRules: true);

app.UseRewriter(rewriteOptions);

app.UseHttpsRedirection();

app.UsePathBase(
"/bankmvc/"
);
app.UseRouting();
app.UseStaticFiles();

app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Index}/{id?}");

app.Run();

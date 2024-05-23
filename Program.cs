using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using ESA.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(); //MVC yapısı için gerekli servis
builder.Services.AddDbContext<DataContext>( //DataContext sınıfımınız DbContext olarak tanımlar
    Options=>{
        var config=builder.Configuration; 
        var conString=config.GetConnectionString("Database");
        Options.UseSqlite(conString);
        Options.EnableSensitiveDataLogging();
        });

var app = builder.Build();




app.UseRouting(); //Routing kullanmamızı sağlar


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}"); //varsayılan rotamız home/İndex sayfası
 app.MapControllerRoute(
     name: "product",
     pattern: "{controller=product}/{action=list}/{id?}");
app.MapControllerRoute(
    name: "Buyproduct",
    pattern: "{controller=product}/{action=BuyProduct}/{id?}");



app.Run(); //uygulama çalıştırılır
using ESA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ESA.Controllers{
    public class HomeController : Controller
{
    public IActionResult Index()
    {
            //  using(var db=new DataContext()){
            //      var products=db.Products;
            //      foreach(var p in products){
            //          db.Remove(p);
            //      }
            //      db.SaveChanges();
            //      db.Database.ExecuteSqlRaw("UPDATE sqlite_sequence SET seq = 0 WHERE name = 'Products';");
            //  }
          return View();
    }
    public IActionResult Login()
    {
        return View("Login");
    }
    [HttpPost]
    public IActionResult Login(User user) //User sınıfından user adında bir nesne alır 
    {
        using(var db=new DataContext()){ //Veri tabanına bağlantıyı sağlayan DataContext sınıfından db nesnesi oluşturrak veri tabanına bağlanırız
            var existingProduct = db.Users.FirstOrDefault(u => u.Email == user.Email); //veri tabanındaki elemanları tarayarak dışarıdan girilen user nesnesinin email'i ile veritabanında aynı emaile sahip user var mı kontrol eder
            if(existingProduct==null){ //kontrol sonucunu existingProduct değişkenine atar ve bu değişken null ise
            db.Add(user); //user nesnesini veritabanına ekler
            db.SaveChanges(); //değişiklikleri kaydeder
            }
            else{
                
            }
        }
        return View(); //sayfayı döndürür
    }
    public IActionResult Contacts()
    {
        return View("Contacts");
    }

    [HttpPost]
    public IActionResult Contacts(Comment comment)
    {
       //yukarıdakiyle aynı mantık sadece değişken isimleri farklı 
        using(var db=new DataContext()){
            var existingUser=db.Users.FirstOrDefault(u=>u.Email==comment.Email && u.Username==comment.Username);
            if(existingUser!=null){

                comment.User = existingUser;
                db.Add(comment);
                db.SaveChanges();
            }
            else{}
        }
        return View();
    }
}
}
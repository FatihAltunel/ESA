using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using ESA.Models;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;


namespace ESA.Controllers{
    public class ProductController : Controller
{
    //ürünlerimizi içine atacağımız paket olan _productViewModel'i oluşturur
    private readonly ProductViewModel _productViewModel = new ProductViewModel();

    public IActionResult List(string q) //List metodu string bir sorgu alır böylece search yaptığımız sayfa girdiğimiz q stringi için yenilenir
    {
        // var p=new List<Product>(){
        //     new Product{Brand="Samsung",Type="S21",Price=42500,ImgURL="https://cdn.dsmcdn.com/ty421/product/media/images/20220507/19/105380999/471065042/1/1_org_zoom.jpg", ProductID=1},
        //     new Product{Brand="Samsung",Type="S20",Price=34900,ImgURL="https://cdn.dsmcdn.com/ty342/product/media/images/20220225/19/58545613/397791270/1/1_org_zoom.jpg",ProductID=2},
        //     new Product{Brand="Reeder",Type="S19",Price=23680,ImgURL="https://cdn.dsmcdn.com/ty508/product/media/images/20220815/16/160249311/510712398/1/1_org_zoom.jpg",ProductID=3},
        //     new Product{Brand="Iphone",Type="11",Price=14750,ImgURL="https://st-troy.mncdn.com/mnresize/1500/1500/Content/media/ProductImg/original/mhdh3tua-apple-iphone-11-128gb-siyah-mhdh3tua-637610881803462641.jpg",ProductID=4},
        //     new Product{Brand="Iphone",Type="13 Pro",Price=32750, ImgURL="https://reimg-teknosa-cloud-prod.mncdn.com/mnresize/600/600/productimage/790181795/790181795_0_MC/9ad71e93ee4b4cea98421fae3ef08666.jpg",ProductID=5},
        //     new Product{Brand="Iphone",Type="14 Pro",Price=32750,ImgURL="https://st-troy.mncdn.com/mnresize/1500/1500/Content/media/ProductImg/original/mq323tua-apple-iphone-14-pro-1tb-derin-mor-mq323tua-637987568976469929.jpg",ProductID=6},
        //     new Product{Brand="Iphone",Type="15 Pro",Price=32750,ImgURL="https://st-troy.mncdn.com/mnresize/1500/1500/Content/media/ProductImg/original/mtv03tua-apple-iphone-15-pro-128gb-blue-titanium-638305571226661972.jpg",ProductID=7},
        //     new Product{Brand="Iphone",Type="14",Price=32750,ImgURL="https://st-troy.mncdn.com/mnresize/1500/1500/Content/media/ProductImg/original/mpvn3tua-apple-iphone-14-128gb-mavi-mpvn3tua-638139798986933432.jpg",ProductID=8}
        // };
        if(!string.IsNullOrEmpty(q)){ //eğer q stringine bir değer girilmişse

#pragma warning disable CS8602 // Dereference of a possibly null reference.
                var p=_productViewModel.Products_;  //productviewmodel paketimizde oluşturduğumuz ürünleri p değişkenine atarız
                p =p.Where(i=>i.Brand.ToLower().Contains(q.ToLower())|| i.Type.ToLower().Contains(q.ToLower())).ToList(); //p içindeki elemanlardan where metoduyla arama yaparız. gönderdiğiniz q stringiyle p elemanlarının brand ya da type özellikleri eşleşiyorsa bunları Tolist medoyula listeleriz ekrana bunlar çıkar
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                _productViewModel.Products_=p; //Yukarıda seçip p içinde listelediğimiz ürünleri tekrardan paketimize atarız ki paketimiz bunlardan ibaret gibi gözükür ve sadece p'De listelenen ürünleri ekrana gösterir
                return View(_productViewModel);
            }
                //    using(var db=new DataContext()){
                //     foreach (var product in p)
                //     {  
                //         var existingProduct = db.Products.FirstOrDefault(p => p.ProductID == product.ProductID);

                //        if (existingProduct == null)
                //         {                
                //             db.Products.Add(product);
                //             db.SaveChanges();
                //         }
                //         else
                //        {
                //        }
                //     }
                //   }
        _productViewModel.AddProductToDatabase(); //fonskiyon isminden anlaşılmıştır

        return View(_productViewModel); //list sayfasına _productviewmodel paketimiz gider. Sayfa bu paketteki içerikleri kullanarak oluşturulacak
   
    }
    public IActionResult BuyProduct(){
        return View();
    }

    [HttpPost] //Bu action'ın bir form vb ile post değer aldığını tanımlarız.
    public IActionResult BuyProduct(int ProductId)
    {
            int variable=ProductId;
            using var db = new DataContext();
            var product = db.Products.FirstOrDefault(p => p.ProductID == ProductId); //veritabanında ID'si Fonksiyona gönderdiğimiz ID'ye eşit olan ürünü seçip onu product nesnesine atarız
            if (product != null) //eğer product null değil ise yani öyle bir ürün varsa
            {
                _productViewModel.Products_.Remove(product); //product view model paketimizden product nesnesini sileriz
                db.Products.Remove(product); //veritabanından sileriz
                db.SaveChanges(); 
                return View(product); //Buy Now'a tıkladığımız zaman açılacak BuyProduct.cshtml dosyası bu product nesnesinin bilgilerini alıp ekrana yazdırır
            }
            else
            {
                return View("ProductNotFound"); //Böyle bir ID yoksa product not found sayfasına yönlendiriliriz
            }
        }
        public IActionResult AddProduct(){
            return View();
        }
    [HttpPost]
    public IActionResult AddProduct(Product product){
        using(var db=new DataContext()){
            int HowManyProducts = db.Products.Count(); //veritabanındaki ürünlerden ne kadar olduğunu sayan count() fonskiyonunu kullanrak bu sayıyı değişkene atarız
        new Product(){
            Brand=product.Brand,
            Type=product.Type,
            Price=product.Price,
            ImgURL=product.ImgURL,
            ProductID=HowManyProducts //değişkene atadığımız sayıyı yeni ürüne ID olarak atarız
        };
        _productViewModel.AddToProductViewModel(product); //yeni ürünü paketimize yükleriz
        }
        return View("list",_productViewModel); //list sayfasına yeni,güncellenmiş paketimizi göndeririz
    }
}
}
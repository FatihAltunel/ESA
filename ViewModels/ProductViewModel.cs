using ESA.Models;
using System.Collections.Generic; 
public class ProductViewModel{ //ürünleri içine atacağımız paket
    public List<Product> Products_ { get; set; } //bir liste oluşturup ürünleri içine atarız
    public ProductViewModel(){
        var p=new List<Product>(){
            new Product{Brand="Samsung",Type="S21",Price=42500,ImgURL="https://cdn.dsmcdn.com/ty421/product/media/images/20220507/19/105380999/471065042/1/1_org_zoom.jpg", ProductID=1},
            new Product{Brand="Samsung",Type="S20",Price=34900,ImgURL="https://cdn.dsmcdn.com/ty342/product/media/images/20220225/19/58545613/397791270/1/1_org_zoom.jpg",ProductID=2},
            new Product{Brand="Reeder",Type="S19",Price=23680,ImgURL="https://cdn.dsmcdn.com/ty508/product/media/images/20220815/16/160249311/510712398/1/1_org_zoom.jpg",ProductID=3},
            new Product{Brand="Iphone",Type="11",Price=14750,ImgURL="https://st-troy.mncdn.com/mnresize/1500/1500/Content/media/ProductImg/original/mhdh3tua-apple-iphone-11-128gb-siyah-mhdh3tua-637610881803462641.jpg",ProductID=4},
            new Product{Brand="Iphone",Type="13 Pro",Price=32750, ImgURL="https://reimg-teknosa-cloud-prod.mncdn.com/mnresize/600/600/productimage/790181795/790181795_0_MC/9ad71e93ee4b4cea98421fae3ef08666.jpg",ProductID=5},
            new Product{Brand="Iphone",Type="14 Pro",Price=32750,ImgURL="https://st-troy.mncdn.com/mnresize/1500/1500/Content/media/ProductImg/original/mq323tua-apple-iphone-14-pro-1tb-derin-mor-mq323tua-637987568976469929.jpg",ProductID=6},
            new Product{Brand="Iphone",Type="15 Pro",Price=32750,ImgURL="https://st-troy.mncdn.com/mnresize/1500/1500/Content/media/ProductImg/original/mtv03tua-apple-iphone-15-pro-128gb-blue-titanium-638305571226661972.jpg",ProductID=7},
            new Product{Brand="Iphone",Type="14",Price=32750,ImgURL="https://st-troy.mncdn.com/mnresize/1500/1500/Content/media/ProductImg/original/mpvn3tua-apple-iphone-14-128gb-mavi-mpvn3tua-638139798986933432.jpg",ProductID=8}
        };
        Products_=p; //yukarıdaki p listesini paketin içine atar
    }
    public void AddProductToDatabase(){ //paketin içindeki ürünleri veritabanına aktarır
        using(var db=new DataContext()){
            var p=Products_;
                    foreach (var product in p)
                    {  
                        var existingProduct = db.Products.FirstOrDefault(p => p.ProductID == product.ProductID); //veritabanındaki ürünleri ID'si ile pakete attığımız ürünlerin ID'sini kıyaslayarak ID'Leri aynı olan bir ürün arar

                       if (existingProduct == null) //eğer aynı ID'li bir ürün bulamazsa
                        {                
                            db.Products.Add(product); //yeni ürünü ekler
                            db.SaveChanges();
                        }
                        else
                       {
                       }
                    }
                  }
    }
    public void AddToProductViewModel(Product product){ //Pakete yeni ürün ekleme fonksiyonu
        Products_.Add(product);
        using(var db=new DataContext()){
            db.Products.Add(product);       //veritabanını günceller
            db.SaveChanges();
        }
    }
}
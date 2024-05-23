using Microsoft.EntityFrameworkCore;

namespace ESA.Models{
    public class DataContext:DbContext{ //DbContext sınıfına bizi veritabanına bağlar bu yüzden o sınıftan kalıtım alarak yeni sınıfımızı oluştururuz
        public DataContext(DbContextOptions<DataContext>options):base(options){
            
        }

        public DataContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) //Sınıfın bizi veritabanına bağladığı yer
        {
            optionsBuilder.UseSqlite("Data Source=Database.db"); //Database.db adında bir veri tabanı oluşturur ve bizi ona bağlar
            optionsBuilder.EnableSensitiveDataLogging(); //bunun işlevini ben de tam bilmiyorum 
        }
        public DbSet<Product>Products{get;set;} //veritabanında Products tablosu oluşturur
        public DbSet<User>Users{get;set;} 
        public DbSet<Comment>Comments{get;set;} 
    }
}
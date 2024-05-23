using System.ComponentModel.DataAnnotations;

namespace ESA.Models{
    public class User{
        public int UserId{get;set;}

         [Required] //rquired yerler bu sınıftan yeni bir nesne oluşturuldu doldurulması gereken yerlerdir
        public string Username{get;set;}=string.Empty;
         [Required]
        public string? Email{get;set;}=string.Empty;
         [Required]
         [DataType(DataType.Password)] //verinin bir password olduğunu tanımlarız
        public string? Password{get;set;}=string.Empty;
        
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESA.Models{
    public class Product{
        [Key]//ProductID'nin primary key olduğunu tanımlarız
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID{get;set;}
        public string? Brand{get;set;}
        public string? Type{get;set;}
        public decimal Price{get;set;}
        
        [Required(ErrorMessage = "ImgURL is Cumpolsory")]
        public string ImgURL { get; set; } = string.Empty;
    }
}
using System.ComponentModel.DataAnnotations;

namespace ESA.Models{
    public class Comment{
        public int CommentId{get;set;}
         [Required]
        public string Username{get;set;}=string.Empty;
         [Required]
        public string Email{get;set;}=string.Empty;
        public string CommentLine{get;set;}=string.Empty;
        public  User? User { get; set; }
    }
}
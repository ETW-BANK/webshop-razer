using System.ComponentModel.DataAnnotations;

namespace webshoping.Models
{
    public class Catagory
    {
        [Key]
        public int CatagoryId { get; set; }

        [Required]
       public string CatagoryName { get; set; }

        public string CatagoryDescription { get; set; }

        public int DisplayOrder { get; set; }   



    }
}

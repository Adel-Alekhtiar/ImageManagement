using System.ComponentModel.DataAnnotations;

namespace ImageManagmentAPI.Models
{
    public class ImagesModel
    {
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Image list is  required")]
        public List<string> Base64EncodedImageString { get; set; }    
    }
}

using DAL.Services;
using ImageManagmentAPI.Models;
using Microsoft.AspNetCore.Mvc;
using DAL;
namespace ImageManagmentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[Action]")]
    public class ManageImagesController : Controller
    {
        private readonly IManageImagesService _manageImagesService;
        public ManageImagesController( IManageImagesService manageImagesService)
        {
            _manageImagesService = manageImagesService;
        }
        /// <summary>
        /// using model for better validation and scalability and utilizing the autmatic model validation of asp.net core 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UploadImages([FromBody] ImagesModel model)
        {
            //find custoner/lead
            var contact = await _manageImagesService.GetContact(model.Id);
            var images = new List<Image>();
            if (contact !=null )
            {
                var imagesCount = model.Base64EncodedImageString.Count;
                if (imagesCount > 0)
                {
                    if (contact.AvailabeImageSlots >= imagesCount)
                    {
                        foreach (var imageString in model.Base64EncodedImageString)
                        {
                            var image = new Image
                            {
                                ContactBase = contact,
                                Base64EncodedImageString = imageString
                            };
                            images.Add(image);
                        }
                        var addedImages = await _manageImagesService.AddImageAsync(images);
                        return Ok(addedImages);
                    }
                    return Ok(new { Success = false, Massage = $"Error: You have only {contact.AvailabeImageSlots} image slots available, but you are trying to upload {imagesCount} images." });
                }
                return Ok(new { Success = false, Massage = $"Error:images list should hae at least 1 image string " });

            }
            return Ok(new {Success =false,Massage= $"Error: Cant finde a lead or customer with this id ={model.Id} "});
        }
        [HttpDelete("{imageId}")]
        public async Task<IActionResult> DeleteImage(int imageId)
        {
            var result = await _manageImagesService.DeleteImageAsync(imageId);
            if (result)
            {
                return Ok(new { Success = true, Massage = "Image deleted successfully." });
            }
            return Ok(new { Success = false, Massage = $"Error: No image found with ID {imageId}." });
        }
        [HttpGet("{contactId}")]
        public async Task<IActionResult> GetContactImages(int contactId)
        {
            var images = await _manageImagesService.GetContactImagesAsync(contactId);
            if (images != null && images.Count > 0)
            {
                return Ok(images);
            }
            return Ok(new { Success = false, Massage = $"Error: No images found for contact ID {contactId}." });
        }
    }
}

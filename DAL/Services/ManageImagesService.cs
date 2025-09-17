using DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace DAL.Services
{
    public class ManageImagesService : IManageImagesService
    {
        private readonly ApplicationContext _context;
        public ManageImagesService( ApplicationContext applicationContext)
        {
            _context = applicationContext;
        }
        public async Task<List<Image>>AddImageAsync( List<Image> images)
        {
            foreach (var image in images)
            {
               await  _context.Images.AddAsync(image);
            }
            await _context.SaveChangesAsync();
            return images;
        }

        public async Task<bool> DeleteImageAsync(int imageId)
        {
            var image = _context.Images.FirstOrDefault(i => i.Id == imageId);
            if (image != null)
            {
                 _context.Images.Remove(image);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public Task<ContactBase> GetContact(int contactId)
        {
            var contact = _context.ContactBase.FirstOrDefaultAsync(c=> c.Id == contactId);           
            return contact;
        }

        public async Task<List<Image>> GetContactImagesAsync(int contactId)
        {
            var image = await _context.Images.Where(i => i.ContactBaseId == contactId).ToListAsync();
            return image;
        }
    }
}

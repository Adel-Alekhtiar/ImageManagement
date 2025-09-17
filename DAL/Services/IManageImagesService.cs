namespace DAL.Services
{
    public interface IManageImagesService
    {
        Task<ContactBase> GetContact(int contactId);
        Task<List<Image>> AddImageAsync(List<Image> images);
        Task<bool> DeleteImageAsync(int imageId);
        Task<List<Image>> GetContactImagesAsync(int contactId);
    }
}

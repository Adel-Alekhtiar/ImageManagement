using System.Text.Json.Serialization;

namespace DAL
{
    public class Image
    {
        public int Id { get; set; }
        public string Base64EncodedImageString { get; set; }
        public int ContactBaseId { get; set; }
        [JsonIgnore] // Prevent circular reference during JSON serialization
                     // for some reason its not always working using the annotation 
        public ContactBase ContactBase { get; set; }
    }
}

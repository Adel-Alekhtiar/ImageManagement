using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// base class for customers and leads
    /// </summary>
    public abstract  class ContactBase
    {
        [NotMapped]
        const int MaxImageSlots = 10;
        public int Id { get; set; }
        [Required(ErrorMessage = " name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage ="email is required")]
        [EmailAddress(ErrorMessage ="invalid email format")]
        public string Email { get; set; }
        public string? Address { get; set; }
        [NotMapped]
        public int AvailabeImageSlots {
            get => MaxImageSlots - Images.Count ; 
        }
        public List<Image>? Images { get; private set; }
        public ContactBase()
        {
            Images = new List<Image>();
        }
    }
}

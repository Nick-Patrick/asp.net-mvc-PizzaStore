using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PizzaShop1.Domain.Entities
{
    public class UserDetail
    {
        [Key]
        public int UserDetailId { get; set; }
       // public string Username { get; set; }
        public string Password { get; set; }
        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter an address")]
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        [Required(ErrorMessage = "Please enter a city")]
        public string City { get; set; }
        public string County { get; set; }
        [Required(ErrorMessage = "Please enter a postcode")]
        public string Postcode { get; set; }
        public string Telephone { get; set; }

    }
}

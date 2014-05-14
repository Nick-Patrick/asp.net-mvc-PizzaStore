using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaShop1.Domain.Entities
{
    public class Orderline
    {
        [Key]
        public int OrderlineId {get;set;}

        public int PizzaId {get;set;}
        public decimal OrderlinePrice {get;set;}
        public int UserId {get;set;}
        public int OrderId {get;set;}
    }
}

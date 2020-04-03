using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FantasticBookStore.Models
{
    public class cart
    {
        [Key]


        public string cartId { get; set; }

        public string userId { get; set; }
        public string bookId { get; set; }
        public string bookName { get; set; }
        public string categoryName { get; set; }

        public double price { get; set; }

        public string authorName { get; set; }

    }
}
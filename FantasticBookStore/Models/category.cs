using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FantasticBookStore.Models
{
    public class category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string catId { get; set; }
        public string catName { get; set; }

    }
}
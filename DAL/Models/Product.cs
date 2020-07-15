using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    /// <summary>
    /// Object Product
    /// </summary>
    public class Product:BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}

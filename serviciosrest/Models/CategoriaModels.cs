using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace serviciosrest.Models
{
    public class CategoriaModels
    {

        public int codigo { get; set; }
        public string nombre { get; set; } = string.Empty;
        public bool estado { get; set;}
    }
}
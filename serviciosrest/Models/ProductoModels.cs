using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace serviciosrest.Models
{
    public class ProductoModels
    {
        public int codpro { get; set; }
        public string nompro { get; set; }
        public string despro { get; set; }
        public decimal prepro { get; set; }
        public decimal canpro { get; set; }
        public bool estpro { get; set; }
        public int codcat { get; set; }

        public virtual categoria categoria { get; set; }
    }
}
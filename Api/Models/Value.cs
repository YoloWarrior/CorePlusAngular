using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CorePlusAngular.Models
{
    public class Value
    {
       
        public int Id { get; set; }
        [Required]
    [MaxLength(50)]
        public string Name { get; set; }

       
        
    }
   
}

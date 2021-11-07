using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cryptocurrency.Agent.Server.Models
{
    public class Symbol
    {
        [Required]
        public string Name { get; set; }
    }
}

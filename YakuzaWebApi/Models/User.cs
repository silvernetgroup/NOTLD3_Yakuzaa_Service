using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YakuzaWebApi.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public int SessionId { get; set; }
        public string Username { get; set; }
        public string RoleName { get; set; }
        public bool isInMafia { get; set; }
        public bool isKilled { get; set; }
    }
}

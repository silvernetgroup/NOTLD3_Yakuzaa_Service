using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YakuzaWebApi.Models
{
    public class GameEvent
    {
        [Key]
        public int Id { get; set; }
        public int GameSessionId { get; set; }
        public string Username { get; set; }
        public string TextContent { get; set; }
        public DateTime Date { get; set; }
        public bool isOnlyForMafia {get;set;}
    }
}

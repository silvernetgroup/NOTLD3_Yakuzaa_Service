using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YakuzaWebApi.Models
{
    public class Vote
    {
        public int Id { get; set; }
        public string VotedUsername { get; set; }
        public int GameSessionId { get; set; }
    }
}
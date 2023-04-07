using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class UserEvents
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public Guid EventId { get; set; }
        public Event Event { get; set; }
        public bool IsHost { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateJoined { get; set; }
    }
}

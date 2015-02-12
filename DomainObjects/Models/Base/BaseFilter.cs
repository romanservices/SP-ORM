using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObjects.Models
{
    public class BaseFilter
    {
        public string CurrentSessionId { get; set; }
        public string CurrentUserName { get; set; }
        public int CurrentUserId { get; set; }
        [System.ComponentModel.DefaultValue(0)]
        public int Index { get; set; }
      
    }
}

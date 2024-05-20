using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public string PropertyName { get; set; }
        public EntityNotFoundException(string propertyname,string? message) : base(message)
        {
            PropertyName = propertyname;
        }
    }
}

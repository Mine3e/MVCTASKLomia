using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Exceptions
{
    public class TeamNotFoundException : Exception
    {
        public string PropertyName {  get; set; }
        public TeamNotFoundException(string propertyname,string? message) : base(message)
        {
            PropertyName = propertyname;
        }
    }
}

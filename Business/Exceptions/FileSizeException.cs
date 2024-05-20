using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Exceptions
{
    public class FileSizeException : Exception
    {
        public string PropertyName { get; set; }
        public FileSizeException(string propertyname,string? message) : base(message)
        {
            PropertyName = propertyname;
        }
    }
}

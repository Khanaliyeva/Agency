using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Business.Exceptions
{
    public class PortfoleNotFoundException:Exception
    {
        public PortfoleNotFoundException() : base("Bele portfolie yoxdu")
        {

        }

        public PortfoleNotFoundException(string message) : base(message)
        {

        }
    }
}

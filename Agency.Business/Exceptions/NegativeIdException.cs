using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Business.Exceptions
{
    public class NegativeIdException:Exception
    {
        public NegativeIdException():base("menfi id olmaz")
        {

        }

        public NegativeIdException(string message):base(message)
        {

        }
    }
}

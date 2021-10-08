using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
   public class ResourceNotFoundException:Exception
    {
        public ResourceNotFoundException(string message):base(message)
        {

        }
    }
}

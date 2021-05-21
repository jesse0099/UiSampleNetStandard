using System;
using System.Collections.Generic;
using System.Text;

namespace UiSampleMigrat.MyExceptions
{
    public class ClientProfileException:Exception
    {
        public ClientProfileException()
        {
        }

        public ClientProfileException(string message)
            : base(message)
        {
        }

        public ClientProfileException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

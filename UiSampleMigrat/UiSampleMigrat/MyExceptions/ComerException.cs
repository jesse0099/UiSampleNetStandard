using System;
using System.Collections.Generic;
using System.Text;

namespace UiSampleMigrat.MyExceptions
{
    public class ComerException:Exception
    {
        public ComerException()
        {
        }

        public ComerException(string message)
            : base(message)
        {
        }

        public ComerException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

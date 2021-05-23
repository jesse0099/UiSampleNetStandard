using System;
using System.Collections.Generic;
using System.Text;

namespace UiSampleMigrat.MyExceptions
{
    public class CategoryException: Exception 
    {
        public CategoryException()
        {

        }

        public CategoryException(string message)
                        : base(message)
        {
        }

        public CategoryException(string message, Exception inner)
            :base(message,inner)
        {

        }

    }
}

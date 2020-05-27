using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiSampleMigrat.Helpers
{
    public static class TaskHelper
    {
        //Este metodo permite la llamada de metodos asincronos en el constructor
        //Donde no pueden ser awaited,por lo que las excepciones pasan por debajo


        public static async void SafeFireAndForget(this Task task, bool returnToCallingContext,
            Action<Exception> onException = null)
        {
            try
            {
                await task.ConfigureAwait(returnToCallingContext);
            }
            catch (Exception ex) when (onException != null)
            {
                onException(ex);
            }
        }
    }
}

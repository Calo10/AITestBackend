using System;
using AITestBackend.Models;

namespace AITestBackend.Helpers
{
    public class ErroMessagesTranslate
    {
        public ErroMessagesTranslate()
        {
        }

        public static string TranslateException(Exception ex)
        {
            switch (ex.HResult)
            {
                default:

                    return AppManagement.MSG_GenericExceptionError;
                    break;
            }
        }
    }
}

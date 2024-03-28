using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Smart_Manufacturing
{
    public class DensoThread
    {
        private string pos;

        // The constructor obtains the state information and the
        // callback delegate.
        private densoCallBack callback;


        public DensoThread()
        {

        }



    }

    public delegate void densoCallBack(int lineCount);
}

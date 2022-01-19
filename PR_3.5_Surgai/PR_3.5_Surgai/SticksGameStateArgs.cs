using System;
using System.Collections.Generic;
using System.Text;

namespace PR_3._5_Surgai
{
    class SticksGameStateArgs
    {
        public string Message { get; }
        public int SticksNumber;
        public int RemainingSticks;

        public SticksGameStateArgs(string message, int sticksNumber, int remainingSticks)
        {
            this.Message = message;
            this.SticksNumber = sticksNumber;
        }
    }
}

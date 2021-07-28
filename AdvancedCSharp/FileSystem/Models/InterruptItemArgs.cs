using System;

namespace FileSystem.Models
{
    public class InterruptItemArgs: EventArgs
    {
        public bool IsInterrupt { get; set; }
    }
}

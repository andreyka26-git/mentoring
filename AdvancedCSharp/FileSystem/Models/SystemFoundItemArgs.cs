using System;

namespace FileSystem.Models
{
    public class SystemFoundItemArgs : EventArgs
    {
        public SystemItemModel Item { get; set; }

        public bool IsDelete { get; set; }
        public bool IsInterrupt { get; set; }
    }
}
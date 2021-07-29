using System;

namespace FileSystem.Models
{
    public class SystemFoundItemArgs : EventArgs
    {
        public SystemItemModel Item { get; set; }

        public bool IsDeleteItem { get; set; }
        public bool IsInterrupt { get; set; }
    }
}
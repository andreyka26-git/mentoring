using System;

namespace FileSystem.Models
{
    public class SystemFoundItemArgs: EventArgs
    {
        public SystemItemModel Item { get; set; }
    }
}
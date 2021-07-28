using System;

namespace FileSystem.Models
{
    public class SystemItemArgs: EventArgs
    {
        public SystemItemModel Item { get; set; }
        public bool IsInterruptedProcess { get; set; }
        public bool IsDeleteFiles { get; set; }
        public bool IsDeleteFolders { get; set; }
    }
}
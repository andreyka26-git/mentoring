using System;
using System.Collections.Generic;

namespace FileSystem.Models
{
    public class SystemItemArgs: EventArgs
    {
        public SystemItemModel Items { get; set; }
        public bool IsInterruptedProcess { get; set; }
        public bool IsDeleteFiles { get; set; }
        public bool IsDeleteFolders { get; set; }
    }
}
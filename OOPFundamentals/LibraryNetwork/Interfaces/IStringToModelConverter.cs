using System;

namespace LibraryNetwork.Interfaces
{
    public interface IStringToModelConverter
    {
        Type StringToModelConvert(string name);
    }
}

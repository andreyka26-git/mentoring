using System;

namespace ClassLibrary
{
    public static class UserInteraction
    {
        public static string GetHello(string name)
        {
            return $"{DateTime.Now.ToShortTimeString()}, Hello {name}!";
        }
    }
}

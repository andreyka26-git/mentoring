using LibraryNetwork.Interfaces;
using LibraryNetwork.Models;
using System;

namespace LibraryNetwork.Services
{
    public class ConsoleInteraction : IUserInteractable
    {
        public string GetId()
        {
            var input = Console.ReadLine();
            return input.Trim();
        }

        public void PrintLibraryEntity(LibraryEntity entity)
        {
            Console.WriteLine(entity != null ? entity.EntityToString() : "Id with such entity isn't exist.");
        }
    }
}

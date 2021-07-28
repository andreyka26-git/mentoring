using System;
using FileSystem.Models;
using FileSystem.Services;

namespace FileSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            const string Path = "C:\\Users\\Serhii_Yurko\\OneDrive - EPAM\\Desktop\\test";

            var fileIterator = new FileSystemVisitor(new PhysicalFileSystemProvider());
            var result = fileIterator.GetSystemTreeItems(Path);
            
            foreach (var systemItemModel in result)
            {
                //TODO use $"";
                Console.WriteLine($"{systemItemModel.Path}   {systemItemModel.Name}");
            }
        }
    }
}

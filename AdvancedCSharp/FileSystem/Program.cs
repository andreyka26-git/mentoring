using System;
using FileSystem.Models;
using FileSystem.Services;

namespace FileSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            const string path = "C:\\Users\\Serhii_Yurko\\OneDrive - EPAM\\Desktop\\test";

            var fileIterator = new FileSystemVisitor(new PhysicalFileSystemProvider());
            var result = fileIterator.GetSystemTreeItems(path);
            
            foreach (SystemItemModel systemItemModel in fileIterator)
            {
                Console.WriteLine($"{systemItemModel.Path}  {systemItemModel.Name}");
            }
        }
    }
}

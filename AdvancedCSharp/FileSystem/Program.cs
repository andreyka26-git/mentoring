using System;
using FileSystem.Services;

namespace FileSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileIterator = new FileSystemVisitor(new PhysicalFileSystemProvider());
            var result = fileIterator.GetSystemTreeItems("C:\\Users\\Serhii_Yurko\\OneDrive - EPAM\\Desktop\\test");
            foreach (var systemItemModel in result)
            {
                Console.WriteLine(systemItemModel.Path + "   " + systemItemModel.Name);
            }
        }
    }
}

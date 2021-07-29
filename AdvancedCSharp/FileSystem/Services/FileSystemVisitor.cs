using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FileSystem.Interfaces;
using FileSystem.Models;

namespace FileSystem.Services
{
    public class FileSystemVisitor : IFileSystemVisitor, IEnumerable
    {
        private readonly IFileSystemProvider _provider;
        private List<SystemItemModel> _systemItems = new();
        private readonly Predicate<string> _filterPredicate;
        private bool _isInterrupted;

        public FileSystemVisitor(IFileSystemProvider provider, Predicate<string> filter, bool isInterrupted)
        {
            _provider = provider;
            _filterPredicate = filter;
            InterruptProcessHandler += InterruptProcess;
            _isInterrupted = isInterrupted;

            //TODO subscribe to

            //TODO subscribe to 
            DeleteSystemItemsHandler += DeleteSystemItemsFromResult;
        }

        public FileSystemVisitor(IFileSystemProvider provider)
        {
            _provider = provider;
            _filterPredicate = value => true;
        }

        public event EventHandler<EventArgs> StartingEventHandler;
        public event EventHandler<EventArgs> StartedEventHandler;
        public event EventHandler<SystemFoundItemArgs> FileFoundEventHandler;
        public event EventHandler<SystemFoundItemArgs> FolderFoundEventHandler;

        //TODO drop it
        public event EventHandler<InterruptItemArgs> InterruptProcessHandler;

        //TODO drop it please we don't need this event according to the task
        public event EventHandler<DeleteItemArgs> DeleteSystemItemsHandler;


        public IEnumerable<SystemItemModel> GetSystemTreeItems(string path, bool isDeleteFiles = false, bool isDeleteFolders = false)
        {
            _systemItems.Clear();

            StartingEventHandler?.Invoke(this, null);
            IterateFileSystemTree(path);
            StartedEventHandler?.Invoke(this, null);
            
            //TODO drop
            DeleteSystemItemsHandler?.Invoke(this,
                new DeleteItemArgs { IsDeleteFiles = isDeleteFiles, IsDeleteFolders = isDeleteFolders });
            
            return _systemItems;
        }

        private void IterateFileSystemTree(string path)
        {
            if (_isInterrupted)
            {
                return;
            }

            var files = _provider.GetFiles(path);
            var directories = _provider.GetDirectories(path);

            foreach (var fileInfo in files)
            {
                if (!_filterPredicate(fileInfo.Name))
                    continue;

                var item = new SystemItemModel(fileInfo.FullName, fileInfo.Name, true);
                _systemItems.Add(item);

                FileFoundEventHandler?.Invoke(fileInfo, new SystemFoundItemArgs { Item = item });
            }

            foreach (var folderInfo in directories)
            {
                if (!_filterPredicate(folderInfo.Name))
                    continue;

                var item = new SystemItemModel(folderInfo.FullName, folderInfo.Name, false);
                _systemItems.Add(item);

                FolderFoundEventHandler?.Invoke(folderInfo, new SystemFoundItemArgs { Item = item });

                IterateFileSystemTree(folderInfo.FullName);
            }
        }

        private void InterruptProcess(object sender, InterruptItemArgs args)
        {
            if (args.IsInterrupt)
            {
                Console.WriteLine("Searching has been interrupted.");
            }
        }

        private void ItemFoundEventHandler(object sender, SystemFoundItemArgs args)
        {
            if (args.IsInterrupt)
                _isInterrupted = true;

            if (args.IsDelete)
                _systemItems.Remove(args.Item);
        }


        //TODO drop it instead of it implement methods for FolderFoundEventHandler and FileFoundEventHandltems.
        private void DeleteSystemItemsFromResult(object sender, DeleteItemArgs args)
        {
            if (args.IsDeleteFiles)
            {
                _systemItems = _systemItems.Where(f => f.IsFolder).ToList();
            }
            else if (args.IsDeleteFolders)
            {
                _systemItems = _systemItems.Where(f => f.IsFile).ToList();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new FileSystemEnumerator(_systemItems);
        }
    }
}
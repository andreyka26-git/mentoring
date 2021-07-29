using System;
using System.Collections;
using System.Collections.Generic;
using FileSystem.Interfaces;
using FileSystem.Models;

namespace FileSystem.Services
{
    public class FileSystemVisitor : IFileSystemVisitor, IEnumerable
    {
        private readonly IFileSystemProvider _provider;
        private readonly List<SystemItemModel> _systemItems = new();
        private readonly Predicate<string> _filterPredicate;
        private bool _isInterrupted;
        private bool _isDeleteFile;
        private bool _isDeleteFolder;

        public FileSystemVisitor(IFileSystemProvider provider, Predicate<string> filter, bool isInterrupted)
        {
            _provider = provider;
            _filterPredicate = filter;
            _isInterrupted = isInterrupted;

            FileFoundEventHandler += ItemFoundEventHandler;
            FolderFoundEventHandler += ItemFoundEventHandler;
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

        public IEnumerable<SystemItemModel> GetSystemTreeItems(string path, bool isDeleteFile = false, bool isDeleteFolder = false)
        {
            _systemItems.Clear();
            _isDeleteFile = isDeleteFile;
            _isDeleteFolder = isDeleteFolder;

            StartingEventHandler?.Invoke(this, null);
            IterateFileSystemTree(path);
            StartedEventHandler?.Invoke(this, null);

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

                FileFoundEventHandler?.Invoke(fileInfo, new SystemFoundItemArgs { Item = item, IsDeleteItem = _isDeleteFile });
            }

            foreach (var folderInfo in directories)
            {
                if (!_filterPredicate(folderInfo.Name))
                    continue;

                var item = new SystemItemModel(folderInfo.FullName, folderInfo.Name, false);
                _systemItems.Add(item);

                FolderFoundEventHandler?.Invoke(folderInfo, new SystemFoundItemArgs { Item = item, IsDeleteItem = _isDeleteFolder });

                IterateFileSystemTree(folderInfo.FullName);
            }
        }

        private void ItemFoundEventHandler(object sender, SystemFoundItemArgs args)
        {
            if (args.IsInterrupt)
                _isInterrupted = true;

            if (args.IsDeleteItem)
                _systemItems.Remove(args.Item);
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return new FileSystemEnumerator(_systemItems);
        }
    }
}
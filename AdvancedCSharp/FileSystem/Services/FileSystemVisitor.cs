using System;
using System.Collections.Generic;
using System.Linq;
using FileSystem.Interfaces;
using FileSystem.Models;

namespace FileSystem.Services
{
    public class FileSystemVisitor : IFileSystemVisitor
    {
        private readonly IFileSystemProvider _provider;
        private List<SystemItemModel> _systemItems = new();
        private readonly Predicate<string> _filterPredicate;
        private bool _isInterrupted;
        private bool _isDeleteFiles;
        private bool _isDeleteFolders;

        public event EventHandler<EventArgs> StartingEventHandler;
        public event EventHandler<EventArgs> StartedEventHandler;
        public event EventHandler<SystemItemArgs> FilesFoundEventHandler;
        public event EventHandler<SystemItemArgs> FoldersFoundEventHandler;
        public event EventHandler<SystemItemArgs> FilteredFilesFoundEventHandler;
        public event EventHandler<SystemItemArgs> FilteredFoldersFoundEventHandler;

        public FileSystemVisitor(IFileSystemProvider provider)
        {
            _provider = provider;
        }

        public FileSystemVisitor(IFileSystemProvider provider, Predicate<string> filter)
        {
            _provider = provider;
            _filterPredicate = filter;
        }

        public IEnumerable<SystemItemModel> GetSystemTreeItems(string path)
        {
            _systemItems.Clear();

            StartingEventHandler?.Invoke(this, null);
            IterateFileSystemTree(path);
            StartedEventHandler?.Invoke(this, null);

            FilesFoundEventHandler?.Invoke(this,
                new SystemItemArgs { Items = _systemItems.Where(i => i.IsFile) });
            FoldersFoundEventHandler?.Invoke(this,
                new SystemItemArgs { Items = _systemItems.Where(i => i.IsFolder) });
            FilterSystemItems();
            FilteredFilesFoundEventHandler?.Invoke(this,
                new SystemItemArgs { Items = _systemItems.Where(i => i.IsFile) });
            FilteredFoldersFoundEventHandler?.Invoke(this,
                new SystemItemArgs { Items = _systemItems.Where(i => i.IsFolder) });

            if (_isDeleteFiles)
                return _systemItems.Where(p => p.IsFolder).ToList();

            if (_isDeleteFolders)
                return _systemItems.Where(p => p.IsFile).ToList();

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
                _systemItems.Add(new SystemItemModel { Name = fileInfo.Name, Path = fileInfo.FullName });
            }

            foreach (var folderInfo in directories)
            {
                _systemItems.Add(new SystemItemModel { Name = folderInfo.Name, Path = folderInfo.FullName });
                IterateFileSystemTree(folderInfo.FullName);
            }
        }
        private void FilterSystemItems()
        {
            if (_filterPredicate != null)
            {
                _systemItems = _systemItems.Where(systemItem => _filterPredicate(systemItem.Name)).ToList();
            }
        }

        public void InterruptProcess(object sender, SystemItemArgs args)
        {
            _isInterrupted = args.IsInterruptedProcess;
        }

        public void DeleteFilesFromResult(object sender, SystemItemArgs args)
        {
            _isDeleteFiles = args.IsDeleteFiles;
        }

        public void DeleteFoldersFromResult(object sender, SystemItemArgs args)
        {
            _isDeleteFolders = args.isDeleteFolders;
        }
    }
}
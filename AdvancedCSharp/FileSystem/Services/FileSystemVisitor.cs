﻿using System;
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

        public FileSystemVisitor(IFileSystemProvider provider)
        {
            _provider = provider;
            _filterPredicate = value => true;
        }

        public event EventHandler<EventArgs> StartingEventHandler;
        public event EventHandler<EventArgs> StartedEventHandler;
        public event EventHandler<SystemFoundItemArgs> FileFoundEventHandler;
        public event EventHandler<SystemFoundItemArgs> FolderFoundEventHandler;
        public event EventHandler<InterruptItemArgs> InterruptProcessHandler;
        public event EventHandler<DeleteItemArgs> DeleteSystemItemsHandler;

        public FileSystemVisitor(IFileSystemProvider provider, Predicate<string> filter)
        {
            _provider = provider;
            _filterPredicate = filter;
            InterruptProcessHandler += InterruptProcess;
            DeleteSystemItemsHandler += DeleteSystemItemsFromResult;
        }

        public IEnumerable<SystemItemModel> GetSystemTreeItems(string path, bool isDeleteFiles = false, bool isDeleteFolders = false)
        {
            _systemItems.Clear();

            StartingEventHandler?.Invoke(this, null);
            IterateFileSystemTree(path);
            StartedEventHandler?.Invoke(this, null);
            
            DeleteSystemItemsHandler?.Invoke(this,
                new DeleteItemArgs { IsDeleteFiles = isDeleteFiles, IsDeleteFolders = isDeleteFolders });
            
            return _systemItems;
        }

        private void IterateFileSystemTree(string path)
        {
            if (_isInterrupted)
            {
                InterruptProcessHandler?.Invoke(this, new InterruptItemArgs { IsInterrupt = true });
                return;
            }

            var files = _provider.GetFiles(path);
            var directories = _provider.GetDirectories(path);

            foreach (var fileInfo in files)
            {
                if (!_filterPredicate(fileInfo.Name))
                    continue;

                var item = new SystemItemModel(fileInfo.FullName, fileInfo.Name, true);
                FileFoundEventHandler?.Invoke(fileInfo, new SystemFoundItemArgs { Item = item });
                _systemItems.Add(item);
            }

            foreach (var folderInfo in directories)
            {
                if (!_filterPredicate(folderInfo.Name))
                    continue;

                var item = new SystemItemModel(folderInfo.FullName, folderInfo.Name, false);
                FolderFoundEventHandler?.Invoke(folderInfo, new SystemFoundItemArgs { Item = item });
                _systemItems.Add(item);
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
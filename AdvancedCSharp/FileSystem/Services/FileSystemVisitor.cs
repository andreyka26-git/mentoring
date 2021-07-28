using System;
using System.Collections;
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
        
        public FileSystemVisitor(IFileSystemProvider provider)
        {
            _provider = provider;
            //TODO check if _filterPredicate equals to null || or set the default one always.
        }

        public event EventHandler<EventArgs> StartingEventHandler;
        public event EventHandler<EventArgs> StartedEventHandler;
        public event EventHandler<SystemItemArgs> FilesFoundEventHandler;
        public event EventHandler<SystemItemArgs> FoldersFoundEventHandler;
        public event EventHandler<SystemItemArgs> FilteredFilesFoundEventHandler;
        public event EventHandler<SystemItemArgs> FilteredFoldersFoundEventHandler;

        public FileSystemVisitor(IFileSystemProvider provider, Predicate<string> filter)
        {
            _provider = provider;
            _filterPredicate = filter;
            //TODO subscribe to all events

        }

        public IEnumerable<SystemItemModel> GetSystemTreeItems(string path)
        {
            _systemItems.Clear();

            StartingEventHandler?.Invoke(this, null);
            IterateFileSystemTree(path);
            StartedEventHandler?.Invoke(this, null);
            
            //TODO return enumerator
            return _systemItems;
        }

        private void IterateFileSystemTree(string path)
        {
            if (_isInterrupted)
            {
                //TODO emit
                InterruptedEventHandler(_isInterrupted);
                return;
            }

            var files = _provider.GetFiles(path);
            var directories = _provider.GetDirectories(path);

            foreach (var fileInfo in files)
            {
                //TODO add filtering here
                //TODO emit
                FileFoundEventHandler(fileInfo, _isDeleted);
                
                _systemItems.Add(new SystemItemModel { Name = fileInfo.Name, Path = fileInfo.FullName });
            }

            foreach (var folderInfo in directories)
            {
                //TODO emit
                //TODO add filtering here
                FolderFoundEventHandler(folderInfo, _isDeleted);
                _systemItems.Add(new SystemItemModel { Name = folderInfo.Name, Path = folderInfo.FullName });
                IterateFileSystemTree(folderInfo.FullName);
            }
        }

        private void InterruptProcess(object sender, SystemItemArgs args)
        {
            //TODO don't change state of the object, just log and return;
        }

        private void DeleteFilesFromResult(object sender, SystemItemArgs args)
        {
            //TODO drop file from SystemItemArgs from _systemitems;
        }

        private void DeleteFoldersFromResult(object sender, SystemItemArgs args)
        {
            //TODO drop folder from SystemItemArgs from _systemitems;
        }


        //TODO implement custom enumerator.
        public class CustomEnumerator : IEnumerator
        {
            private readonly List<SystemItemModel> _collection;

            private int index = 0;
            
            public CustomEnumerator(List<SystemItemModel> collection)
            {
                _collection = collection;
            }

            public bool MoveNext()
            {
                if (index + 1 > _collection.Count - 1)
                    return false;

                index++;
                return true;
            }

            public void Reset()
            {
                index = 0;
            }

            public object Current => _collection[index];
        }
    }
}
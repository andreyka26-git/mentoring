using System;
using System.Collections;
using System.Collections.Generic;
using FileSystem.Models;

namespace FileSystem
{
    public class FileSystemEnumerator : IEnumerator
    {
        private readonly List<SystemItemModel> _collection;
        private int _index = -1;

        public FileSystemEnumerator(List<SystemItemModel> collection)
        {
            _collection = collection;
        }

        public bool MoveNext()
        {
            _index++;
            return _index < _collection.Count;
        }

        public void Reset()
        {
            _index = -1;
        }

        public SystemItemModel Current
        {
            get
            {
                try
                {
                    return _collection[_index];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        object IEnumerator.Current => Current;
    }
}
using IvisGroup.Atiq.ReaderWriter.Interfaces;
using System;
using System.IO;

namespace IvisGroup.Atiq.ReaderWriter.Impl
{
    public class CSVReader : IReader
    {
        private StreamReader _readerStream = null;
        private char? _separator = null;
        private bool _disposed = false;

        public void Initialize(string fileName, char seperator)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException("fileName");

            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException(string.Format("File not found! - {0}", fileName));
            }

            _separator = seperator;
            _readerStream = File.OpenText(fileName);
        }

        public string[] ReadLine()
        {
            string[] columns = new string[0];

            if (_readerStream == null)
            {
                throw new Exception("Reader has not been initialized!");
            }

            if (!_separator.HasValue)
            {
                throw new Exception("No separator declared!");
            }

            var line = _readerStream.ReadLine();
            if (!string.IsNullOrEmpty(line))
            {
                columns = line.Split(_separator.Value);
            }

            return columns;
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                if (_readerStream != null)
                {
                    _readerStream.Close();
                    _separator = null;
                }
                
                _disposed = true;
            }
        }

    }
}

using IvisGroup.Atiq.ReaderWriter.Interfaces;
using System;
using System.IO;
using System.Text;

namespace IvisGroup.Atiq.ReaderWriter.Impl
{
    public class CSVWriter : IWriter
    {
        private StreamWriter _writerStream = null;
        private char? _separator = null;
        private bool _disposed = false;

        public void Initialize(string fileName, char seperator)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException("fileName");

            _separator = seperator;

            FileInfo fileInfo = new FileInfo(fileName);
            if (File.Exists(fileName))
            {
                _writerStream = fileInfo.AppendText();
            }
            else
            {
                _writerStream = fileInfo.CreateText();
            }
        }

        public void WriteLine(params string[] columns)
        {
            if (_writerStream == null)
            {
                throw new Exception("Writer has not been initialized!");
            }

            if (!_separator.HasValue)
            {
                throw new Exception("No separator declared!");
            }

            var line = new StringBuilder();

            for (int i = 0; i < columns.Length; i++)
            {
                line.Append(columns[i]);
                if ((columns.Length - 1) != i)
                {
                    line.Append(_separator.Value);
                }
            }

            _writerStream.WriteLine(line.ToString());
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                if (_writerStream != null)
                {
                    _writerStream.Close();
                    _separator = null;
                }
                _disposed = true;
            }
        }
    }
}

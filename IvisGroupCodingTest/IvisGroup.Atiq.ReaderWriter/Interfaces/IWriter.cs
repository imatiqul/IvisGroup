using IvisGroup.Atiq.ReaderWriter.Interfaces.Base;
using System;

namespace IvisGroup.Atiq.ReaderWriter.Interfaces
{
    public interface IWriter : IBase, IDisposable
    {
        void WriteLine(params string[] columns);
    }
}

using IvisGroup.Atiq.ReaderWriter.Interfaces.Base;
using System;

namespace IvisGroup.Atiq.ReaderWriter.Interfaces
{
    interface IReader : IBase, IDisposable
    {
        string[] ReadLine();
    }
}

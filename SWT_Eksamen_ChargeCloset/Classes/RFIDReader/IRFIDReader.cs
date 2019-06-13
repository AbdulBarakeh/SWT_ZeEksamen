using System;

namespace Classes.RFIDReader
{
    public interface IRFIDReader
    {
        event EventHandler<Guid> RFChipRead;
    }
}
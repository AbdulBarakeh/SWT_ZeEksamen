using System;

namespace Classes.RFIDReader
{
    public interface IRFIDReader
    {
        Guid RFChipRead(Guid ID);
    }
}
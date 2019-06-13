using System;

namespace Classes.RFIDReader
{
    public class RFReaderChangedEventArgs : EventArgs
    {
        public Guid _ID { get; set; }
        public RFReaderChangedEventArgs( Guid ID)
        {
            _ID = ID;
        }
    }
    public interface IRFIDReader
    {
        event EventHandler<RFReaderChangedEventArgs> RFChipRead;
        
    }
    
}
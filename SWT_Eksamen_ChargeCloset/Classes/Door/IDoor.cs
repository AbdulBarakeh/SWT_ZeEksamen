using System;
using Classes.Control;

namespace Classes.Door
{
    public class DoorStateChangedEventArgs : EventArgs
    {
        public Control.Control.LadeskabState DoorState { get; set; }

    }
    public interface IDoor
    {
        event EventHandler<DoorStateChangedEventArgs> DoorOpenedEvent;
        event EventHandler<DoorStateChangedEventArgs> DoorClosedEvent;

        void LockDoor();
        void UnlockDoor();
    }
}
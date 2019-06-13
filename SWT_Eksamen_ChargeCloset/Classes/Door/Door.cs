using System;
using System.Collections.Generic;
using System.Text;

namespace Classes.Door
{
    public class Door : IDoor
    {
        public event EventHandler<DoorStateChangedEventArgs> DoorOpenedEvent;
        public event EventHandler<DoorStateChangedEventArgs> DoorClosedEvent;

        public virtual void OnDoorOpenedEvent(DoorStateChangedEventArgs e)
        {
            DoorOpenedEvent?.Invoke(this,e);
        }

        public virtual void OnDoorClosedEvent(DoorStateChangedEventArgs e)
        {
            DoorClosedEvent?.Invoke(this,e);
        }

        public void LockDoor()
        {
            
            Console.WriteLine("Door is locked");
        }

        public void UnlockDoor()
        {

            Console.WriteLine("Door is locked");
        }
    }
}

using System;

namespace Classes.Door
{
    public interface IDoor
    {
        EventHandler DoorOpened();
        EventHandler DoorClosed();

        void LockDoor();
        void UnlockDoor();
    }
}
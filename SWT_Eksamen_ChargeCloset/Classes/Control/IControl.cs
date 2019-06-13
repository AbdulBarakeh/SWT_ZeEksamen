using System;

namespace Classes.Control
{
    public interface IControl
    {
        void RFIDDetected(Guid id);
        void Unlocked();
        void Locked();
        bool CheckId(Guid oldId, Guid newId);
        void LogDoorlocked(Guid id);
        void LogDoorunlocked(Guid id);


    }
}
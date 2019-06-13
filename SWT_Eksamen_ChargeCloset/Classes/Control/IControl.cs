namespace Classes.Control
{
    public interface IControl
    {
        void RFIDDetected(int id);
        void Unlocked();
        void Locked();
        bool CheckId(int oldId, int newId);
        void LogDoorlocked(int id);
        void LogDoorunlocked(int id);


    }
}
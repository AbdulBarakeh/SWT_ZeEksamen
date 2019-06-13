using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Classes.Charger;
using Classes.Control;
using Classes.Display;
using Classes.Door;
using Classes.Logger;
using Classes.RFIDReader;

namespace Classes.Control
{
    public class Control : IControl
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        public enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        //private string logFile = "logfile.txt"; // Navnet på systemets log-fil
        private Guid _oldId;

        private LadeskabState state { get; set; }


        private IDoor _door;
        private ICharger _charger;
        private ILogger _logger;
        private IRFIDReader _reader;
        private IDisplay _display;
        public Control(IDoor door, ICharger charger, ILogger logger, IRFIDReader reader, IDisplay display)
        {
            door.DoorOpenedEvent += HandleDoorOpenedEvent;
            door.DoorClosedEvent += HandleDoorClosedEvent;
            reader.RFChipRead += Reader_RFChipRead;

            _door = door;
            _charger = charger;
            _logger = logger;
            _reader = reader;
            _display = display;

            
        }

        private void Reader_RFChipRead(object sender, RFReaderChangedEventArgs e)
        {
            _oldId = e._ID;
            state = LadeskabState.Available;
            _display.displayMsg($"Id: {_oldId} received ");
        }

        private void HandleDoorClosedEvent(object sender, EventArgs e)
        {
            state = LadeskabState.Locked;
            _display.displayMsg("State Changed to closed");
        }

        private void HandleDoorOpenedEvent(object sender, EventArgs e)
        {
            state = LadeskabState.DoorOpen;
            _display.displayMsg("State Changed to open");
        }

        public void RFIDDetected(Guid id)
        {
            switch (state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_charger.IsConnected())
                    {
                        _door.LockDoor();
                        _charger.StartCharge();
                        _oldId = id;
                        LogDoorlocked(id);

                        _display.displayMsg("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
                        state = LadeskabState.Locked;
                    }
                    else
                    {
                        _display.displayMsg("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
                    }

                    break;

                case LadeskabState.DoorOpen:
                    _display.displayMsg("Nothing");

                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    
                    if (CheckId(_oldId, id))
                    {
                        _charger.StopCharge();
                        _door.UnlockDoor();
                        LogDoorunlocked(id);

                        _display.displayMsg("Tag din telefon ud af skabet og luk døren");
                        state = LadeskabState.Available;
                    }
                    else
                    {
                        _display.displayMsg("Forkert RFID tag");
                    }

                    break;
            }
        }
        

        public void Locked()
        {
            _door.LockDoor();
        }

        public void Unlocked()
        {
            _door.UnlockDoor();
        }

        public bool CheckId(Guid oldId, Guid newId)
        {
            if (_oldId == newId)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void LogDoorlocked(Guid id)
        {
            _logger.log($"{DateTime.Now}: cabinet locked with RFID: {id}");

        }

        public void LogDoorunlocked(Guid id)
        {
            _logger.log($"{DateTime.Now}: cabinet unlocked with RFID: {id}");
        }
    }
}

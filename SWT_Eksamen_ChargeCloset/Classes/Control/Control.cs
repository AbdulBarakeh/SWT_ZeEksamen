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
    class Control : IControl
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        private enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        private string logFile = "logfile.txt"; // Navnet på systemets log-fil
        private int oldId;

        private LadeskabState state { get; set; }


        private IDoor door;
        private ICharger charger;



        public void RFIDDetected(int id)
        {
            switch (state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (charger.IsConnected())
                    {
                        door.LockDoor();
                        charger.StartCharge();
                        oldId = id;
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", id);
                        }

                        Console.WriteLine("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
                        state = LadeskabState.Locked;
                    }
                    else
                    {
                        Console.WriteLine("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
                    }

                    break;

                case LadeskabState.DoorOpen:
                    // Ignore
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    if (id == oldId)
                    {
                        charger.StopCharge();
                        door.UnlockDoor();
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", id);
                        }

                        Console.WriteLine("Tag din telefon ud af skabet og luk døren");
                        state = LadeskabState.Available;
                    }
                    else
                    {
                        Console.WriteLine("Forkert RFID tag");
                    }

                    break;
            }
        }
        

        public void Locked()
        {
            throw new NotImplementedException();
        }

        public void Unlocked()
        {
            throw new NotImplementedException();
        }
    }
}

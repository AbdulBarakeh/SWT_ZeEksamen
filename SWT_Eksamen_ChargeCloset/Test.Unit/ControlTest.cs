using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using Classes.Charger;
using Classes.Control;
using Classes.Door;
using Classes.Display;
using Classes.Logger;
using Classes.RFIDReader;

namespace Test.Unit
{
    [TestFixture]
    public class ControlTest
    {
        private IDoor _door;
        private ICharger _charger;
        private ILogger _logger;
        private IRFIDReader _reader;
        private IDisplay _display;
        private IControl _uut;

        [SetUp]
        public void SetUp()
        {
            _door = Substitute.For<IDoor>();
            _charger = Substitute.For<ICharger>();
            _logger = Substitute.For<ILogger>();
            _reader = Substitute.For<IRFIDReader>();
            _display = Substitute.For<IDisplay>();


            _uut = new Control(_door,_charger,_logger,_reader,_display);
        }

        [Test]
        public void StateAvailable_ChargingFalse()
        {
            var id = Guid.NewGuid();
            _charger.IsConnected().Returns(false);
            _reader.RFChipRead += Raise.EventWith<RFReaderChangedEventArgs>(new RFReaderChangedEventArgs(id));
            _uut.RFIDDetected(id);

            _display.Received().displayMsg("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");

        }

        [Test]
        public void StateAvailable_ChargingTrue()
        {
            var id = Guid.NewGuid();
            _charger.IsConnected().Returns(true);
            _reader.RFChipRead += Raise.EventWith<RFReaderChangedEventArgs>(new RFReaderChangedEventArgs(id));
            _uut.RFIDDetected(id);

            _display.Received().displayMsg("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");

        }

        [Test]
        public void StateDoorOpen()
        {
            _door.DoorOpenedEvent += Raise.Event();
            _uut.RFIDDetected(Guid.NewGuid());
            _display.Received().displayMsg("Nothing");
        }

        [Test]
        public void StateLocked_CheckIdTrue()
        {
            var id = Guid.NewGuid();
            _charger.IsConnected().Returns(true);
            _reader.RFChipRead += Raise.EventWith<RFReaderChangedEventArgs>(new RFReaderChangedEventArgs(id));
            _uut.RFIDDetected(id);

            _uut.RFIDDetected(id);
            _display.Received().displayMsg("Tag din telefon ud af skabet og luk døren");

        }

        [Test]
        public void StateLocked_CheckIdFalse()
        {
            //possibility 1
            //var id = Guid.NewGuid();
            //_charger.IsConnected().Returns(true);
            //_reader.RFChipRead += Raise.EventWith<RFReaderChangedEventArgs>(new RFReaderChangedEventArgs(id));
            //_uut.RFIDDetected(id);

            //Posibility 2
            _door.DoorClosedEvent += Raise.Event();

            _uut.RFIDDetected(Guid.NewGuid());
            _display.Received().displayMsg("Forkert RFID tag");
            
        }
    }
}
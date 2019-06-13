using System;
using System.Collections.Generic;
using System.Text;

namespace Classes.Charger
{
    class Charger : ICharger
    {
        private bool _charging;
        public bool IsConnected()
        {
            return _charging;
        }

        public void StartCharge()
        {
            _charging = true;
        }

        public void StopCharge()
        {
            _charging = false;
        }
    }
}

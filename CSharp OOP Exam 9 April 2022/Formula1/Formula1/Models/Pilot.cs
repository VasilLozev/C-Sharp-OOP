using Formula1.Models.Contracts;
using Formula1.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Formula1.Models
{
    public class Pilot : IPilot
    {
        public string FullName
        {
            get
            {
               
                return _fullName;
            }
            private set
            {
                if (string.IsNullOrEmpty(value) || value.Length < 5)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidPilot, value));
                }
                _fullName = value;
            }
        }
        private string _fullName;

        public IFormulaOneCar Car
        {
            get
            {
                
                return _car;
            }
            private set {
                if (value == null)
                {
                    throw new NullReferenceException(String.Format(ExceptionMessages.InvalidCarForPilot));
                }
                _car = value; }
        }

        private IFormulaOneCar _car;

        public int NumberOfWins
        {
            get { return numberOfWins; }
            private set { numberOfWins = value; }
        }

        private int numberOfWins;

        private bool canRace;

        public bool CanRace
        {
            get { return canRace; }
            private set { canRace = value; }
        }

         
        public void AddCar(IFormulaOneCar car)
        {
            Car = car;
            CanRace = true;
        }

        public void WinRace()
        {
            NumberOfWins++;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Pilot {_fullName} has {numberOfWins} wins.");
            return sb.ToString().Trim();
        }
        public Pilot(string fullName)
        {
            FullName = fullName;
        }
    }
}

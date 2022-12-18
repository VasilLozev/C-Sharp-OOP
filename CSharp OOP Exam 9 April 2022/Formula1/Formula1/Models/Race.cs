using Formula1.Models.Contracts;
using Formula1.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Formula1.Models
{
    public class Race : IRace
    {
        public string RaceName
        {
            get
            {
                return _raceName;
            }
            private set
            {
                if (string.IsNullOrEmpty(value) || value.Length < 5)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidRaceName, value));
                }
                _raceName = value;
            }
        }
        private string _raceName;
        public int NumberOfLaps
        {
            get 
            {
                
                return _numberOfLaps;
            }
            private set {
                if (value < 1)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidLapNumbers, value));
                }
                _numberOfLaps = value; }
        }
        private int _numberOfLaps;

        private bool tookPlace;

        public bool TookPlace
        {
            get { return tookPlace; }
            set { tookPlace = value; }
        }

        private ICollection<IPilot> pilots;

        public ICollection<IPilot> Pilots
        {
            get { return pilots; }
        }


        public void AddPilot(IPilot pilot)
        {
            Pilots.Add(pilot);
        }

        public string RaceInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"The {RaceName} race has:");
            sb.AppendLine($"Participants: {Pilots.Count}");
            sb.AppendLine($"Number of laps: {NumberOfLaps}");
            if (TookPlace == true)
            {
                sb.AppendLine($"Took place: Yes");
            }
            else
            {
                sb.AppendLine($"Took place: No");
            }
            return sb.ToString().Trim();
        }
        public Race(string raceName, int numberOfLaps)
        {
            pilots = new List<IPilot>();
            RaceName = raceName;
            NumberOfLaps = numberOfLaps;
        }
    }
}

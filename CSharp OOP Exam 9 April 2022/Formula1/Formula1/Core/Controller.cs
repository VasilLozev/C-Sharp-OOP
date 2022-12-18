using Formula1.Core.Contracts;
using Formula1.Models;
using Formula1.Models.Contracts;
using Formula1.Repositories;
using Formula1.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;

namespace Formula1.Core
{
    public class Controller : IController
    {
        public Controller()
        {
            pilotRepository = new PilotRepository();
            raceRepository = new RaceRepository();
            carRepository = new FormulaOneCarRepository();
        }
        private PilotRepository pilotRepository;
        private RaceRepository raceRepository;
        private FormulaOneCarRepository carRepository;


        public string AddCarToPilot(string pilotName, string carModel)
        {
            if (pilotRepository.FindByName(pilotName) == null)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.PilotDoesNotExistOrHasCarErrorMessage, pilotName));
            }
            if (pilotRepository.FindByName(pilotName).CanRace == true)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.PilotDoesNotExistOrHasCarErrorMessage, pilotName));
            }
            if (carRepository.FindByName(carModel) == null)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.CarDoesNotExistErrorMessage, carModel));
            }
            pilotRepository.FindByName(pilotName).AddCar(carRepository.FindByName(carModel));
            carRepository.Remove(carRepository.FindByName(carModel));
            return string.Format(OutputMessages.SuccessfullyPilotToCar, pilotName, pilotRepository.FindByName(pilotName).Car.GetType().Name, carModel);
        }

        public string AddPilotToRace(string raceName, string pilotFullName)
        {
            if (!raceRepository.Models.Any(x=>x.RaceName == raceName))
            {
                throw new NullReferenceException(String.Format(ExceptionMessages.RaceDoesNotExistErrorMessage, raceName));
            }
            if (pilotRepository.FindByName(pilotFullName) == null)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.PilotDoesNotExistErrorMessage, pilotFullName));
            }
            if (raceRepository.FindByName(raceName).Pilots.Any(x=>x.FullName == pilotFullName))
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.PilotDoesNotExistErrorMessage, pilotFullName));
            }
            if (!pilotRepository.FindByName(pilotFullName).CanRace)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.PilotDoesNotExistErrorMessage, pilotFullName));
            }
            raceRepository.FindByName(raceName).AddPilot(pilotRepository.FindByName(pilotFullName));
            return String.Format(OutputMessages.SuccessfullyAddPilotToRace, pilotFullName, raceName);
        }

        public string CreateCar(string type, string model, int horsepower, double engineDisplacement)
        {
            if (carRepository.Models.Any(x => x.Model == model))
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.CarExistErrorMessage, model));
            }
            if (type != "Ferrari" && type != "Williams")
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.InvalidTypeCar, type));
            }
            if (type == "Ferrari")
            {
                carRepository.Add(new Ferrari (model, horsepower, engineDisplacement));
            }
            if (type == "Williams")
            {
                carRepository.Add(new Williams(model, horsepower, engineDisplacement));
            }
            return string.Format(OutputMessages.SuccessfullyCreateCar, type, model);
        }

        public string CreatePilot(string fullName)
        {
            
            if (pilotRepository.Models.Any(x=>x.FullName == fullName))
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.PilotExistErrorMessage, fullName));
            }
            pilotRepository.Add(new Pilot(fullName));
            return String.Format(OutputMessages.SuccessfullyCreatePilot, fullName);
        }

        public string CreateRace(string raceName, int numberOfLaps)
        {
            if (raceRepository.Models.Any(x=>x.RaceName == raceName))
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.RaceExistErrorMessage, raceName));
            }
            raceRepository.Add(new Race(raceName, numberOfLaps));
            return String.Format(OutputMessages.SuccessfullyCreateRace, raceName);

        }

        public string PilotReport()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in pilotRepository.Models.OrderByDescending(x => x.NumberOfWins))
            {
                sb.AppendLine(item.ToString());
            }
            return sb.ToString().TrimEnd();
        }

        public string RaceReport()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in raceRepository.Models)
            {
                if (item.TookPlace == true)
                {
                    sb.AppendLine(item.RaceInfo());
                }
            }
            return sb.ToString().TrimEnd();
        }

        public string StartRace(string raceName)
        {
            if (raceRepository.FindByName(raceName) == null)
            {
                throw new NullReferenceException(String.Format(ExceptionMessages.RaceDoesNotExistErrorMessage,raceName));
            }
            if (raceRepository.FindByName(raceName).Pilots.Count < 3)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.InvalidRaceParticipants, raceName));
            }
            if (raceRepository.FindByName(raceName).TookPlace == true)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.RaceTookPlaceErrorMessage, raceName));
            }

            raceRepository.FindByName(raceName).Pilots.OrderByDescending(x=>x.Car.RaceScoreCalculator(raceRepository.FindByName(raceName).NumberOfLaps));

            raceRepository.FindByName(raceName).TookPlace = true;
            raceRepository.FindByName(raceName).Pilots.OrderByDescending(x => x.Car.RaceScoreCalculator(raceRepository.FindByName(raceName).NumberOfLaps)).First().WinRace();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Pilot {raceRepository.FindByName(raceName).Pilots.OrderByDescending(x => x.Car.RaceScoreCalculator(raceRepository.FindByName(raceName).NumberOfLaps)).First().FullName} wins the { raceName } race.");
            sb.AppendLine($"Pilot {raceRepository.FindByName(raceName).Pilots.OrderByDescending(x => x.Car.RaceScoreCalculator(raceRepository.FindByName(raceName).NumberOfLaps)).Take(2).Last().FullName} is second in the {raceName} race.");
            sb.AppendLine($"Pilot {raceRepository.FindByName(raceName).Pilots.OrderByDescending(x => x.Car.RaceScoreCalculator(raceRepository.FindByName(raceName).NumberOfLaps)).Take(3).Last().FullName} is third in the {raceName} race.");
            return sb.ToString().TrimEnd();
        }
    }
}

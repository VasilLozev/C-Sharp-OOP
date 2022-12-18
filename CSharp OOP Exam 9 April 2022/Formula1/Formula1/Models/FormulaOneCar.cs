using Formula1.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using Formula1.Utilities;
using Formula1.Core;

namespace Formula1.Models
{
    public abstract class FormulaOneCar : IFormulaOneCar
    {
        public string Model
        {
            get
            {
                
                return _model;
            }
            private set
            {
                if (string.IsNullOrEmpty(value) || value.Length < 3)
                {
                    throw new ArgumentException(String.Format(ExceptionMessages.InvalidF1CarModel, value));
                }
                _model = value;
            }
        }

        private string _model;
        public int Horsepower
        {
            get
            {
                
                return _horsePower;
            }
            private set
            {
                if (value < 900 || value > 1050)
                {
                    throw new ArgumentException(String.Format(ExceptionMessages.InvalidF1HorsePower, value));
                }
                _horsePower = value;
            }
        }
        private int _horsePower;
        public double EngineDisplacement
        {
            get
            {
                
                return _engineDisplacement;
            }
            private set
            {
                if (value < 1.60 || value > 2.00)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidF1EngineDisplacement, value));
                }
                _engineDisplacement = value;
            }
        }
        private double _engineDisplacement;
        public double RaceScoreCalculator(int laps)
        {
            return _engineDisplacement / _horsePower * laps;
        }
        public FormulaOneCar(string model, int horsePower, double engineDisplacement)
        {
            Model = model;
            Horsepower = horsePower;
            EngineDisplacement = engineDisplacement;
        }
    }
}

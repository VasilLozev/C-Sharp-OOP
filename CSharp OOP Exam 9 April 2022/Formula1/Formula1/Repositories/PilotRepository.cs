using Formula1.Models.Contracts;
using Formula1.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Formula1.Repositories
{
    public class PilotRepository : IRepository<IPilot>
    {
        public IReadOnlyCollection<IPilot> Models => _models.AsReadOnly();
        
        private List<IPilot> _models;

        public void Add(IPilot pilot)
        {
            _models.Add(pilot);
        }

        public IPilot FindByName(string fullName)
        {
            return _models.FirstOrDefault(x => x.FullName == fullName);
        }

        public bool Remove(IPilot model)
        {
            return _models.Remove(model);
        }
        public PilotRepository()
        {
            _models = new List<IPilot>();
        }
    }
}

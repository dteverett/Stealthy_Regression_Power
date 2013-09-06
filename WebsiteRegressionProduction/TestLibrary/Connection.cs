using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLibrary
{
    public class Connection : IDisposable
    {
        public ClaimstakerDataDataContext _repository;

        public Connection()
        {
            _repository = new ClaimstakerDataDataContext();
        }

        public void Dispose()
        {
            if (_repository != null)
            {
                _repository.Dispose();
            }
        }
    }
}

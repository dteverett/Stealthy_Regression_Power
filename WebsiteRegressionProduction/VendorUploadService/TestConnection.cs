using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendorUploadService
{
    class TestConnection : IDisposable
    {
        public TestDatabaseDataContext _testRepos;

        public TestConnection()
        {
            _testRepos = new TestDatabaseDataContext();
        }

        public void Dispose()
        {
            if (_testRepos != null)
            {
                _testRepos.Dispose();
            }
        }
    }
}

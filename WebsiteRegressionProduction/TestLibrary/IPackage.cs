using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLibrary
{
    public interface IPackage
    {
        Document Document { get; set; }
        Client Client { get; set; }

        string ToString();
    }
}

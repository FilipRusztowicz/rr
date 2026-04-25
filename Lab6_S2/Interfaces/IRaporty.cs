using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IRaporty
    {
        IEnumerable<Zawodnik> PobierzZawodnikowKlubu(int klubId);
        int GoleKlubu(int klubId);
        double SredniaKondycjaZawodnikow(int klubId);
        
    }
}

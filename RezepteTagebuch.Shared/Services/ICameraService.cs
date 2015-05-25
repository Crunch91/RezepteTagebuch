using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezepteTagebuch.Shared.Services
{
    public interface ICameraService
    {
        string GetPicturePath(string fileName);
        void SavePicture(string fileName);
    }
}

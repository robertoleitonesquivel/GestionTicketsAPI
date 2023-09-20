using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuniBarva.COMMON.Interfaces
{
    public interface IConfigKey
    {
       Task<string> GetKeyValue(string _key);
    }
}

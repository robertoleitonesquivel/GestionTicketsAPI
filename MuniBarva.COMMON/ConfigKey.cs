using Microsoft.Extensions.Configuration;
using MuniBarva.COMMON.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuniBarva.COMMON
{
    public class ConfigKey : IConfigKey
    {
        private readonly IConfiguration _configuration;

        public ConfigKey(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> GetKeyValue(string _key)
        {
            return await Task.Run(() => {

                var generalConfiguration = _configuration.GetSection("GeneralConfiguration");

                return generalConfiguration[_key];
               
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog_API.Settings
{
    public class JwtSettings
    {
        public string Key { get; set; }
        public string RefreshTokenKey { get; set; }
        public string Issuer { get; set; }
    }
}
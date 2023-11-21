using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hims_Security_API.Model
{
    public class EncryptedDataVo
    {

        public string Key
        {
            get; set;
        }
        public string IV
        {
            get; set;
        }
        public string EncryptedPassword
        {
            get; set;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniProductCRUD.Models
{
    public enum ResultCodes
    {
        Success=0,
        ServerError=1,
        DataError=2,
        TokenMissing=9,
        TokenError=99,
    }
}

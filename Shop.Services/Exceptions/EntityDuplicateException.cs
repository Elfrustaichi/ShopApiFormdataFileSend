using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNT.Services.Exceptions
{
    public class EntityDuplicateException:Exception
    {
        public EntityDuplicateException(string message):base(message)
        {
            
        }
    }
}

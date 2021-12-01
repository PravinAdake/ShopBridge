using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridgeRepository
{
    public interface IMasters
    {
        string GetInventoryDetails(string reqtype);
    }
}

using ShopBridgeModel;
using System.Collections.Generic;

namespace ShopBridgeRepository
{
    public interface ITransations
    {
        string PostInventoryDetails(Insert_Inventory_Request request);
    }
}

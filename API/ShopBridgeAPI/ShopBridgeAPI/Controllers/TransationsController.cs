using ShopBridgeCommon;
using ShopBridgeModel;
using ShopBridgeRepository;
using System;
using System.Web.Http;
using System.Web.Http.Description;

namespace ShopBridgeAPI.Controllers
{
    [RoutePrefix("Transactions")]
    public class TransationsController : ApiController
    {
        private ITransations _transationsrepo = null;
        public TransationsController(ITransations transationsrepo)
        {
            _transationsrepo = transationsrepo;
        }

        [Route("PostInventoryDetails")]
        [HttpPost]
        [ResponseType(typeof(string))]
        public IHttpActionResult PostInventoryDetails(Insert_Inventory_Request request)
        {
            try
            {
                var response = _transationsrepo.PostInventoryDetails(request);
                if (response != null)
                {
                    return Ok(response);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.SendErrorToText(ex);
                return InternalServerError();
            }
        }

    }
}

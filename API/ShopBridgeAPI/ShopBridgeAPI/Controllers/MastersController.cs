using ShopBridgeCommon;
using ShopBridgeRepository;
using System;
using System.Web.Http;
using System.Web.Http.Description;

namespace ShopBridgeAPI.Controllers
{
    [RoutePrefix("Masters")]
    public class MastersController : ApiController
    {
        private IMasters _mastersrepo = null;
        public MastersController(IMasters mastersrepo)
        {
            _mastersrepo = mastersrepo;
        }

        [Route("GetInventoryDetails")]
        [HttpGet]
        [ResponseType(typeof(string))]
        public IHttpActionResult GetInventoryDetails(string reqtype)
        {
            try
            {
                var response = _mastersrepo.GetInventoryDetails(reqtype);
                if (response != null)
                {
                    return Ok(response);
                }
                else
                {
                    return NotFound();
                }
            }
            catch(Exception ex)
            {
                ErrorLog.SendErrorToText(ex);
                return InternalServerError();
            }
        }
    }
}

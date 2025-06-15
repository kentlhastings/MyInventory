using Microsoft.AspNetCore.Mvc;
using MyInventory.Logic;

namespace MyInventory.Service
{
    [ApiController]
    [Route("api/[controller]")]
    public class TradeController : ControllerBase
    {
        private readonly TradeLogic _tradeLogic;

        public TradeController(TradeLogic tradeLogic)
        {
            _tradeLogic = tradeLogic;
        }
    }
}

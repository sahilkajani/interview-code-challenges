using Microsoft.AspNetCore.Mvc;

namespace OneBeyondApi.Controllers
{
    public class OnLoanController : ControllerBase
    {
        [Route("GetCurrentBooksOnLoan")]
        public ActionResult GetCurrentBooksOnLoan()
        {

            return Ok();
        }
    }
}

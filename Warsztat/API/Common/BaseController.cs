using Microsoft.AspNetCore.Mvc;

namespace Warsztat.API
{
    /*
     *
     * This controller with endpoint was created for Kubernetes.
     * When application respond with HTTP 200 OK - Kubernetes knows that application started and is in 'healthy' condition.
     * 
     */
    [ApiController]
    public class BaseController : Controller
    {       
        [HttpGet]
        [Route("health-check")]
        public IActionResult HealthChecks()
        {
            return Ok(200);
        }   
    }
}
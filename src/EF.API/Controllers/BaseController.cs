using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EF.API.Controllers
{
    [Authorize]
    public class BaseController : ControllerBase
    {
    }
}

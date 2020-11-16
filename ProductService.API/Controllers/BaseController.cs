using Microsoft.AspNetCore.Mvc;
using ProductService.API.Infrastructure;

namespace ProductService.API.Controllers
{
    [ServiceFilter(typeof(ActionLogFilter))]
    [ServiceFilter(typeof(ErrorLogFilter))]
    public class BaseController : Controller
    {
    }
}

using ETicaretUygulamasi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ETicaretUygulamasi.Controllers
{
    public class BaseController : Controller
    {
        public int GetUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}

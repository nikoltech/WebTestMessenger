namespace WebTestMessenger.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    public abstract class BaseController : ControllerBase
    {
        protected int UserId
        {
            get
            {
                return int.Parse(this.User.Claims.First(i => i.Type == "UserId").Value);
            }
        }

        //protected int UserId()
        //{
        //    return int.Parse(this.User.Claims.First(i => i.Type == "UserId").Value);
        //}
    }
}

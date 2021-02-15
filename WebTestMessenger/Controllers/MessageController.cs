namespace WebTestMessenger.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Authorize]
    [Route("api/[controller]")]
    public class MessageController : BaseController
    {
        [AllowAnonymous]
        [HttpGet("Index")]
        public IActionResult Index()
        {
            return this.Ok("Works!!!");
        }

        // get messages for you
        [HttpGet("GetMessages")]
        public async Task GetMessagesAsync()
        {
            
        }

        // get list of users with count of messages
        [HttpGet("GetUsersList")]
        public async Task GetUsersListAsync()
        {

        }

        // send message for user
        [HttpPost("SendMessage")]
        public async Task SendMessageAsync()
        {

        }

        // delete message if you send it
        [HttpDelete("DeleteMessage")]
        public async Task DeleteMessageAsync()
        {

        }
    }
}

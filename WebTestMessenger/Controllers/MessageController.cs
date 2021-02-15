namespace WebTestMessenger.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WebTestMessenger.BusinessLogic.Interfaces;
    using WebTestMessenger.BusinessLogic.Models;

    [Authorize]
    [Route("api/[controller]")]
    public class MessageController : BaseController
    {
        private readonly IMessageManagement messageManagement;

        public MessageController(IMessageManagement messageManagement)
        {
            this.messageManagement = messageManagement;
        }

        [AllowAnonymous]
        [HttpGet("Index")]
        public IActionResult Index()
        {
            return this.Ok(this.UserId);
        }

        // get messages for you
        [HttpGet("List")]
        public async Task<IActionResult> GetMessagesAsync()
        {
            try
            {
                var result = await this.messageManagement.GetMessagesAsync(this.UserId).ConfigureAwait(false);

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // get own messages
        [HttpGet("MyList")]
        public async Task<IActionResult> GetMyMessagesAsync()
        {
            try
            {
                var result = await this.messageManagement.GetOwnMessagesAsync(this.UserId).ConfigureAwait(false);

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // get list of users with count of messages
        [HttpGet("GetUsersList")]
        public async Task<IActionResult> GetUsersListAsync()
        {
            try
            {
                IList<(string, int)> result = await this.messageManagement.GetUsersListAsync().ConfigureAwait(false);

                List<object> resultList = new List<object>();
                foreach (var item in result)
                {
                    resultList.Add(new { user = item.Item1, Messages = item.Item2 });
                }

                return this.Ok(resultList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // send message for user
        [HttpPost("Send/{userIdTo}")]
        public async Task<IActionResult> SendMessageAsync([FromBody] MessageModel messageModel, int userIdTo)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            try
            {
                var result = await this.messageManagement.SendMessageAsync(messageModel, this.UserId, userIdTo).ConfigureAwait(false);

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // delete message if you send it
        [HttpDelete("Remove/{id}")]
        public async Task<IActionResult> RemoveMessageAsync(int id)
        {
            try
            {
                var result = await this.messageManagement.DeleteMessageAsync(id, this.UserId).ConfigureAwait(false);

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

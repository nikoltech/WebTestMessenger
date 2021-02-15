namespace WebTestMessenger.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
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
            return this.Ok("Works!!!");
        }

        // get messages for you
        [HttpGet("GetMessages")]
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
        [HttpGet("GetOwnMessages")]
        public async Task<IActionResult> GetOwnMessagesAsync()
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
                var result = await this.messageManagement.GetUsersListAsync().ConfigureAwait(false);

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // send message for user
        [HttpPost("SendMessage")]
        public async Task<IActionResult> SendMessageAsync(MessageModel messageModel, int recipientUserId)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            try
            {
                var result = await this.messageManagement.SendMessageAsync(messageModel, this.UserId, recipientUserId).ConfigureAwait(false);

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // delete message if you send it
        [HttpDelete("DeleteMessage")]
        public async Task<IActionResult> DeleteMessageAsync(int messageId)
        {
            try
            {
                var result = await this.messageManagement.DeleteMessageAsync(messageId, this.UserId).ConfigureAwait(false);

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

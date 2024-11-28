using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using YChatApi.DTOs;
using YChatApi.Services;

namespace YChatApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MessagesController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMessagesService _messagesService;

        public MessagesController(IUserService userService, IMessagesService messagesService)
        {
            _userService = userService;
            _messagesService = messagesService;
        }

        [HttpGet("{chatId}")]
        public async Task<ActionResult> GetMessages(int chatId)
        {
            try
            {
                var messages = await _messagesService.GetMessages(chatId);

                return Ok(messages);
            } 
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("{chatId}")]
        public async Task<ActionResult> SendMessages(int chatId, MessageDTO dto)
        {
            try
            {
                var currentUser = await _userService.GetCurrentUser(this.HttpContext.Request.Headers.Authorization);
                var message = await _messagesService.SendMessage(chatId, dto, currentUser);

                return Ok(message);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


    }
}

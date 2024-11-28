using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using YChatApi.DTOs;
using YChatApi.Services;

namespace YChatApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;
        private readonly IUserService _userService;

        public ChatController(IChatService chatService, IUserService userService)
        {
            _chatService = chatService;
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult> AddChat(ChatDto dto)
        {
            try
            {
                var currentUser = await _userService.GetCurrentUser(this.HttpContext.Request.Headers.Authorization);

                dto.Usernames = dto.Usernames.Append(currentUser.Username).ToList();

                var chat = await _chatService.AddChat(dto);

                return Ok(chat);
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

        [HttpGet]
        public async Task<ActionResult> GetChats()
        {
            try
            {
                var currentUser = await _userService.GetCurrentUser(this.HttpContext.Request.Headers.Authorization);

                var chats = await _chatService.GetAllUserChats(currentUser.Id);

                return Ok(chats.ToList());
            } catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            } catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Text.Json;

namespace EventService.EventService
{
    [Route("api/service")]
    [ApiController]
    public class EventController : Controller
    {
        [HttpGet]
        [Route("1")]
        public async Task Test()
        {
            Ok("Good");
        }
        private static Queue<Event> _queue = new Queue<Event>();
        [HttpPost]
        [Route("addEvent")]
        public async Task<IActionResult> PostEvent()
        {
            var response = ControllerContext.HttpContext.Response;
            var request = ControllerContext.HttpContext.Request;
            try
            {
                var jsonoptions = new JsonSerializerOptions();
                jsonoptions.Converters.Add(new EventConverter());
                var Event = await request.ReadFromJsonAsync<Event>(jsonoptions);
                if(Event != null)
                {
                    _queue.Enqueue(Event);
                    return Ok("Event added");
                }
            }
            catch(JsonException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex}");
            }
            return BadRequest("Something goes wrong");
        }
        [HttpGet]
        [Route("getValues")]
        public async Task<IActionResult> GetValues() 
        {
            Result currentResult = new Result(_queue);
            return Json(currentResult);
        }
    }
}

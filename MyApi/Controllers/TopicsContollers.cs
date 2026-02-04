using System.Net;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using MyApi.Models;
using MyApi.Models.DTO;
using MyApi.Services;

namespace MyApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TopicsController : ControllerBase
    {
        private readonly ITopicService _topicService;
        public TopicsController(ITopicService topicService)
        {
            _topicService = topicService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTopics(
            [FromQuery] int? pageNumber = 1,
            [FromQuery] int? pageSize = 10
        )
        {
            var (result, Count) = await _topicService.GetAllTopicsAsync(pageNumber, pageSize);
            return Ok(new CommonResponse
            {
                Success = true,
                Message = "Topics retrieved successfully",
                StatusCode = HttpStatusCode.OK,
                Data = new
                {
                    Topics = result,
                    TotalCount = Count
                }
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTopicById(Guid id)
        {
            var result = await _topicService.GetTopicByIdAsync(id);

            return Ok(new CommonResponse
            {
                Success = true,
                Message = "Topic retrieved successfully",
                StatusCode = HttpStatusCode.OK,
                Data = result
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateTopic([FromBody] CreateTopicDTO model)
        {
            var id = await _topicService.CreateTopicAsync(model);
            return CreatedAtAction(nameof(GetTopicById), new { id }, new CommonResponse
            {
                Success = true,
                Message = "Topic created successfully",
                StatusCode = HttpStatusCode.Created,
                Data = id
            });
        }


        [HttpPut]
        public async Task<IActionResult> UpdateTopic([FromBody] UpdateTopicDTO model)
        {
            var result = await _topicService.UpdateTopicAsync(model);
            return Ok(new CommonResponse
            {
                Success = true,
                Message = "Topic updated successfully",
                StatusCode = HttpStatusCode.OK,
                Data = result
            });
        }
    }
}
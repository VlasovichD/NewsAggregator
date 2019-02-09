using AutoMapper;
using CodeHollow.FeedReader;
using FeedAggregator.BLL.Dtos;
using FeedAggregator.BLL.Infrastructure;
using FeedAggregator.BLL.Services;
using FeedAggregator.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace FeedAggregator.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/collections/{collectionId}/[controller]")]
    public class FeedsController : ControllerBase
    {
        private readonly ICollectionManagerService _collectionService;
        private readonly IFeedManagerService _feedService;
        private readonly IFeedReadingService _readingService;
        private readonly IMapper _mapper;

        public FeedsController(
            ICollectionManagerService collectionService,
            IFeedManagerService feedService,
            IFeedReadingService readingService,
            IMapper mapper)
        {
            _collectionService = collectionService;
            _feedService = feedService;
            _readingService = readingService;
            _mapper = mapper;
        }

        // POST api/collections/{collectionId}/feeds
        [HttpPost]
        public IActionResult Create(int collectionId, [FromBody, Bind("Name, Description, Url")] FeedModel feed)
        {
            try
            {
                var currentUserId = int.Parse(User.Identity.Name);

                var feedDto = _mapper.Map<FeedDto>(feed);
                feedDto.CollectionId = collectionId;

                var newFeedDto = _feedService.Create(feedDto, currentUserId);

                return Ok(_mapper.Map<FeedModel>(newFeedDto));
            }
            catch (ValidationException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET api/collections/{collectionId}/feeds
        [HttpGet]
        public IActionResult GetAll(int collectionId)
        {
            try
            {
                var currentUserId = int.Parse(User.Identity.Name);

                var feedDtos = _feedService.GetAll(collectionId, currentUserId);

                return Ok(_mapper.Map<List<FeedModel>>(feedDtos));
            }
            catch (ValidationException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET api/collections/{collectionId}/feeds/{feedId}
        [HttpGet("{feedId}")]
        public IActionResult GetById(int collectionId, int feedId)
        {
            try
            {
                var currentUserId = int.Parse(User.Identity.Name);

                var feedDto = _feedService.GetById(collectionId, feedId, currentUserId);

                return Ok(_mapper.Map<FeedModel>(feedDto));
            }
            catch (ValidationException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT api/collections/{collectionId}/feeds/{feedId}
        [HttpPut("{feedId}")]
        public IActionResult Update(int collectionId, int feedId, [FromBody, Bind("Name, Description, Url")] FeedModel feed)
        {
            try
            {
                var currentUserId = int.Parse(User.Identity.Name);

                var feedDto = _mapper.Map<FeedDto>(feed);
                feedDto.Id = feedId;
                feedDto.CollectionId = collectionId;

                _feedService.Update(feedDto, currentUserId);

                return Ok();
            }
            catch (ValidationException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE api/collections/{collectionId}/feeds/{feedId}
        [HttpDelete("{feedId}")]
        public IActionResult Delete(int collectionId, int feedId)
        {
            try
            {
                var currentUserId = int.Parse(User.Identity.Name);

                _feedService.Delete(collectionId, feedId, currentUserId);

                return Ok();
            }
            catch (ValidationException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET api/collections/{collectionId}/feeds/content
        [HttpGet("content")]
        public IActionResult ReadAll(int collectionId)
        {
            try
            {
                var currentUserId = int.Parse(User.Identity.Name);

                var feedsContent = _readingService.ReadAll(collectionId, currentUserId);

                return Ok(_mapper.Map<List<FeedContentModel>>(feedsContent));
            }
            catch (ValidationException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET api/collections/{collectionId}/feeds/{feedId}/content
        [HttpGet("{feedId}/content")]
        public IActionResult ReadById(int collectionId, int feedId)
        {
            try
            {
                var currentUserId = int.Parse(User.Identity.Name);

                var feedContent = _readingService.ReadById(collectionId, feedId, currentUserId);

                return Ok(_mapper.Map<FeedContentModel>(feedContent));
            }
            catch (ValidationException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET api/collections/{collectionId}/feeds/url?url=
        [AllowAnonymous]
        [HttpGet("url")]
        public IActionResult ReadByUrl([FromQuery] string url)
        {
            try
            {
                var feedContent = _readingService.ReadByUrl(url);

                return Ok(_mapper.Map<FeedContentModel>(feedContent));
            }
            catch (ValidationException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

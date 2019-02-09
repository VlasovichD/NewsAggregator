using AutoMapper;
using FeedAggregator.BLL.Dtos;
using FeedAggregator.BLL.Infrastructure;
using FeedAggregator.BLL.Services;
using FeedAggregator.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FeedAggregator.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CollectionsController : ControllerBase
    {
        private readonly ICollectionManagerService _collectionService;
        private readonly IMapper _mapper;

        public CollectionsController(
            ICollectionManagerService collectionService,
            IMapper mapper)
        {
            _collectionService = collectionService;
            _mapper = mapper;
        }

        // POST api/collections
        [HttpPost]
        public IActionResult Create([FromBody, Bind("Name, Description")] CollectionModel collection)
        {
            try
            {
                var collectionDto = _mapper.Map<CollectionDto>(collection);

                collectionDto.UserId = int.Parse(User.Identity.Name);

                var newCollectionDto = _collectionService.Create(collectionDto);

                return Ok(_mapper.Map<CollectionModel>(newCollectionDto));
            }
            catch (ValidationException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET api/collections
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var currentUserId = int.Parse(User.Identity.Name);

                var collectionDtos = _collectionService.GetAll(currentUserId);

                return Ok(_mapper.Map<List<CollectionDto>>(collectionDtos));
            }
            catch (ValidationException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET api/collections/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var currentUserId = int.Parse(User.Identity.Name);

                var collectionDto = _collectionService.GetById(id, currentUserId);

                return Ok(_mapper.Map<CollectionModel>(collectionDto));
            }
            catch (ValidationException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT api/collections/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody, Bind("Name, Description")] CollectionModel collection)
        {
            try
            {
                var collectionDto = _mapper.Map<CollectionDto>(collection);

                collectionDto.Id = id;
                collectionDto.UserId = int.Parse(User.Identity.Name);

                _collectionService.Update(collectionDto);

                return Ok();
            }
            catch (ValidationException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE api/collections/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var currentUserId = int.Parse(User.Identity.Name);

                _collectionService.Delete(id, currentUserId);

                return Ok();
            }
            catch (ValidationException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

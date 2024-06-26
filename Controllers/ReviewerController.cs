﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ThePokemonProject.Dto;
using ThePokemonProject.Interfaces;
using ThePokemonProject.Models;
using ThePokemonProject.Repositories;

namespace ThePokemonProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewerController : Controller
    {
        private readonly IReviewerRepository _reviewerRepository;
        private readonly IMapper _mapper;

        public ReviewerController(IReviewerRepository reviewerRepository, IMapper mapper)
        {
            _reviewerRepository = reviewerRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Reviewer>))]
        public IActionResult GetReviewers()
        {
            var reviewers = _mapper.Map<List<ReviewerDto>>(_reviewerRepository.GetReviewers());
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(reviewers);
        }
        [HttpGet("{reviewerId}")]
        [ProducesResponseType(200, Type = typeof(Reviewer))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewer(int reviewerId)
        {
            if (!_reviewerRepository.ReviewerExists(reviewerId))
                return NotFound();
            var reviewers = _mapper.Map<ReviewerDto>(_reviewerRepository.GetReviewer(reviewerId));
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(reviewers);
        }
        [HttpGet("review/{reviewerId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewsByReviewer(int reviewerId)
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewerRepository.GetReviewsByReviewer(reviewerId));
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(reviews);

        }
        [HttpGet("reviewer/{reviewId}")]
        [ProducesResponseType(200, Type = typeof(Reviewer))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewerByReview(int reviewId)
        {
            var reviewer = _mapper.Map<ReviewerDto>(_reviewerRepository.GetReviewerByReview(reviewId));
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(reviewer);

        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateReviewer([FromBody] ReviewerDto reviewerCreate)
        {
            if (reviewerCreate == null)
            {
                return BadRequest(ModelState);
            }
            var review = _reviewerRepository.GetReviewers()
                .Where(c => c.LastName.Trim().ToUpper() == reviewerCreate.LastName.TrimEnd().ToUpper()).FirstOrDefault();
            if (review != null)
            {
                ModelState.AddModelError("", "Reviewer Already Exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var reviewerMap = _mapper.Map<Reviewer>(reviewerCreate);

            if (!_reviewerRepository.CreateReviewer(reviewerMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully Created");
        }

        [HttpPut("{reviewerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReviewer(int reviewerId, [FromBody] ReviewerDto reviewerUpdate)
        {
            if (reviewerUpdate == null)
            {
                return BadRequest(ModelState);
            }
            if (reviewerId != reviewerUpdate.Id)
            {
                return BadRequest(ModelState);
            }
            if (!_reviewerRepository.ReviewerExists(reviewerId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var reviewerMap = _mapper.Map<Reviewer>(reviewerUpdate);
            if (!_reviewerRepository.UpdateReviewer(reviewerMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating country");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }
        [HttpDelete("{reviewerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReviewer(int reviewerId)
        {
            if (!_reviewerRepository.ReviewerExists(reviewerId))
            {
                return NotFound();

            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var reviewerToDelete = _reviewerRepository.GetReviewer(reviewerId);
            if (!_reviewerRepository.DeleteReviewer(reviewerToDelete))
            {
                ModelState.AddModelError("", "something went wrong deleting");
            }
            return NoContent();
        }
    }
}



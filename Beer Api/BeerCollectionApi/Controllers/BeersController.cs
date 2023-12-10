using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class BeersController : ControllerBase
{
    private readonly BeerDbContext _context;

    public BeersController(BeerDbContext context)
    {
        _context = context;
    }

    // GET: api/Beers
    [HttpGet]
    public ActionResult<IEnumerable<Beer>> GetBeers()
    {
        return _context.Beers.ToList();
    }

    // GET: api/Beers/search?query=...
    [HttpGet("search")]
    public ActionResult<IEnumerable<Beer>> SearchBeers(string query)
    {
        if (string.IsNullOrEmpty(query))
        {
            return BadRequest("Query parameter 'query' is required for searching.");
        }

        var results = _context.Beers.Where(beer => beer.Name.Contains(query, System.StringComparison.OrdinalIgnoreCase)).ToList();
        return Ok(results);
    }

    [HttpPost("CreateBeer")]
    public ActionResult<Beer> CreateBeer(CreateBeer beer)
    {
        if (string.IsNullOrEmpty(beer.Name) || string.IsNullOrEmpty(beer.Type))
        {
            return BadRequest("Name and type are required for a beer.");
        }

        Beer newBeer = new Beer();
        newBeer.Name = beer.Name;
        newBeer.Type = beer.Type;

        if (beer.Rating > 0 & beer.Rating <= 5)
        {
            newBeer.Ratings.Add(beer.Rating);
            newBeer.AverageRating = beer.Rating;
        }

       _context.Beers.Add(newBeer);

        _context.SaveChanges();

       return Ok("Success");
    }

    // PATCH: api/Beers/{id}/rating
    [HttpPatch("{id}/rating")]
    public ActionResult<Beer> UpdateBeerRating(int id, int rating)
    {
        var beer = _context.Beers.Find(id);

        if (beer == null)
        {
            return NotFound("Beer not found.");
        }

        if (rating < 1 || rating > 5)
        {
            return BadRequest("Rating must be a number between 1 and 5.");
        }

        beer.Ratings.Add(rating);
        beer.AverageRating = beer.Ratings.Sum() / (double)beer.Ratings.Count;

        _context.Entry(beer).State = EntityState.Modified;
        _context.SaveChanges();

        return Ok(beer);
    }
}

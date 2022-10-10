using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHero.API.Database;
using SuperHero.API.Models;

namespace SuperHero.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly AppDatabase database;

        public SuperHeroController(AppDatabase database)
        {
            this.database = database;
        }
        [HttpGet]
        public async Task<ActionResult<List<SuperHeroModel>>> GetSuperHeroes()
        {
            return Ok(await database.SuperHeroes.ToListAsync());
        }
        [HttpPost]
        public async Task<ActionResult<SuperHeroModel>> CreateSuperHero(SuperHeroModel hero)
        {
            database.SuperHeroes.Add(hero);
            await database.SaveChangesAsync();
            return Ok(hero);
        }
        [HttpPut]
        public async Task<ActionResult<SuperHeroModel>> UpdateSuperHero(SuperHeroModel hero)
        {
            var dbHero = await database.SuperHeroes.FindAsync(hero.Id);
            if (dbHero != null)
            {
                dbHero.Name = hero.Name;
                dbHero.FirstName = hero.FirstName;
                dbHero.LastName = hero.LastName;
                dbHero.Place = hero.Place;
                await database.SaveChangesAsync();
                return Ok(hero);
            }
            else
            {
                return BadRequest($"Can't find hero with ID : {hero.Id}");
            }
        }
        [HttpDelete("{Id}")]
        public async Task<ActionResult<SuperHeroModel>> DeleteSuperHero(int Id)
        {
            var dbHero = await database.SuperHeroes.FindAsync(Id);
            if(dbHero!=null)
            {
                database.SuperHeroes.Remove(dbHero);
                await database.SaveChangesAsync();
                return Ok(dbHero);
            }
            else
            {
                return BadRequest($"Can't find hero with ID : {Id}");
            }
        }
    }
}

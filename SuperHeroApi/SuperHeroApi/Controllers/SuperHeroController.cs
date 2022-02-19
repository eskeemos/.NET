using Microsoft.AspNetCore.Mvc;

namespace SuperHeroApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly Context context;

        public SuperHeroController(Context context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetHeros()
        {
            var heros = await context.SuperHeroes.ToListAsync();

            return Ok(heros);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> GetHeroById([FromRoute] int id)
        {
            var hero = await context.SuperHeroes.FindAsync(id);

            if(hero != null) return Ok(hero);

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateHero(SuperHero model)
        {
            context.SuperHeroes.Add(model);

            await context.SaveChangesAsync();

            return Ok(model.Id);
        }

        [HttpPut]
        public async Task<ActionResult<SuperHero>> UpdateHero([FromBody] SuperHero model)
        {
            var hero = await context.SuperHeroes.FindAsync(model.Id);

            if (hero != null)
            {
                hero.Name = model.Name;
                hero.FirstName = model.FirstName;
                hero.LastName = model.LastName;
                hero.Place = model.Place;

                await context.SaveChangesAsync();

                return Ok(hero);
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> RemoveHero([FromRoute] int id)
        {
            var hero = await context.SuperHeroes.FindAsync(id);

            if (hero != null)
            {
                context.SuperHeroes.Remove(hero);

                await context.SaveChangesAsync();

                return Ok(hero.Id);
            }

            return NotFound();
        }
    }
}

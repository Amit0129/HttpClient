using API.Context;
using Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/people")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly PersonDB_Context dB_Context;
        public PersonController(PersonDB_Context dB_Context)
        {
            this.dB_Context = dB_Context;
        }
        [HttpPost]
        public async Task<Person> AddPerson(Person person)
        {
            try
            {
                dB_Context.PersonInfo.Add(person);
                await dB_Context.SaveChangesAsync();
                return person;
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetAllPersonInfo()
        {
            try
            {
                return await dB_Context.PersonInfo.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateAPersonInfo(int id,Person person)
        {
            try
            {
                var personInfo = dB_Context.PersonInfo.FirstOrDefault(x => x.Id == id);
                if (personInfo == null)
                {
                    return NotFound();
                }
                else
                {
                    personInfo.Name = person.Name;
                    dB_Context.PersonInfo.Update(personInfo);
                    dB_Context.SaveChanges();
                    return NoContent();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

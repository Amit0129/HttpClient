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
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspProject.ServiceHosting.Controllers
{
    //шаблон маршрута по умолчанию
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private static readonly List<string> __Values = Enumerable
            .Range(1, 10)
            .Select(i => $"Value-{i:00}")
            .ToList();

        //Тут два вариента - если режим отладки - можно выбрать ActionResult
        [HttpGet] // http://localhost:5001/api/values
        public IEnumerable<string> Get() => __Values;

        //[HttpGet] // http://localhost:5001/api/values
        //public ActionResult<IEnumerable<string>> Get() => __Values;

        [HttpGet("{id}")] // http://localhost:5001/api/values/5
        public ActionResult<string> Get(int id)
        {
            if (id < 0)
                return BadRequest();
            if (id >= __Values.Count)
                return NotFound();

            return __Values[id];
        }
        //Добавить новую запись
        [HttpPost]                  // post -> http://localhost:5001/api/values
        [HttpPost("add")]    // post -> http://localhost:5001/api/values/add
        public ActionResult Post( string Str)
        {
            __Values.Add(Str);
            //return Ok();
            return CreatedAtAction(nameof(Get), __Values[__Values.Count-1]);
            // http://localhost:5001/api/values/10
        }

        //добавить занчение по указанному индексу
        [HttpPut("{id}")]       // put -> http://localhost:5001/api/values/5
        [HttpPut("edit/{id}")]  // put -> http://localhost:5001/api/values/edit/5
        public ActionResult Put(int id, string Str)
        {
            if (id < 0)
                return BadRequest();
            if (id >= __Values.Count)
                return NotFound();

            __Values[id] = Str;

            return Ok();
        }

        [HttpDelete("{id}")] // delete -> http://localhost:5001/api/values/5
        public ActionResult Delete(int id)
        {
            if (id < 0)
                return BadRequest();
            if (id >= __Values.Count)
                return NotFound();

            __Values.RemoveAt(id);

            return Ok();
        }
    }
}

﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace PaginationApi.Controllers
{
    public class TestClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IEnumerable<TestClass> GetListOfTestObjects(int numberOfTestObjects)
        {
            for (int i = 1; i <= numberOfTestObjects; i++)
                yield return new TestClass() { Id = i, Name = $"Name of {i}"};
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<TestClass>> Get(
            int currentPageNumber = 1,
            int itemsPerPage = 1)
        {
            var results = GetListOfTestObjects(100);

            return Ok(results);
        }
    }
}

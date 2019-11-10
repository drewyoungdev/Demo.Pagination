using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PaginationLib;

namespace PaginationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private const int TEST_NUMBER_OF_OBJECTS = 100;
        private const int DEFAULT_ITEMS_PER_PAGE = 5;

        private IEnumerable<object> GetListOfTestObjects()
        {
            for (int i = 1; i <= TEST_NUMBER_OF_OBJECTS; i++)
                yield return new { Id = i, Name = $"Name of {i}"};
        }

        // GET api/values
        // No pagination
        [HttpGet]
        public ActionResult<IEnumerable<object>> Get()
        {
            var items = GetListOfTestObjects();

            return Ok(items);
        }

        // GET api/values/paginated
        // Alternatively we could allow clients to send in their own "items per page".
        // In this example we just control it from API to keep the endpoint query params minimal.
        // On initial GET of this endpoint (no query params), we just pull the first page for the client.
        [HttpGet("paginated")]
        public ActionResult<PagedResult<object>> GetPaginated(int? currentPageNumber, int? pageSize)
        {
            var currentPageNo = currentPageNumber ?? 1;
            var itemsPerPage = pageSize ?? DEFAULT_ITEMS_PER_PAGE;

            var pagedResult = GetListOfTestObjects()
                .ToPagedResult(currentPageNo, itemsPerPage);

            return Ok(pagedResult);
        }
    }
}

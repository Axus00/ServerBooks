
using Microsoft.AspNetCore.Mvc;

namespace Books.App.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController
    {
        private readonly IBooksRepository _couponsRepository;
        public UsersController(IBooksRepository couponsRepository)
        {
            _couponsRepository = couponsRepository;
        }

        [HttpGet]
        public int GetAll()
        {
        return 1;
        }
            
    }
}
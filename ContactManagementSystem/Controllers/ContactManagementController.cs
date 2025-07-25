using Microsoft.AspNetCore.Mvc;

namespace ContactManagementSystem.Controllers
{
    public class ContactManagementController : ControllerBase
    {
        private readonly IContactRepository _repository;

        public ContactManagementController(IContactRepository repository)
       
        {
            _repository = repository;
        }
        [HttpPost("Add")]
        public IActionResult Add(Contact contact) => Ok(_repository.Add(contact));

        [HttpGet("{mobile}")]
        public IActionResult Get(string mobile) => Ok(_repository.Get(mobile));

        [HttpGet("GetAll")]
        public IActionResult GetAll() => Ok(_repository.GetAll());

        [HttpPost("{Update}")]
        public IActionResult Update(Contact contact) => Ok(_repository.Update(contact));

        [HttpGet("delete/{mobile}")]
        public IActionResult GetDelete(string mobile) => Ok(_repository.Delete(mobile));
    }
    
}


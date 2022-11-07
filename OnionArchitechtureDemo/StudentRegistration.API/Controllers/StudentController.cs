using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using StudentRegistration.API.Validators;
using StudentRegistration.Domain.Data;
using StudentRegistration.Domain.Models;
using StudentRegistration.Service.ICustomServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentRegistration.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly ICustomService<Student> _customService;
        private readonly StudentContext _applicationDbContext;
        private readonly IValidator<Student> _validator;
        public StudentController(ICustomService<Student> customService, StudentContext applicationDbContext, IValidator<Student> studentvalidator)
        {
            _customService = customService;
            _applicationDbContext = applicationDbContext;
            _validator = studentvalidator;
        }
        [HttpGet(nameof(GetStudentById))]
        public IActionResult GetStudentById(int Id)
        {
            var obj = _customService.Get(Id);
            if (obj == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(obj);
            }
        }
        [HttpGet(nameof(GetAllStudent))]
        public IActionResult GetAllStudent()
        {
            var obj = _customService.GetAll();
            if (obj == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(obj);
            }
        }
        [HttpPost(nameof(CreateStudent))]
        public IActionResult CreateStudent(Student student)
        {
            var validation = _validator.Validate(student);
            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }
            else
            {
                _customService.Insert(student);
                return Ok("Student Created Successfully");
            }
        }
        [HttpPost(nameof(UpdateStudent))]
        public IActionResult UpdateStudent(Student student)
        {
            var validation = _validator.Validate(student);
            if (!validation.IsValid)
            {
                return BadRequest(validation.Errors);
            }
            else
            {
                _customService.Update(student);
                return Ok("Student Updated SuccessFully");
            }
        }
        [HttpDelete(nameof(DeleteStudent))]
        public IActionResult DeleteStudent(Student student)
        {
            if (student != null)
            {
                _customService.Delete(student);
                return Ok("Deleted Successfully");
            }
            else
            {
                return BadRequest("Something went wrong");
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Lab_1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly ILogger<StudentsController> _logger;

        public StudentsController(ILogger<StudentsController> logger)
        {
            _logger = logger;
        }

        private static readonly List<Teacher> _teachers = new List<Teacher>
        {
            new Teacher { Id = 1, Name = "Mrs. Smith" },
            new Teacher { Id = 2, Name = "Mr. Johnson" },
            new Teacher { Id = 3, Name = "Ms. Davis" },
            new Teacher { Id = 4, Name = "Dr. Lee" },
            new Teacher { Id = 5, Name = "Mrs. Brown" }
        };

        private static readonly List<Student> _students = new List<Student>
        {
            new Student { Id = 1, Name = "Alice", Age = 10, TeacherId = 1, Teacher = _teachers.First(t => t.Id == 1) },
            new Student { Id = 2, Name = "Bob", Age = 11, TeacherId = 2, Teacher = _teachers.First(t => t.Id == 2) },
            new Student { Id = 3, Name = "Charlie", Age = 12, TeacherId = 1, Teacher = _teachers.First(t => t.Id == 1) },
            new Student { Id = 4, Name = "Diana", Age = 10, TeacherId = 3, Teacher = _teachers.First(t => t.Id == 3) },
            new Student { Id = 5, Name = "Ethan", Age = 13, TeacherId = 4, Teacher = _teachers.First(t => t.Id == 4) },
            new Student { Id = 6, Name = "Fiona", Age = 11, TeacherId = 5, Teacher = _teachers.First(t => t.Id == 5) },
            new Student { Id = 7, Name = "George", Age = 12, TeacherId = 2, Teacher = _teachers.First(t => t.Id == 2) },
            new Student { Id = 8, Name = "Hannah", Age = 10, TeacherId = 3, Teacher = _teachers.First(t => t.Id == 3) }
        };

        [HttpGet("{id:int}")]
        public ActionResult<Student> Get(int id)
        {
            var student = _students.FirstOrDefault(s => s.Id == id);
            if (student == null) return NotFound();
            student.Teacher = _teachers.FirstOrDefault(t => t.Id == student.TeacherId);
            return Ok(student);
        }

        [HttpPost]
        public ActionResult<Student> Post([FromBody] Student student)
        {
            if (student == null) return BadRequest();
            if (!_teachers.Any(t => t.Id == student.TeacherId))
                return BadRequest("TeacherId does not exist.");

            student.Id = _students.Any() ? _students.Max(s => s.Id) + 1 : 1;
            student.Teacher = _teachers.First(t => t.Id == student.TeacherId);
            _students.Add(student);
            return CreatedAtAction(nameof(Get), new { id = student.Id }, student);
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] Student updated)
        {
            var existing = _students.FirstOrDefault(s => s.Id == id);
            if (existing == null) return NotFound();
            if (!_teachers.Any(t => t.Id == updated.TeacherId))
                return BadRequest("TeacherId does not exist.");

            existing.Name = updated.Name;
            existing.Age = updated.Age;
            existing.TeacherId = updated.TeacherId;
            existing.Teacher = _teachers.First(t => t.Id == updated.TeacherId);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var existing = _students.FirstOrDefault(s => s.Id == id);
            if (existing == null) return NotFound();

            _students.Remove(existing);
            return NoContent();
        }

        [HttpGet("hello")]
        public IActionResult Hello()
        {
            return Content("Hello World", "text/plain");
        }
    }
}
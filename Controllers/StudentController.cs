using Microsoft.AspNetCore.Mvc;
using testpraktikum.Models;
using testpraktikum.Repositories;

namespace testpraktikum.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentController : ControllerBase
    {
        private readonly StudentRepository repo = StudentRepository.Instance;

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(repo.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var student = repo.GetAll().FirstOrDefault(x => x.Id == id);
            return student == null ? NotFound() : Ok(student);
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (string.IsNullOrEmpty(student.Nama) || student.Umur <= 0)
                return BadRequest("Data mahasiswa tidak valid");

            var data = repo.GetAll();
            student.Id = data.Count == 0 ? 1 : data.Max(x => x.Id) + 1;
            data.Add(student);
            repo.Save(data);

            return Ok(student);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Student student)
        {
            var data = repo.GetAll();
            var existing = data.FirstOrDefault(x => x.Id == id);
            if (existing == null) return NotFound();

            existing.Nama = student.Nama;
            existing.Umur = student.Umur;
            existing.Jurusan = student.Jurusan;
            repo.Save(data);

            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var data = repo.GetAll();
            var student = data.FirstOrDefault(x => x.Id == id);
            if (student == null) return NotFound();

            data.Remove(student);
            repo.Save(data);

            return Ok();
        }
    }
}

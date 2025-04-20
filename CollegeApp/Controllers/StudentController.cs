using Microsoft.AspNetCore.Mvc;
using CollegeApp.Models;
using CollegeApp.Repository;
using System.Reflection.Metadata.Ecma335;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(200, Type = typeof(Student))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(404)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        [Route("All", Name = "GetAllStudents")]
        public ActionResult<IEnumerable<Student>> GetStudents()
        {
            //// normal way
            //var students = new List<StudentDTO>();
            //foreach (var item in CollegeRepository.Students)
            //{
            //    StudentDTO obj = new StudentDTO
            //    {
            //        Id = item.Id,
            //        StudentName = item.StudentName,
            //        Email = item.Email,
            //        Address = item.Address
            //    };
            //    students.Add(obj);
            //}

            //// using LINQ
            var students = CollegeRepository.Students.Select(s => new StudentDTO
            {
                Id = s.Id,
                StudentName = s.StudentName,
                Email = s.Email,
                Address = s.Address,
            });

            // ok = success = 200 status code
            return Ok(CollegeRepository.Students);
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetStudentsById")]
        public ActionResult<StudentDTO> GetStudentById(int id)
        {
            //Bad Request = 400 = Client error
            if (id <= 0)
                return BadRequest();

            var student = CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();
            // Not Found = 404 = client error
                if (student == null)
                    return NotFound($"The student with id {id} is not found");

            var studentDTO = new StudentDTO
            {
                Id = student.Id,
                StudentName = student.StudentName,
                Email = student.Email,
                Address = student.Address
            };
            // ok = success = 200 status code
            return Ok(studentDTO);
        }

        [HttpGet]
        [Route("{name:alpha}", Name = "GetStudentByName")]
        public ActionResult<Student> GetStudentByName(string name)
        {
            //Bad Request = 400 = Client error
            if (string.IsNullOrEmpty(name))
                return BadRequest();

            var student = CollegeRepository.Students.Where(n => n.StudentName == name).FirstOrDefault();
            // Not Found = 404 = client error
            if (student == null)
                return NotFound($"The student with name {name} not found");

            // ok = success = 200 status code
            return Ok(student);
        }

        [HttpPost]
        [Route("Create")]
        //api/student/create
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<StudentDTO> CreateStudent([FromBody]StudentDTO model)
        { 
            if (model == null)
                return BadRequest();

            //if (model.AdmissionDate <= DateTime.Now)
            //{
            //    // 1. Directly adding error messaege to modelstate
            //    // 2. Using Custom Attribute
            //    ModelState.AddModelError("AdmissionDate Error", "Admission date should be greater than or equal to today");
            //    return BadRequest(ModelState);
            //}

            int newId = CollegeRepository.Students.LastOrDefault().Id + 1;
            Student student = new Student
            {
                Id = newId,
                StudentName = model.StudentName,
                Email = model.Email,
                Address = model.Address
            };
            CollegeRepository.Students.Add(student);

            model.Id = student.Id;

            // Created = 201 = success
            // https://localhost:pornt/api/student/1
            // new student details
            model.Id = student.Id;
            return CreatedAtRoute("GetStudentsById", new { id = model.Id }, model);
        }

        [HttpPut]
        [Route("Update")]
        //api/student/update
        public ActionResult<StudentDTO> UpdateStudent([FromBody] StudentDTO model)
        {
            if (model == null || model.Id <= 0)
                BadRequest();

            var existingStudent = CollegeRepository.Students.Where(s => s.Id == model.Id).FirstOrDefault();
            
            if (existingStudent == null)
                return NotFound();
            
            existingStudent.StudentName = model.StudentName;
            existingStudent.Email = model.Email;
            existingStudent.Address = model.Address;

            return NoContent();
        }

        [HttpDelete]
        [Route("{id:int:min(1):max(100)}", Name = "DeleteStudents")]
        public ActionResult<bool> DeleteStudent(int id)
        {
            //Bad Request = 400 = Client error
            if (id <= 0)
                return BadRequest();

            var student = CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();
            // Not Found = 404 = client error
            if (student == null)
                return NotFound($"The student with id {id} not found");

            CollegeRepository.Students.Remove(student);

            // ok = success = 200 status code
            return Ok(student);
        }
    }
}
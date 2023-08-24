using backendForCollegeStudents.EntityFrameworkConfig;
using backendForCollegeStudents.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backendForCollegeStudents.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        EntityFrameworkDbContext Ef = new EntityFrameworkDbContext();
        [HttpGet]
        public string GetName()
        {
            var List=Ef.Student.Where(e => e.Sex == "").Count();
            return List.ToString();
        }
        [HttpPost]
        public async Task<bool> InsertStudent(Student stu)
        {
            await Ef.Student.AddAsync(stu);
            return await Ef.SaveChangesAsync()>0;
        }
    }
}

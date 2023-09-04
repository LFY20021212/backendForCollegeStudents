using backendForCollegeStudents.EntityFrameworkConfig;
using backendForCollegeStudents.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        [HttpPost]
        public async Task<string> PostAdd()
        {
            Student stu = new Student() { BornDateTime = DateTime.MaxValue, Sex = "男", StudentName = "zhangsan" };
            var name = new
            {
                MD_UserRegisterOID = "CCC科技公司",
                FullName = "CC",
                UnitName = "UnitName",
                TelPhone = "185625625002",
                RegistrateTime = "2023-09-09",
                Product = "10001"
            };
        
            return JsonConvert.SerializeObject(name);
        }
        [HttpPost]
        public async Task<UserRegister> PName(string name)
        {
            return JsonConvert.DeserializeObject<UserRegister>(name);
        }


    }
    public class UserRegister
    {
        public string MD_UserRegisterOID { get; set; }
        public string TelPhone { get; set; }//手机号
        public DateTime RegistrateTime { get; set; } //注册时间
        public string FullName { get; set; } //姓名
        public string UnitName { get; set; } //企业名称
        public string Product { get; set; } //关注产品
    }
}

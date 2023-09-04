using backendForCollegeStudents.EntityFrameworkConfig;
using backendForCollegeStudents.MapperModels;
using backendForCollegeStudents.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Drawing;

namespace backendForCollegeStudents.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Serializable]
    public class LoginController : ControllerBase
    {
        EntityFrameworkDbContext Ef = new EntityFrameworkDbContext();
        /// <summary>
        /// 检查当前用户是否存在
        /// </summary>
        /// <param name="LoginName">登陆账号</param>
        /// <param name="LoginPwd">登录名称</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Login> LoginCheck(LoginModel login)
        {
            var Result= await Ef.Logins.FirstOrDefaultAsync(e => e.LoginName == login.LoginName && e.LoginPwd == login.LoginPwd);
            return Result;
        }
        /// <summary>
        /// 添加新用户
        /// </summary>
        /// <param name="login">注册信息</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> InsertLogin(Login login)
        {
            //判断当前账号是否已经注册
            if (await Ef.Logins.CountAsync(e => e.LoginName == login.LoginName) > 0)
                return false;
            //保存到数据库当中
                await Ef.Logins.AddAsync(login);
            return await Ef.SaveChangesAsync() > 0;
        }
    }
}

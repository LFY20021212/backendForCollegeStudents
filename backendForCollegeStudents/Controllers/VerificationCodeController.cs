using backendForCollegeStudents.EntityFrameworkConfig;
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
    public class VerificationCodeController : ControllerBase
    {
        EntityFrameworkDbContext Db = new EntityFrameworkDbContext();
        [HttpPost]
        public async Task<FileContentResult> Execl()
        {
            // 加载模板Excel文件
            FileInfo templateFile = new FileInfo("template.xlsx");
            if (!templateFile.Exists)
            {
                throw new Exception("模板文件不存在！");
            }
            // 设置EPPlus许可上下文
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            // 创建一个新的Excel包，基于模板
            using (var package = new ExcelPackage(templateFile))
            {
                // 获取工作表
                ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet1"];
                // 在工作表中写入数据
                worksheet.Cells["J14"].Value = "Demo";
                worksheet.Cells["J15"].Value = "这是一个案例Demo";
                // 保存导出的Excel文件
                var excelBytes = package.GetAsByteArray();
                var result = new FileContentResult(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                result.FileDownloadName = "output.xlsx";
                return result;
            }
        }

        /// <summary>
        /// 导出ExeclCode内容
        /// </summary>
        /// <param name="CodeType">如果为空值返回全部数据，反之通过A-H进行筛选</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        public async Task<FileContentResult> ExeclVerificationCoreTemplate(string? CodeType)
        {
            // 加载模板Excel文件
            FileInfo templateFile = new FileInfo("VerificationCodeTemplate.xlsx");
            if (!templateFile.Exists)
            {
                throw new Exception("模板文件不存在！");
            }
            // 设置EPPlus许可上下文
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            //创建一个新的Excel包，基于模板
            //通过EF获取数据库数据
            List<VerificationCode> CodeData = new List<VerificationCode>();
            if (string.IsNullOrEmpty(CodeType))
            {
                CodeData = await Db.VerificationCodes.ToListAsync();
            }               
            else
            {
                CodeData = await Db.VerificationCodes.Where(e => e.CodeType == CodeType).ToListAsync();
            }
            using (var package = new ExcelPackage(templateFile))
            {
                // 获取工作表
                ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet1"];
                // 在工作表中写入数据
                for (int i = 0; i < CodeData.Count; i++)
                {
                    worksheet.Cells["A"+ (i + 2)].Value = CodeData[i].Id;
                    worksheet.Cells["B"+ (i + 2)].Value = CodeData[i].CodeContent;
                    worksheet.Cells["C"+ (i + 2)].Value = CodeData[i].User;
                }                
                // 保存导出的Excel文件
                var excelBytes = package.GetAsByteArray();
                var result = new FileContentResult(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                var OutPutDateTime = DateTime.Now.ToString("yyyy-MM-dd");
                result.FileDownloadName = OutPutDateTime+ "VerificationCore.xlsx";
                return result;
            }
        }
        [HttpPost]
        public async Task<FileContentResult> ExeclTest()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                // 在工作表中写入数据
                worksheet.Cells["A1"].Value = "id";
                worksheet.Cells["B1"].Value = "年龄";

                worksheet.Cells["A2"].Value = "Alice";
                worksheet.Cells["B2"].Value = 25;

                worksheet.Cells["A3"].Value = "Bob";
                worksheet.Cells["B3"].Value = 30;

                // 设置数据行样式
                using (var range = worksheet.Cells["A1:B3"])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                }

                var excelBytes = package.GetAsByteArray();
                var result = new FileContentResult(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                result.FileDownloadName = "output.xlsx";
                return result;
            }
        }
    }
}

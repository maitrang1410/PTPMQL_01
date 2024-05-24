using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MvcMovie.Data;
using MvcMovie.Models;
using SQLitePCL;
using System.Diagnostics.SymbolStore;
using System.Linq.Expressions;
using MvcMovie.Models.Process;
using OfficeOpenXml;

namespace MvcMovie.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext  _context;
        public StudentController(ApplicationDbContext context)
        {
            _context = context;
    
        }
        private readonly ExcelProcess _excelProcess = new ExcelProcess() ;
        public async Task<IActionResult> Index()
        {
           // return _context.Student !=null ?
          //  View(await _context.Student.ToListAsync()):
          //  Problem ("Entity set 'ApplicationDbContext.Student'  is null." );
            var student = await _context.Student.ToListAsync();
            return View(student);
            }
        public IActionResult Create(){
            return View();
            }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId , FullName")] Student student){
            if (ModelState.IsValid){
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);

        }
    public async Task<IActionResult> Edit(string id ){
        if ( id == null || _context.Student == null)
        {
            return NotFound();
        }
        var student = await _context.Student.FindAsync(id);
        if (student == null)
        {
            return NotFound();
        }
        return View(student);
        }
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(string id, [Bind("StudentId, FullName")]  Student student){
    if (id != student.StudentId )
    {
        return NotFound();
    }
    if (ModelState.IsValid)
    {
        try{
            _context.Update(student);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if(!StudentExists(student.StudentId))
            {
                return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
            }
            return View(student);
            }
    public async Task<IActionResult> Delete(string id)
    {
        if (id == null || _context.Student ==null)
        {
            return NotFound();
        }
        var student = await _context.Student.FirstOrDefaultAsync(m => m.StudentId ==id );
        if (student == null)
        {
             return NotFound();
            }
            return View (student);

}   
[HttpPost, ActionName("Delete")]
[ValidateAntiForgeryToken]
 public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Student == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Student'  is null.");
            }
            var student = await _context.Student.FindAsync(id);
            if (student != null)
            {
                _context.Student.Remove(student);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
         [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null)
            {
                string fileExtension = Path.GetExtension(file.FileName);
                if (fileExtension != ".xls" && fileExtension != ".xlsx")
                {
                    ModelState.AddModelError("", "plese upload excel!");
                }
                else
                // rename to sever
                {
                    var fileName = DateTime.Now.ToShortTimeString() + fileExtension;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory() + "/Upload/Excels", fileName);
                    var fileLocation = new FileInfo(filePath).ToString();
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        //save file to sever
                        await file.CopyToAsync(stream);
                        //read data from excel to file from dt
                        var  dt = _excelProcess.ExcelToDataTable(fileLocation);
                        //using for loop  to read date from dt
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                       string StudentId = dt.Rows[i][0].ToString();
                           if (!await _context.Student.AnyAsync(p => p.StudentId == StudentId))
                              {
                                var st = new Student
                                  {
                              StudentId = dt.Rows[i][0].ToString(), // Gán giá trị cho thuộc tính StudentId
                                 FullName = dt.Rows[i][1].ToString()
            
                                 };
                               _context.Add(st);
                                 }
                                    else
                                     {
                                     // Xử lý trường hợp PersonId đã tồn tại, ví dụ như bỏ qua bản ghi hoặc thông báo lỗi
                                 }
}
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));

                    }
                }
            }
            return View();

        }
       
       public IActionResult Download()
        {
            var fileName = "StudentList.xlsx";
            using(ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet Worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");
                Worksheet.Cells["A1"].Value = "StudentId";
                Worksheet.Cells["B1"].Value = "FullName";
                //excelWorksheet.Cells["C1"].Value = "Address";
                var studentList = _context.Student.ToList();
                Worksheet.Cells["A2"].LoadFromCollection(studentList);
                var stream = new MemoryStream(excelPackage.GetAsByteArray());
                return File(stream,"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",fileName);
            }
        }

private bool StudentExists(string id)
{
    return (_context.Student?.Any ( e => e.StudentId == id ) ).GetValueOrDefault();
    }
}
}
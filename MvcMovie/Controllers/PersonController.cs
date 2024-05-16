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
    public class PersonController : Controller
{
    private readonly  ApplicationDbContext _context ;//dòng 11 -15 là khai báo applicationDbContext đển làm việc vs CSDL
    private readonly ExcelProcess _excelProcess = new ExcelProcess() ;

    public PersonController(ApplicationDbContext context)
    {
        _context=context;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _context.Persons.ToListAsync();
            return View(model);
        }
       // GET: Person/Create
        public IActionResult Create()
        {
            return View();
        }
        //upload/excels
        public async Task<IActionResult> Upload()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file!=null)
                {
                    string fileExtension = Path.GetExtension(file.FileName);// Path.GetExtension trích xuất phần mở rộng của file 
                    if (fileExtension != ".xls" && fileExtension != ".xlsx")
                    {
                        ModelState.AddModelError("", "Please choose excel file to upload!");
                    }
                    else
                    {
                        //rename(đổi) file when upload to server
                        var fileName =DateTime.Now.ToShortTimeString()+ fileExtension;
                        var filePath = Path.Combine(Directory.GetCurrentDirectory() + "/Upload/Excels",fileName);
                        var fileLocation = new FileInfo(filePath).ToString();//khởi tạo đối tượng FileInfo từ đường dẫn tệp filePath, toString chuyển đổi đối tượng thành 1 chuỗi
                        if (file.Length > 0)
                        {
                            using (var stream = new FileStream(filePath, FileMode.Create))//FileMode.Create` chỉ định rằng nếu tệp tin không tồn tại, một tệp mới sẽ được tạo ra. Nếu tệp tin đã tồn tại, nó sẽ bị ghi đè.

                            {
                                //save file to server
                                await file.CopyToAsync(stream);
                                //read data from file and write to database
                                var dt = _excelProcess.ExcelToDataTable(fileLocation);//đọc DL  excel sang datatable; excelToDataTable là pthuc của lớp ExcelProcess ;fileLocation đường dẫn File Execl đã tải lên
                                for(int i = 0; i < dt.Rows.Count; i++)
                                {
                                    var ps = new Person();//khởi tạo đối tượng Person
                                    //gán gtri từ datatabke vào person
                                    ps.PersonId = dt.Rows[i][0].ToString();
                                    ps.FullName = dt.Rows[i][1].ToString();
                                    ps.Address = dt.Rows[i][2].ToString();
                                    _context.Add(ps);
                                }
                                await _context.SaveChangesAsync();
                                return RedirectToAction(nameof(Index));
                            }
                        }
                    }
                }
            
            return View();
        }

        // POST: Person/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonID,FullName,Address,Age")] Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: Person/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Persons == null)
            {
                return NotFound();
            }

            var person = await _context.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: Person/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PersonID,FullName,Address,Age")] Person person)
        {
            if (id != person.PersonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.PersonId))
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
            return View(person);
        }

        // GET: Person/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Persons == null)
            {
                return NotFound();
            }

            var person = await _context.Persons
                .FirstOrDefaultAsync(m => m.PersonId == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: Person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Persons == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Person'  is null.");
            }
            var person = await _context.Persons.FindAsync(id);
            if (person != null)
            {
                _context.Persons.Remove(person);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Download()
        {
            var fileName = "PersonList.xlsx";
            using(ExcelPackage excelPackage = new ExcelPackage())//tạo một tệp execl mới và tệp sẽ tự giải phóng khi k còn cần thiết (nếu tệp )
            {
                ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");
                excelWorksheet.Cells["A1"].Value = "PersonID";
                excelWorksheet.Cells["B1"].Value = "FullName";
                excelWorksheet.Cells["C1"].Value = "Address";
                var psList = _context.Persons.ToList();// truy vấn vào CSDL trả về (danh sách)bản ghi từ  bảng Persons
                excelWorksheet.Cells["A2"].LoadFromCollection(psList);
                var stream = new MemoryStream(excelPackage.GetAsByteArray());
                return File(stream,"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",fileName);
            }
        }

        private bool PersonExists(string id)
        {
          return (_context.Persons?.Any(e => e.PersonId == id)).GetValueOrDefault();
        }
    }
}
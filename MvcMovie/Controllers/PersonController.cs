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
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _context.Persons.ToListAsync();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonId,FullName,Address")] Person person)
        {
            if (ModelState.IsValid)
            {var existingPerson = await _context.Persons.FindAsync(person.PersonId);
        if (existingPerson != null)
        {
            ModelState.AddModelError("PersonId", "Một person với Id này đã tồn tại.");
            return View(person);
        }

                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PersonId,FullName,Address")] Person person)
        {
            if (id != person.PersonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                  //  _context.Update(person);
                  //  await _context.SaveChangesAsync();
                  var existingPerson = await _context.Persons.FirstOrDefaultAsync(p => p.PersonId == person.PersonId && p.PersonId != id);
            if (existingPerson != null)
            {
                ModelState.AddModelError("PersonId", "Một person với ID này đã tồn tại.");
                return View(person);
            }

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
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>    DeleteConfirmed(string id)
        {
            if (_context.Persons == null)
            {
                return Problem("Entity set 'ApplicationDbcontext.Person' is null.");
            } 
            var person  = await _context.Persons.FindAsync(id);  
            if (person != null) 
            {
            _context.Persons.Remove(person);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Upload()
        {
            return View();
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
                       string personId = dt.Rows[i][0].ToString();
                           if (!await _context.Persons.AnyAsync(p => p.PersonId == personId))
                         {
                                 var ps = new Person
                         {
                                PersonId = personId,
                                 FullName = dt.Rows[i][1].ToString(),
                                Address = dt.Rows[i][2].ToString()
                         };
                         _context.Add(ps);
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
            var fileName = "PersonList.xlsx";
            using(ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");
                excelWorksheet.Cells["A1"].Value = "PersonID";
                excelWorksheet.Cells["B1"].Value = "FullName";
                excelWorksheet.Cells["C1"].Value = "Address";
                var psList = _context.Persons.ToList();
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
        
        
    

    

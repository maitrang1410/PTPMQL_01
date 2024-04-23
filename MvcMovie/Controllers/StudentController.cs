using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext  _context;
        public StudentController(ApplicationDbContext context)
        {
            _context = context;
    
        }
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
private bool StudentExists(string id)
{
    return (_context.Student?.Any ( e => e.StudentId == id ) ).GetValueOrDefault();
    }
}
}
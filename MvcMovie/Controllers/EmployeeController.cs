using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;

namespace MvcMovie.Controllers;

public class EmployeeController : Controller {
   private readonly  ApplicationDbContext _context ; //dòng 11 -15 là khai báo applicationDbContext đển làm việc vs CS
    public EmployeeController(ApplicationDbContext context)
    {
        _context=context;
        }
        public async Task<IActionResult> Index()
        {// action Index trả về view 1 list dl person trong Csdl
            var model = await _context.Employees.ToListAsync();//dùng 
            return View(model);
        }

    public IActionResult Create(){
        //trả về View thêm 1 person mới vào csdl
        return View();
        }
   
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("EmployeeID, Age ,PersonId, FullName,Address")] Employee employee)
    {
        if ( ModelState.IsValid ){
            _context.Add(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(employee);
    }
    //Employee /Edit
    public async Task<IActionResult> Edit(string id)
    {
        if ( id==null || _context.Employees==null)
        {
            return NotFound();

            }
            var employee = await _context.Employees.FindAsync(id);
            if (employee==null){
                return NotFound();
                }
            return View(employee);
            }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind(" EmployeeID, Age ,PersonId, FullName, Address")] Employee employee)
        { 
            if( id != employee.PersonId)
            {
                return NotFound();
            }
            if ( ModelState.IsValid)
            {
                try {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                    }

                    catch(DbUpdateConcurrencyException)
                    {
                        if ( !PersonExists(employee.PersonId))
                        {
                            return NotFound();

                        }else{
                            throw;
                }
                }
                return RedirectToAction(nameof(Index));

        }
        return View(employee);
            }

    public async Task<IActionResult> Delete(string id )
    {
        if ( id==null|| _context.Employees ==null)
        {
            return NotFound();
        }
        var employee = await _context.Employees.FirstOrDefaultAsync(m => m.PersonId==id);
        if(employee==null){
            return NotFound();
            }
            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if(_context.Employees==null)
            {
                return Problem("Entity set ' applicationDbContext.Employee' is null.");
            }
            var employee= await _context.Employees.FindAsync(id) ;
            if(employee!=null){
                _context.Employees.Remove(employee);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index)); 
            }
            
        private bool PersonExists(string id)
        {
           return(_context.Employees?.Any(e=>e.PersonId== id)).GetValueOrDefault();
        }
    }
    
            
            



            

            
            
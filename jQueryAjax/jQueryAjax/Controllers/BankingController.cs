using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using jQueryAjax.Data;
using jQueryAjax.Models;

namespace jQueryAjax.Controllers
{
    public class BankingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BankingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Banking
        public async Task<IActionResult> Index()
        {
              return _context.Bankings != null ? 
                          View(await _context.Bankings.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Bankings'  is null.");
        }

        // GET: Banking/Create
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if(id == 0)
            {
                return View(new BankingModel());
            }
            else
            {
                var bankingModel = await _context.Bankings.FindAsync(id);
                if( bankingModel == null)
                {
                    return NotFound();
                }
                return View(bankingModel);
            }
            
         }

        // POST: Banking/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("BankId,Name,AccountNumber,BeneficiaryName,CreatedDate")] BankingModel bankingModel)
        {
            if (ModelState.IsValid)
            {
                if(id == 0)
                {
                    _context.Add(bankingModel);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    try
                    {
                        _context.Update(bankingModel);
                        await _context.SaveChangesAsync();
                    }
                     catch(DbUpdateConcurrencyException)
                    {
                        if (!BankingModelExists(bankingModel.BankId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }

                //return RedirectToAction(nameof(Index));
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Bankings.ToList()) });
            }
           // return View(bankingModel);
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", bankingModel) });
        }


        //POST: Banking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bankings == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Bankings'  is null.");
            }
            var bankingModel = await _context.Bankings.FindAsync(id);
            if (bankingModel != null)
            {
                _context.Bankings.Remove(bankingModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BankingModelExists(int id)
        {
          return (_context.Bankings?.Any(e => e.BankId == id)).GetValueOrDefault();
        }
    }
}

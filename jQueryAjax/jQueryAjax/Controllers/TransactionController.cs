using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using jQueryAjax.Data;
using jQueryAjax.Models;
using jQueryAjax.ViewModels;

namespace jQueryAjax.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TransactionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Transaction
     
        public async Task<IActionResult> Index()
        {

            return _context.Bankings != null ?
                       View(await _context.Transactions.ToListAsync()) :
                      Problem("Entity set 'TransactionDbContext.Transactions'  is null.");
        }

        // GET: Transaction/AddOrEdit
        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {

            TransactionViewModel model = new TransactionViewModel();

            var bankingData = _context.Bankings;
            foreach (var data in bankingData)
            {
                model.BankList.Add(new SelectListItem
                {
                    Value = data.BankId.ToString(),
                    Text = data.Name
                });
            }

            if (id == 0)
                return View(model);
            else
            {
                var transactionModel = await _context.Transactions.FindAsync(id);
                if (transactionModel != null)
                {
                    model.AccountNumber = transactionModel.AccountNumber;
                    model.Amount = transactionModel.Amount;
                    model.BankId = transactionModel.BankId;
                    //model.BankName = transactionModel.BankName;
                    model.BeneficiaryName = transactionModel.BeneficiaryName;
                    model.SwiftCode = transactionModel.SwiftCode;


                }
                return View(model);
            }

        }

        // POST: Transaction/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(TransactionViewModel model)
        {

            if (ModelState.IsValid)
            {
                TransactionModel tmodel = new TransactionModel();
                if (model.TransactionId == 0)
                {
                    tmodel.BankId = Convert.ToInt32(model.BankId);
                    tmodel.Date = DateTime.Now;
                    tmodel.AccountNumber = model.AccountNumber;
                    tmodel.Amount = model.Amount;
                    //tmodel.BankName = model.BankName;
                    tmodel.BankName = _context.Bankings.Find(model.BankId).Name;
                    tmodel.SwiftCode = model.SwiftCode;
                    tmodel.BeneficiaryName = model.BeneficiaryName;

                    _context.Add(tmodel);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    try
                    {
                        _context.Update(model);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TransactionModelExists(model.TransactionId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }

                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Transactions.ToList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", model) });
        }

    


    // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Transactions == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Transactions'  is null.");
            }
            var transactionModel = await _context.Transactions.FindAsync(id);
            if (transactionModel != null)
            {
                _context.Transactions.Remove(transactionModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionModelExists(int id)
        {
          return (_context.Transactions?.Any(e => e.TransactionId == id)).GetValueOrDefault();
        }
    }
}

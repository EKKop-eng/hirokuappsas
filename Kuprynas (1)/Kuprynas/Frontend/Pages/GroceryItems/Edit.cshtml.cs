using Kuprynas.Data;
using Kuprynas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Kuprynas.Pages.GroceryItems
{
    public class EditModel : PageModel
    {
        private readonly KruprynasDbContext _context;

        public EditModel(KruprynasDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public GroceryItem GroceryItem { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            GroceryItem = await _context.GroceryItems.FindAsync(id);

            if (GroceryItem == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var existingItem = await _context.GroceryItems.FindAsync(id);
            if (existingItem == null)
            {
                return NotFound();
            }

            // Explicitly prevent changes to the Id property
            GroceryItem.Id = existingItem.Id;

            // Set values for all properties except the Id
            _context.Entry(existingItem).CurrentValues.SetValues(GroceryItem);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.GroceryItems.Any(e => e.Id == GroceryItem.Id))
                {
                    return NotFound();
                }
                throw;
            }

            return RedirectToPage("Index");
        }

    }
}

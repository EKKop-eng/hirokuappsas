using Kuprynas.Data;
using Kuprynas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kuprynas.Pages.GroceryItems
{
    public class DeleteModel : PageModel
    {
        private readonly KruprynasDbContext _context;

        public DeleteModel(KruprynasDbContext context)
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
            var groceryItem = await _context.GroceryItems.FindAsync(id);

            if (groceryItem == null)
            {
                return NotFound();
            }

            _context.GroceryItems.Remove(groceryItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}

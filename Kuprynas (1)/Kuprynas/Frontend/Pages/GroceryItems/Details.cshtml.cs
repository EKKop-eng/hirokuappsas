using Kuprynas.Data;
using Kuprynas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kuprynas.Pages.GroceryItems
{
    public class DetailsModel : PageModel
    {
        private readonly KruprynasDbContext _context;

        public DetailsModel(KruprynasDbContext context)
        {
            _context = context;
        }

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
    }
}

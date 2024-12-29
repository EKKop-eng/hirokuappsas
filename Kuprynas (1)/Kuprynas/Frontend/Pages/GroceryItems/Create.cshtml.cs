using Kuprynas.Data;
using Kuprynas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kuprynas.Pages.GroceryItems
{
    public class CreateModel : PageModel
    {
        private readonly KruprynasDbContext _context;

        public CreateModel(KruprynasDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public GroceryItem GroceryItem { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.GroceryItems.Add(GroceryItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}

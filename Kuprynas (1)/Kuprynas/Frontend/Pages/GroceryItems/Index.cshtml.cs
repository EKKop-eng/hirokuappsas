using Kuprynas.Data;
using Kuprynas.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Kuprynas.Pages.GroceryItems
{
    public class IndexModel : PageModel
    {
        private readonly KruprynasDbContext _context;

        public IndexModel(KruprynasDbContext context)
        {
            _context = context;
        }

        public IList<GroceryItem> GroceryItems { get; set; }

        public async Task OnGetAsync()
        {
            GroceryItems = await _context.GroceryItems.ToListAsync();
        }
    }
}

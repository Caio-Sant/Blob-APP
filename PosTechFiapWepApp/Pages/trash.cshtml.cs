using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PosTechFiapWepApp.Models;
using PosTechFiapWepApp.Services;

#nullable disable

namespace PosTechFiapWepApp.Pages
{
    public class trashModel : PageModel
    {
        private readonly ILogger<trashModel> _logger;
        private readonly ImagensService _service;

        public List<ImagemResponseViewModel> ImagensDeletadas { get; set; }

        public trashModel(ILogger<trashModel> logger, ImagensService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            //ImagensDeletadas = await _service.PesquisarTodasImagensDeletadasAsync();

            return Page();
        }
    }
}


using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PosTechFiapWepApp.Models;
using PosTechFiapWepApp.Services;

#nullable disable

namespace PosTechFiapWepApp.Pages
{
    public class DeletadasModel : PageModel
    {
        private readonly ImagensService _service;

        public DeletadasModel(ImagensService service)
        {
            _service = service;
        }

        public List<ImagemResponseViewModel> Imagens { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Imagens = await _service.PesquisarImagensDeletadasAsync();

            return Page();
        }
    }
}
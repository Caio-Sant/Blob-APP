using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PosTechFiapWepApp.Models;
using PosTechFiapWepApp.Services;

#nullable disable

namespace PosTechFiapWepApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ImagensService _service;

        public List<ImagemResponseViewModel> Imagens { get; set; }

        public string Nome { get; set; }

        public IndexModel(ILogger<IndexModel> logger, ImagensService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Imagens = await _service.PesquisarTodasImagensAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string nome)
        {
            Imagens = await _service.PesquisarImagemPorNomeAsync(nome);

            return Page();
        }
    }
}
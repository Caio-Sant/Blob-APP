using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PosTechFiapWepApp.Services;

#nullable disable

namespace PosTechFiapWepApp.Pages
{
    public class RecuperaImagemModel : PageModel
    {
        private readonly ImagensService _service;

        public RecuperaImagemModel(ImagensService service)
        {
            _service = service;
        }

        public string Nome { get; set; }

        public void OnGet(string nome)
        {
            Nome = nome;
        }

        public async Task<IActionResult> OnPost(string nome)
        {
            await _service.RecuperarImagemPorNomeAsync(nome);

            return RedirectToPage("Index");
        }
    }
}
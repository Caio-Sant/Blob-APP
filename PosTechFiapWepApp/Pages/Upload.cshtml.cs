using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PosTechFiapWepApp.Services;

#nullable disable

namespace PosTechFiapWepApp.Pages
{
    public class UploadModel : PageModel
    {
        private readonly ImagensService _service;

        public UploadModel(ImagensService service)
        {
            _service = service;
        }

        public string Link { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync(IFormFile formFile)
        {
            if (formFile == null)
            {
                return Page();
            }

            MemoryStream ms = new();

            Stream stream = formFile.OpenReadStream();

            stream.CopyTo(ms);

            Models.ImagemResponseViewModel resposta = await _service.EfetuarUploadAsync(new Models.ImageRequestViewModel
            {
                Nome = formFile.FileName,
                DadosBytes = ms.ToArray()
            });

            if (resposta.ImagemUri == null)
            {
                return Page();
            }

            Link = resposta.ImagemUri.ToString();

            return Page();
        }
    }
}
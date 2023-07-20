using System;
using System.Net.Http;
using System.Net.Http.Headers;
using DemoSignalR.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace DemoSignalR.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SignalRDemo: ControllerBase
    {
        private readonly ISendMessage _sendMessage;
        private readonly IHttpClientFactory _httpClientFactory;

        public SignalRDemo(ISendMessage sendMessage, IHttpClientFactory httpClientFactory)
		{
            this._sendMessage = sendMessage;
            this._httpClientFactory = httpClientFactory;
		}

        [HttpGet(Name = "SendMessageToClient")]
        public async Task<IActionResult> SendMessageToClient()
        {
            var urlFile = "https://happypaystorage.file.core.windows.net/happypayfilestorage/Comercio/0202519369/contrato.pdf?sv=2022-11-02&ss=bfqt&srt=sco&sp=rwdlacupiytfx&se=2026-06-12T00:16:10Z&st=2023-06-15T16:16:10Z&spr=https&sig=Aoap%2FiyhnLaosNHTlMwVOvbYKc6Ea%2Btwv90ZmULRKoI%3D";

            var httpClient = _httpClientFactory.CreateClient();

            var response = await httpClient.GetAsync(urlFile);

            ContentDispositionHeaderValue contentDisposition = new("inline")
            {
                // Asignar el nombre del archivo (opcional)
                FileName = "certificado.pdf" // Reemplaza "nombre_del_archivo.pdf" con el nombre deseado
            };

            // Agregar el encabezado 'Content-Disposition' en el objeto Content de la respuesta
            Response.Headers["Content-Disposition"] = contentDisposition.ToString();

            if (response.IsSuccessStatusCode)
            {
                var pdfContent = await response.Content.ReadAsByteArrayAsync();

                return File(pdfContent, "application/pdf");
            }

            return NotFound();
        }
    }
}


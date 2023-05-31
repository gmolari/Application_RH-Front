using Application_RH.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForRHTest.Controllers
{
    public class VagaController : Controller
    {
        private readonly string ENDPOINT = "https://localhost:7245/api/Vaga";
        private readonly HttpClient httpClient = null;
        public VagaController()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ENDPOINT);
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                List<VagaModel> vagas = null;

                HttpResponseMessage response = await httpClient.GetAsync(httpClient.BaseAddress);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    vagas = JsonConvert.DeserializeObject<List<VagaModel>>(content);
                }
                else ModelState.AddModelError(null, "Erro ao processar a solicitação");

                return View(vagas);
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Criar(VagaModel vaga)
        {
            try
            {
                var vagaFormatado = JsonConvert.SerializeObject(vaga);

                var data = new StringContent(vagaFormatado, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(ENDPOINT, data);

                if (!response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError(null, "Erro ao processar a solicitação");
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return View();
            }
        }

        public async Task<IActionResult> Editar(int id)
        {
            try
            {
                VagaModel vaga = null;
                HttpResponseMessage response = await httpClient.GetAsync(ENDPOINT + "/" + id);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    vaga = JsonConvert.DeserializeObject<VagaModel>(content);
                }
                else ModelState.AddModelError(null, "Erro ao processar a solicitação");

                return View(vaga);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Editar(VagaModel vaga)
        {
            try
            {
                var vagaFormatado = JsonConvert.SerializeObject(vaga, Formatting.Indented);

                var data = new StringContent(vagaFormatado, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PutAsync(new Uri(ENDPOINT + "/atualizarVaga/" + vaga.Id), data);

                if (!response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError(null, "Erro ao processar a solicitação");
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return View();
            }
        }

        public async Task<IActionResult> Apagar(int id)
        {
            try
            {
                Console.WriteLine("teste" + id);
                HttpResponseMessage response = await httpClient.DeleteAsync(ENDPOINT + "/deletarVaga/" + id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return View();
            }
        }

        public async Task<IActionResult> ApagarConfirmacao(int id)
        {
            try
            {
                VagaModel vaga = null;
                HttpResponseMessage response = await httpClient.GetAsync(ENDPOINT + "/" + id);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    vaga = JsonConvert.DeserializeObject<VagaModel>(content);
                }
                else ModelState.AddModelError(null, "Erro ao processar a solicitação");

                return View(vaga);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return View();
            }
        }
    }
}

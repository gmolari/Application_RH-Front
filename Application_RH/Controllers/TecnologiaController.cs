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
    public class TecnologiaController : Controller
    {
        private readonly string ENDPOINT = "https://localhost:7245/api/Tecnologia";
        private readonly HttpClient httpClient = null;
        public TecnologiaController()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ENDPOINT);
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                List<TecnologiaModel> tecnologias = null;

                HttpResponseMessage response = await httpClient.GetAsync(httpClient.BaseAddress);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    tecnologias = JsonConvert.DeserializeObject<List<TecnologiaModel>>(content);
                }
                else ModelState.AddModelError(null, "Erro ao processar a solicitação");

                return View(tecnologias);
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }

        public async Task<IActionResult> TecnologiaVaga(int id)
        {
            try
            {
                List<VagaModel> vagas = null;
                TecnologiaViewModel fmtTecnologia = new TecnologiaViewModel();
                TecnologiaModel tecModel = null;

                HttpResponseMessage vagasResponse = await httpClient.GetAsync("https://localhost:7245/api/Vaga");
                HttpResponseMessage tecResponse = await httpClient.GetAsync(ENDPOINT + "/" + id);

                if (vagasResponse.IsSuccessStatusCode)
                {
                    string vagasContent = await vagasResponse.Content.ReadAsStringAsync();
                    string tecContent = await tecResponse.Content.ReadAsStringAsync(); 

                    vagas = JsonConvert.DeserializeObject<List<VagaModel>>(vagasContent);
                    tecModel = JsonConvert.DeserializeObject<TecnologiaModel>(tecContent);
                    Console.WriteLine(tecModel);
                    fmtTecnologia.Id = tecModel.Id;
                    fmtTecnologia.Nome = tecModel.Nome;
                    
                    foreach (VagaModel vaga in vagas)
                    {
                        fmtTecnologia.Vagas.Add(vaga);
                    }

                }
                else ModelState.AddModelError(null, "Erro ao processar a solicitação");
                
                return View(fmtTecnologia);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return View();
            }
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Criar(TecnologiaModel tecnologia)
        {
            try
            {
                var TecnologiaFormatado = JsonConvert.SerializeObject(tecnologia);
                TecnologiaViewModel cTec = null;
                var data = new StringContent(TecnologiaFormatado, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(ENDPOINT, data);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    cTec = JsonConvert.DeserializeObject<TecnologiaViewModel>(content);
                }
                else ModelState.AddModelError(null, "Erro ao processar a solicitação");
                return RedirectToAction("TecnologiaVaga", new { id = cTec.Id.ToString() });
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
                TecnologiaModel tecnologia = null;
                HttpResponseMessage response = await httpClient.GetAsync(ENDPOINT + "/" + id);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    tecnologia = JsonConvert.DeserializeObject<TecnologiaModel>(content);
                }
                else ModelState.AddModelError(null, "Erro ao processar a solicitação");

                return View(tecnologia);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Editar(TecnologiaModel tecnologia)
        {
            try
            {
                var tecnologiaFormatado = JsonConvert.SerializeObject(tecnologia, Formatting.Indented);

                var data = new StringContent(tecnologiaFormatado, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PutAsync(new Uri(ENDPOINT + "/atualizarTecnologia/" + tecnologia.Id), data);

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

        public async Task<IActionResult> AtualizarPeso(string[] pesoTecs)
        {
            try
            {
                foreach(string s in pesoTecs)
                {
                    TecnologiaVagaModel newTecVaga = new TecnologiaVagaModel();
                    string[] formatedString = s.Split(" ");
                    newTecVaga.IdVaga = int.Parse(formatedString[0]);
                    newTecVaga.IdTec = int.Parse(formatedString[1]);
                    newTecVaga.peso = int.Parse(formatedString[2]);
                    var pesoFormatado = JsonConvert.SerializeObject(newTecVaga, Formatting.Indented);
                    var data = new StringContent(pesoFormatado, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await httpClient.PostAsync(new Uri("https://localhost:7245/api/TecnologiaVaga"), data);

                    if (!response.IsSuccessStatusCode)
                    {
                        ModelState.AddModelError(null, "Erro ao processar a solicitação");
                    }
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
                List<TecnologiaVagaModel> tecnologias = null;
                HttpResponseMessage response = await httpClient.DeleteAsync(ENDPOINT + "/deletarTecnologia/" + id);
                HttpResponseMessage response2 = await httpClient.DeleteAsync("https://localhost:7245/api/TecnologiaVaga/deletarTecnologiaVaga/" + id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> ApagarConfirmacao(int id)
        {
            try
            {
                TecnologiaModel tecnologia = null;
                HttpResponseMessage response = await httpClient.GetAsync(ENDPOINT + "/" + id);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    tecnologia = JsonConvert.DeserializeObject<TecnologiaModel>(content);
                }
                else ModelState.AddModelError(null, "Erro ao processar a solicitação");

                return View(tecnologia);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return View();
            }
        }
    }
}

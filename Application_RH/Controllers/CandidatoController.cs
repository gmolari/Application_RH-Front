using Application_RH.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace AppForRHTest.Controllers
{
    public class CandidatoController : Controller
    {
        private readonly string ENDPOINT = "https://localhost:7245/api/Candidato";
        private readonly HttpClient httpClient = null;
        public CandidatoController()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ENDPOINT); 
        }

        public async Task<IActionResult> Visualizar()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                List<CandidatoModel> candidatos = null;

                HttpResponseMessage response = await httpClient.GetAsync(httpClient.BaseAddress);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    candidatos = JsonConvert.DeserializeObject<List<CandidatoModel>>(content);
                    
                    foreach (CandidatoModel candidato in candidatos)
                    {
                        string newTecnologias = "";
                        string[] arrayTecs = candidato.Tecnologias.Split(",");
                        bool primeiraRepeticao = true;
                        foreach (string t in arrayTecs)
                        {
                            string[] strings = t.Trim().Split(" ");
                            if (primeiraRepeticao) {
                                newTecnologias += strings[0];
                                primeiraRepeticao = false;
                                continue;
                            }
                            newTecnologias += ", " + strings[0];
                        }
                        candidato.Tecnologias = newTecnologias;
                    }
                }
                else ModelState.AddModelError(null, "Erro ao processar a solicitação");

                return View(candidatos);
            }
            catch (Exception ex) 
            {
                return View(ex);
            }
        }

        public async Task<IActionResult> Criar()
        {
            try
            {
                List<TecnologiaModel> tecnologias = null;
                List<VagaModel> vagas = null;
                CandidatoViewModel fmtCandidato = new CandidatoViewModel();

                HttpResponseMessage tecResponse = await httpClient.GetAsync("https://localhost:7245/api/Tecnologia");
                HttpResponseMessage vagasResponse = await httpClient.GetAsync("https://localhost:7245/api/Vaga");

                if (tecResponse.IsSuccessStatusCode && vagasResponse.IsSuccessStatusCode)
                {
                    string tecContent = await tecResponse.Content.ReadAsStringAsync();
                    string vagasContent = await vagasResponse.Content.ReadAsStringAsync();

                    tecnologias = JsonConvert.DeserializeObject<List<TecnologiaModel>>(tecContent);
                    vagas = JsonConvert.DeserializeObject<List<VagaModel>>(vagasContent);

                    foreach (VagaModel vaga in vagas)
                    {
                        fmtCandidato.Vagas.Add(vaga);
                    }

                    foreach (TecnologiaModel tecnologia in tecnologias){
                        fmtCandidato.Tecnologias.Add(tecnologia);
                    }
                    
                }
                else ModelState.AddModelError(null, "Erro ao processar a solicitação");

                fmtCandidato.StringTecnologias = new List<string>();
                return View(fmtCandidato);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Criar(CandidatoViewModel candidato, string[] StringTecnologias)
        {
            try
            {
                string allTecnologias = StringTecnologias[0];
                foreach(string tecnologia in StringTecnologias)
                {
                    if (tecnologia != StringTecnologias[0]) allTecnologias += ", " + tecnologia;
                }
                string[] cVaga = candidato.Vaga.Split("|");
                CandidatoModel newCandidato = new CandidatoModel();

                newCandidato.Nome = candidato.Nome;
                newCandidato.Contato = candidato.Contato;
                newCandidato.NomeVaga = cVaga[0];
                newCandidato.IdVaga = int.Parse(cVaga[1]);
                newCandidato.Tecnologias = allTecnologias;
                
                var CandidatoFormatado = JsonConvert.SerializeObject(newCandidato);

                var data = new StringContent(CandidatoFormatado, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(ENDPOINT, data);
                Console.WriteLine(data);
                if (!response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError(null, "Erro ao processar a solicitação");
                }
                
                return RedirectToAction("TecnologiaVaga");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return View();
            }
        }

        public async Task<IActionResult> TecnologiaVaga(int id)
        {
            return View();
        }


        public async Task<IActionResult> Editar(int id)
        {
            try
            {
                CandidatoModel candidato = null;
                HttpResponseMessage response = await httpClient.GetAsync(ENDPOINT+"/"+id);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    candidato = JsonConvert.DeserializeObject<CandidatoModel>(content);
                }
                else ModelState.AddModelError(null, "Erro ao processar a solicitação");

                return View(candidato);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Editar(CandidatoModel candidato)
        {
            try
            {
                var CandidatoFormatado = JsonConvert.SerializeObject(candidato, Formatting.Indented);

                var data = new StringContent(CandidatoFormatado, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PutAsync(new Uri(ENDPOINT+"/atualizarCandidato/"+candidato.Id), data);

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
                Console.WriteLine("teste"+id);
                HttpResponseMessage response = await httpClient.DeleteAsync(ENDPOINT + "/deletarCandidato/" + id);
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
                CandidatoModel candidato = null;
                HttpResponseMessage response = await httpClient.GetAsync(ENDPOINT + "/" + id);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    candidato = JsonConvert.DeserializeObject<CandidatoModel>(content);
                }
                else ModelState.AddModelError(null, "Erro ao processar a solicitação");

                return View(candidato);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return View();
            }
        }
    }
}

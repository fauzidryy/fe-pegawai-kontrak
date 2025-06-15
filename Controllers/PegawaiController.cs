using FE_PegawaiKontrak.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace FE_PegawaiKontrak.Controllers
{
    public class PegawaiController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl = "https://localhost:7014/api/Pegawai";
        private readonly string _cabangApi = "https://localhost:7014/api/Cabang";
        private readonly string _jabatanApi = "https://localhost:7014/api/Jabatan";
        private readonly string _import_excel = "https://localhost:7014/api/Pegawai/import-excel";

        public PegawaiController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            var pegawais = await _httpClient.GetFromJsonAsync<List<Pegawai>>(_apiUrl);
            return View(pegawais);
        }

        [HttpPost]
        public async Task<IActionResult> UploadExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                TempData["Error"] = "File tidak ditemukan atau kosong.";
                return RedirectToAction("Index");
            }

            try
            {
                using var content = new MultipartFormDataContent();
                using var fileStream = file.OpenReadStream();
                var fileContent = new StreamContent(fileStream);
                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
                content.Add(fileContent, "file", file.FileName);

                using var client = new HttpClient();
                var response = await client.PostAsync(_import_excel, content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Berhasil mengimpor data dari Excel!";
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrWhiteSpace(errorMessage))
                    {
                        TempData["Error"] = $"Gagal impor Excel: Terjadi kesalahan pada server (status {response.StatusCode}).";
                    }
                    else
                    {
                        TempData["Error"] = $"Gagal impor Excel: {errorMessage}";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Terjadi kesalahan: {ex.Message}";
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Cabangs = await _httpClient.GetFromJsonAsync<List<Cabang>>(_cabangApi);
            ViewBag.Jabatans = await _httpClient.GetFromJsonAsync<List<Jabatan>>(_jabatanApi);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Pegawai pegawai)
        {
            var response = await _httpClient.PostAsJsonAsync(_apiUrl, pegawai);

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Data pegawai berhasil disimpan!";
                return RedirectToAction("Index");
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            TempData["Error"] = $"{errorContent}";
            Console.WriteLine($"[ERROR API]: {errorContent}");

            ViewBag.Cabangs = await _httpClient.GetFromJsonAsync<List<Cabang>>(_cabangApi);
            ViewBag.Jabatans = await _httpClient.GetFromJsonAsync<List<Jabatan>>(_jabatanApi);
            return View(pegawai);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var pegawai = await _httpClient.GetFromJsonAsync<Pegawai>($"{_apiUrl}/{id}");
            ViewBag.Cabangs = await _httpClient.GetFromJsonAsync<List<Cabang>>(_cabangApi);
            ViewBag.Jabatans = await _httpClient.GetFromJsonAsync<List<Jabatan>>(_jabatanApi);
            return View(pegawai);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Pegawai pegawai)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_apiUrl}/{id}", pegawai);

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Data pegawai berhasil diperbarui!";
                return RedirectToAction("Index");
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            TempData["Error"] = $"{errorContent}";
            Console.WriteLine($"[ERROR API]: {errorContent}");

            ViewBag.Cabangs = await _httpClient.GetFromJsonAsync<List<Cabang>>(_cabangApi);
            ViewBag.Jabatans = await _httpClient.GetFromJsonAsync<List<Jabatan>>(_jabatanApi);
            return View(pegawai);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_apiUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }

            return StatusCode(500, "Gagal menghapus data pegawai.");
        }
    }
}

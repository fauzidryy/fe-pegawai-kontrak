using FE_PegawaiKontrak.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace FE_PegawaiKontrak.Controllers
{
    public class CabangController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl = "https://localhost:7014/api/Cabang";

        public CabangController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            var cabangs = await _httpClient.GetFromJsonAsync<List<Cabang>>(_apiUrl);
            return View(cabangs);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Cabang cabang)
        {
            var response = await _httpClient.PostAsJsonAsync(_apiUrl, cabang);

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Data berhasil disimpan!";
                return RedirectToAction("Index");
            }

            TempData["Error"] = "Gagal menyimpan data!";
            return View(cabang);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var cabang = await _httpClient.GetFromJsonAsync<Cabang>($"{_apiUrl}/{id}");
            return View(cabang);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Cabang cabang)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_apiUrl}/{id}", cabang);

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Data cabang berhasil diperbarui!";
                return RedirectToAction("Index");
            }

            ViewData["Error"] = "Gagal memperbarui cabang. Silakan periksa data.";
            return View(cabang);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_apiUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }

            return StatusCode(500, "Gagal menghapus data.");
        }

    }
}

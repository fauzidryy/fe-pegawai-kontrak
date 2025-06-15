using FE_PegawaiKontrak.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace FE_PegawaiKontrak.Controllers
{
    public class JabatanController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl = "https://localhost:7014/api/Jabatan";

        public JabatanController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            var jabatans = await _httpClient.GetFromJsonAsync<List<Jabatan>>(_apiUrl);
            return View(jabatans);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Jabatan jabatan)
        {
            var response = await _httpClient.PostAsJsonAsync(_apiUrl, jabatan);

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Data jabatan berhasil disimpan!";
                return RedirectToAction("Index");
            }

            TempData["Error"] = "Gagal menyimpan data jabatan!";
            return View(jabatan);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var jabatan = await _httpClient.GetFromJsonAsync<Jabatan>($"{_apiUrl}/{id}");
            return View(jabatan);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Jabatan jabatan)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_apiUrl}/{id}", jabatan);

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Data jabatan berhasil diperbarui!";
                return RedirectToAction("Index");
            }

            ViewData["Error"] = "Gagal memperbarui data jabatan.";
            return View(jabatan);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_apiUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }

            return StatusCode(500, "Gagal menghapus data jabatan.");
        }
    }
}

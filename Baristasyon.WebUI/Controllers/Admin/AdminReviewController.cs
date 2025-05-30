﻿using Baristasyon.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Baristasyon.WebUI.Controllers.Admin
{
    public class AdminReviewController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminReviewController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("api");
            var response = await client.GetAsync("review");

            if (!response.IsSuccessStatusCode)
                return View(new List<ResultReviewDto>());

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<ResultReviewDto>>(json);
            return View(data);
        }

       
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient("api");

            var response = await client.DeleteAsync($"review/{id}");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Yorum silinirken bir hata oluştu.";
            }

            return RedirectToAction("Index");
        }

    }
}

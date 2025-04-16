using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;
using System.Text.Json;

public class StoreLoginModel : PageModel
{
    private readonly IHttpClientFactory _clientFactory;
    public StoreLoginModel(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    [BindProperty]
    public string? Message { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        var form = Request.Form;

        var loginPayload = new
        {
            Email = form["Email"].ToString().Trim(),
            Password = form["Password"].ToString().Trim()
        };

        var client = _clientFactory.CreateClient("ApiClient");

        var response = await client.PostAsJsonAsync("auth/login/store", loginPayload);

        var raw = await response.Content.ReadAsStringAsync();
        Console.WriteLine("API RESPONSE: " + raw);

        if (response.IsSuccessStatusCode)
        {
            try
            {
                var loginResult = JsonSerializer.Deserialize<StoreLoginResponse>(raw, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                TempData["StoreId"] = loginResult?.StoreId;
                TempData["StoreName"] = loginResult?.StoreName;

                return RedirectToPage("/IDK");
            }
            catch (Exception ex)
            {
                Message = $"Success but parsing failed: {ex.Message}";
                return Page();
            }
        }

        Message = $"Login failed: {raw}";
        return Page();
    }

    public class StoreLoginResponse
    {
        public string Message { get; set; } = "";
        public int StoreId { get; set; }
        public string StoreName { get; set; } = "";
    }
}
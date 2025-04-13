using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;
using System.Text.Json;

public class LoginModel : PageModel
{
    private readonly IHttpClientFactory _clientFactory;
    public LoginModel(IHttpClientFactory clientFactory)
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
        var response = await client.PostAsJsonAsync("auth/login", loginPayload);

        var raw = await response.Content.ReadAsStringAsync(); // Always log this
        Console.WriteLine("API RESPONSE: " + raw);

        if (response.IsSuccessStatusCode)
        {
            try
            {
                var loginResult = JsonSerializer.Deserialize<LoginResponse>(raw, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                TempData["UserId"] = loginResult?.UserId;
                TempData["Username"] = loginResult?.Username;

                return RedirectToPage("/Canoes");
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

    public class LoginResponse
    {
        public string Message { get; set; } = "";
        public int UserId { get; set; }
        public string Username { get; set; } = "";
    }

}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        var rawResponse = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var result = JsonSerializer.Deserialize<LoginResponse>(rawResponse,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            HttpContext.Session.SetInt32("StoreID", result!.StoreId);
            HttpContext.Session.SetString("StoreName", result.StoreName);

            return RedirectToPage("/ManageListings");
        }

        Message = $"Login failed: {rawResponse}";
        return Page();
    }

    private class LoginResponse
    {
        public string Message { get; set; } = "";
        public int StoreId { get; set; }
        public string StoreName { get; set; } = "";
    }
}
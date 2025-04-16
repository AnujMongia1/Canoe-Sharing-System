using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class StoreRegisterModel : PageModel
{
    private readonly IHttpClientFactory _clientFactory;
    public StoreRegisterModel(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    [BindProperty]
    public string? Message { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        var form = Request.Form;

        var storePayload = new
        {
            StoreName = form["StoreName"].ToString().Trim(),
            Email = form["Email"].ToString().Trim(),
            Password = form["Password"].ToString().Trim(),
            Address = form["Address"].ToString().Trim(),
            PhoneNumber = form["PhoneNumber"].ToString().Trim(),
            Location = form["Location"].ToString().Trim()
        };

        var client = _clientFactory.CreateClient("ApiClient");
        var response = await client.PostAsJsonAsync("auth/register/store", storePayload);

        if (response.IsSuccessStatusCode)
        {
            Message = "Store registration successful!";
        }
        else
        {
            var errorDetails = await response.Content.ReadAsStringAsync();
            Message = $"Registration failed: {errorDetails}";
        }

        return Page();
    }
}
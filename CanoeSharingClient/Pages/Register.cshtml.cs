using System.Net.Http.Json;
using CanoeSharingClient.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class RegisterModel : PageModel
{
    private readonly IHttpClientFactory _clientFactory;
    public RegisterModel(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    [BindProperty]
    public string? Message { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        var form = Request.Form;

        var registerPayload = new UserRegisterDto
        {
            Username = form["Username"],
            Email = form["Email"],
            Password = form["Password"]
        };

        var client = _clientFactory.CreateClient("ApiClient");
        var response = await client.PostAsJsonAsync("auth/register", registerPayload);

        if (response.IsSuccessStatusCode)
        {
            Message = "Registration successful!";
        }
        else
        {
            var errorDetails = await response.Content.ReadAsStringAsync();
            Message = $"Registration failed: {errorDetails}";
        }

        return Page();
    }


}

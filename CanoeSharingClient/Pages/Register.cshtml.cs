using System.Net.Http.Json;
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
        var registerPayload = new
        {
            username = form["Username"],
            email = form["Email"],
            password = form["Password"]
        };

        var client = _clientFactory.CreateClient("ApiClient");
        var response = await client.PostAsJsonAsync("auth/register", registerPayload);

        Message = response.IsSuccessStatusCode ? "Registration successful!" : "Registration failed.";
        return Page();
    }
}

using System.Text.Json;

class Program
{
    static async Task Main(string[] args)
    {
        string url = "https://api.disneyapi.dev/character/423";

        using HttpClient client = new HttpClient();

        try
        {
            HttpResponseMessage response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();

            DisneyResponse? personagem =
                JsonSerializer.Deserialize<DisneyResponse>(
                    json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

            if (personagem?.Data != null)
            {
                Console.WriteLine("=== Personagem Disney ===");
                Console.WriteLine();
                Console.WriteLine($"Nome: {personagem.Data.Name}");
                Console.WriteLine($"Imagem: {personagem.Data.ImageUrl}");
            }
            else
            {
                Console.WriteLine("Não foi possível obter os dados do personagem.");
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Erro ao acessar a API: {ex.Message}");
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Erro ao interpretar os dados da API: {ex.Message}");
        }
    }
}

public class DisneyResponse
{
    public DisneyCharacter? Data { get; set; }
}

public class DisneyCharacter
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? ImageUrl { get; set; }

    public string? Url { get; set; }
}
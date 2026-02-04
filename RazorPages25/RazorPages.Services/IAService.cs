using System.Text;
using System.Text.Json;

public class IAService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public IAService(HttpClient httpClient, string apiKey)
    {
        _httpClient = httpClient;
        _apiKey = apiKey;
    }

    public async Task<string> PreguntarAsync(string mensaje)
    {
        // Llamada a la API local
        var response = await _httpClient.GetAsync("https://localhost:7244/api/pais");
        response.EnsureSuccessStatusCode();
        var apiData = await response.Content.ReadAsStringAsync();

        // Llamada a la API de OpenRouter
        var request = new HttpRequestMessage(HttpMethod.Post,
            "https://openrouter.ai/api/v1/chat/completions");

        request.Headers.Add("Authorization", $"Bearer {_apiKey}");
        request.Headers.Add("HTTP:Referer", "http://localhost");
        request.Headers.Add("X-Title", "MiApp");

        var body = new
        {
            model = "openai/gpt-oss-20b:free",
            messages = new[]
            {
                new
                {
                    role = "system", content = @"RESPONDE SIGUIENDO REGLAS RIGIDAS:
                                                   1) Responde UNICAMENTE en este formato:
                                                    Encabezado con el nombre del grupo, seguido de dos puntos
                                                    En la(s) línea(s) siguientes, lista con guiones cada elemento, uno por línea.
                                                   2) No incluyas tablas, JSON, llaves, ni texto explicativo adicional.
                                                   3) Ejemplo exacto de la salida deseada:
                                                    Paises Americanos:
                                                    -Brasil.
                                                    -Canadá.
                                                   4)Si no hay elementos que responder, responde 'No hay elementos que mostrar.'"
                },
                new { role = "user", content = $"Datos: {apiData}" },
                new { role = "user", content = $"Pregunta: {mensaje}" }
            }
        };


        request.Content = new StringContent(
            JsonSerializer.Serialize(body),
            Encoding.UTF8,
            "application/json");


        var apiResponse = await _httpClient.SendAsync(request);
        var json = await apiResponse.Content.ReadAsStringAsync();

        using var doc = JsonDocument.Parse(json);

        if (doc.RootElement.TryGetProperty("choices", out var choices))
        {
            var content = choices[0].GetProperty("message").GetProperty("content").GetString();
            return content;
        }

        // Manejo de error si la respuesta no es la esperada
        return "Error: No se pudo obtener una respuesta válida de la API.\n\n " + json;

    }
}

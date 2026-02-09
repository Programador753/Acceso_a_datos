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
        // Llamadas paralelas a las tres APIs
        var taskCalificaciones = _httpClient.GetAsync("https://localhost:7244/api/calificacion");
        var taskPaises = _httpClient.GetAsync("https://localhost:7244/api/Pais");
        var taskAlumnos = _httpClient.GetAsync("https://localhost:7244/api/Alumno");

        await Task.WhenAll(taskCalificaciones, taskPaises, taskAlumnos);

        // Verificar que todas las respuestas fueron exitosas
        taskCalificaciones.Result.EnsureSuccessStatusCode();
        taskPaises.Result.EnsureSuccessStatusCode();
        taskAlumnos.Result.EnsureSuccessStatusCode();

        // Leer el contenido de cada respuesta
        var calificacionesData = await taskCalificaciones.Result.Content.ReadAsStringAsync();
        var paisesData = await taskPaises.Result.Content.ReadAsStringAsync();
        var alumnosData = await taskAlumnos.Result.Content.ReadAsStringAsync();

        // Combinar todos los datos
        var apiData = $@"
        CALIFICACIONES: {calificacionesData}

        PAISES: {paisesData}

        ALUMNOS: {alumnosData}";

        var request = new HttpRequestMessage(HttpMethod.Post, "https://openrouter.ai/api/v1/chat/completions");

        request.Headers.Add("Authorization", $"Bearer {_apiKey}");
        request.Headers.Add("HTTP-Referer", "http://localhost/");
        request.Headers.Add("X-Title", "MiApp");

        var body = new
        {
            model = "openai/gpt-oss-20b",
            messages = new[]
            {
                new { role = "system", content = @"RESPONDE SIGUIENDO REGLAS RIGIDAS:
                        Devuelve EXLCUSIVAMENTE un JSON valido.
                        No escribas texto adicional.
                        No uses Markdown.
                        No uses explicaciones.
                        No uses comillas simples.
                        No escribas encabezados.
                        Si en una columna te sale un valor numérico conviertelo a string.
                        El JSON DEBE tener exactamente esta estructura;
                        {
                            'columns': ['Columna1', 'Columna2'],
                            'rows': [
                                ['valor1', 'valor2'],
                                ['valor1', 'valor2']
                            ]
                        }
                        Si no hay elementos, responde el encabezado seguido de '- (ninguno)." },
                new { role = "user", content = $"Datos disponibles: {apiData}\n\nPregunta: {mensaje}" }
            }
        };

        request.Content = new StringContent(
            JsonSerializer.Serialize(body),
            Encoding.UTF8,
            "application/json"
        );

        var apiResponse = await _httpClient.SendAsync(request);
        var json = await apiResponse.Content.ReadAsStringAsync();

        using var doc = JsonDocument.Parse(json);

        if (doc.RootElement.TryGetProperty("choices", out var choices) && choices.GetArrayLength() > 0)
        {
            var content = choices[0].GetProperty("message").GetProperty("content").GetString();
            return content ?? "Respuesta vacía de la IA.";
        }

        return $"Error de la IA: {json}";
    }
}
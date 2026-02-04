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
        // 1. DEFINIR LAS TAREAS (No se ejecutan con await aún)
        // Usamos GetStringAsync para obtener el cuerpo directamente
        var taskAlumnos = _httpClient.GetStringAsync("https://localhost:7244/api/alumno");
        var taskPaises = _httpClient.GetStringAsync("https://localhost:7244/api/pais");

        // 

        // 2. EJECUTAR EN PARALELO
        // Esperamos a que ambas terminen. Esto es más rápido que hacer una y luego la otra.
        await Task.WhenAll(taskAlumnos, taskPaises);

        // 3. COMBINAR LOS DATOS
        // Creamos un objeto anónimo para estructurar la información para la IA
        var datosCombinados = new
        {
            Alumnos = JsonDocument.Parse(taskAlumnos.Result).RootElement, // Parseamos para evitar doble stringify
            Paises = JsonDocument.Parse(taskPaises.Result).RootElement
        };

        string apiDataJson = JsonSerializer.Serialize(datosCombinados);

        // 4. PREPARAR LLAMADA A OPENROUTER
        var request = new HttpRequestMessage(HttpMethod.Post, "https://openrouter.ai/api/v1/chat/completions");

        request.Headers.Add("Authorization", $"Bearer {_apiKey}");
        // OpenRouter requiere estos headers para rankings (opcional pero recomendado)
        request.Headers.Add("HTTP-Referer", "https://localhost/");
        request.Headers.Add("X-Title", "MiApp");

        var body = new
        {
            model = "openai/gpt-oss-20b", // Asegúrate de que este modelo exista y esté disponible
            messages = new[]
            {
                new
                {
                    role = "system",
                    content = @"RESPONDE SIGUIENDO REGLAS RIGIDAS:
                                1) Formato: Encabezado con nombre del grupo seguido de dos puntos.
                                2) Lista con guiones, uno por línea.
                                3) Sin JSON, tablas o explicaciones extra.
                                4) Si está vacío: 'No hay elementos que mostrar.'"
                },
                new { role = "user", content = $"Contexto de datos (JSON): {apiDataJson}" },
                new { role = "user", content = $"Pregunta: {mensaje}" }
            }
        };

        request.Content = new StringContent(
            JsonSerializer.Serialize(body),
            Encoding.UTF8,
            "application/json");

        // 5. ENVIAR Y PROCESAR RESPUESTA
        var apiResponse = await _httpClient.SendAsync(request);
        var jsonResponse = await apiResponse.Content.ReadAsStringAsync();

        if (!apiResponse.IsSuccessStatusCode)
        {
            return $"Error {apiResponse.StatusCode}: {jsonResponse}";
        }

        using var doc = JsonDocument.Parse(jsonResponse);
        if (doc.RootElement.TryGetProperty("choices", out var choices) && choices.GetArrayLength() > 0)
        {
            return choices[0].GetProperty("message").GetProperty("content").GetString();
        }

        return "Error: La respuesta de la IA no tiene el formato esperado.";
    }
}
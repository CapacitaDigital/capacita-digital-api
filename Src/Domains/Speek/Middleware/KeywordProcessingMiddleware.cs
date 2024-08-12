using Microsoft.AspNetCore.Http;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace capacita_digital_api.Src.Domains.Speek.middleware.KeywordProcessingMiddleware
{
    public class KeywordProcessingMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly ConcurrentDictionary<string, string> _messageResponseCache = new();

        public KeywordProcessingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                await ProcessWebSocket(context, webSocket);
            }
            else
            {
                await _next(context);
            }
        }

        private static async Task ProcessWebSocket(HttpContext context, WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            while (!result.CloseStatus.HasValue)
            {
                var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                string responseMessage = ProcessMessage(message);

                if (!string.IsNullOrEmpty(responseMessage))
                {
                    var responseBuffer = Encoding.UTF8.GetBytes(responseMessage);
                    await webSocket.SendAsync(new ArraySegment<byte>(responseBuffer), result.MessageType, result.EndOfMessage, CancellationToken.None);
                }

                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }

            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }

        private static string ProcessMessage(string message)
        {
            if (_messageResponseCache.ContainsKey(message))
            {
                return "";
            }

            string responseMessage = "";

            if (message.IndexOf("oi", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                responseMessage = "Olá! Como posso ajudar?";
            }
            else if (message.IndexOf("fechar", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                responseMessage = "Encerrando a conversa.";
            }
            else if (message.IndexOf("iniciar", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                responseMessage = "Iniciando o processo.";
            }

            // Armazena a mensagem e sua resposta no cache
            if (!string.IsNullOrEmpty(responseMessage))
            {
                _messageResponseCache.TryAdd(message, responseMessage);
            }

            return responseMessage;
        }
    }
}

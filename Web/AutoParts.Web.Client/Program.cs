using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Threading.Tasks;

namespace AutoParts.Web.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            // Blazor WA currently has an issue related to server streaming. No messages are returned from the server until the call is complete.
            // Setting WasmHttpMessageHandler.StreamingEnabled to true (reflection required) allows server streaming to work - https://github.com/mono/mono/issues/18718
            var wasmHttpMessageHandlerType = System.Reflection.Assembly.Load("WebAssembly.Net.Http").GetType("WebAssembly.Net.Http.HttpClient.WasmHttpMessageHandler");
            var streamingProperty = wasmHttpMessageHandlerType.GetProperty("StreamingEnabled", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
            streamingProperty.SetValue(null, true, null);

            builder.Services.AddBlazoredLocalStorage();

            builder.Services.ConfigureServices();

            await builder
                .Build()
                .RunAsync();
        }
    }
}

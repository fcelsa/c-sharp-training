using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading;

namespace MultiPlatformWebViewApp
{
    public static class WindowManager
    {
        public static dynamic MainWindow;
        private static dynamic debugConsoleWindow;

        public static void ShowMain(string htmlContent)
        {
            Console.WriteLine($"[WindowManager] ShowMain - HTML length: {htmlContent?.Length ?? 0}");
            Console.WriteLine($"[WindowManager] HTML starts with: {(htmlContent?.Substring(0, Math.Min(50, htmlContent?.Length ?? 0)) ?? "null")}");
            
            MainWindow = CreatePhotinoWindow()
                .SetTitle("Multi-Platform WebView App")
                .SetSize(1024, 768)
                .SetResizable(true)
                .RegisterWebMessageReceivedHandler(new EventHandler<string>(OnWebMessageReceived))
                .LoadRawString(htmlContent);
            
            Console.WriteLine("[WindowManager] Window created and HTML loaded");
        }

        private static void OnWebMessageReceived(object sender, string message)
        {
            try
            {
                using var doc = JsonDocument.Parse(message);
                var root = doc.RootElement;

                if (!root.TryGetProperty("command", out var commandEl)) return;

                string command = commandEl.GetString() ?? string.Empty;
                string data = root.TryGetProperty("data", out var dataEl) ? (dataEl.ValueKind == JsonValueKind.String ? dataEl.GetString() ?? string.Empty : dataEl.ToString()) : string.Empty;

                switch (command)
                {
                    case "openWindow":
                        OpenNewWindow(data);
                        break;

                    case "openModal":
                        OpenModalWindow(data);
                        break;

                    case "showDebugConsole":
                        ShowDebugConsole();
                        break;

                    case "executeCommand":
                        ExecuteBackendCommand(data);
                        break;

                    case "log":
                        Console.WriteLine($"[WebView] {data}");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore nel processing del messaggio: {ex.Message}");
            }
        }

        public static void OpenNewWindow(string title)
        {
            string content = $"<html><body><h1>{title}</h1><p>Nuova finestra</p></body></html>";

            CreatePhotinoWindow()
                .SetTitle(title)
                .SetSize(800, 600)
                .LoadRawString(content);
        }

        public static void OpenModalWindow(string title)
        {
            string content = $@"
                <html>
                <head>
                    <style>
                        body {{ font-family: Arial; padding: 20px; }}
                        button {{ padding: 10px 20px; margin: 10px; }}
                    </style>
                </head>
                <body>
                    <h2>{title}</h2>
                    <p>Questa è una finestra modale</p>
                    <button onclick=""window.external.sendMessage('closeModal')"">Chiudi</button>
                </body>
                </html>";

            var modalWindow = CreatePhotinoWindow()
                .SetTitle("Modal - " + title)
                .SetSize(400, 300)
                .SetResizable(false)
                .RegisterWebMessageReceivedHandler(new EventHandler<string>((s, msg) =>
                {
                    if (msg.Contains("closeModal"))
                    {
                        ((dynamic)s).Close();
                    }
                }))
                .LoadRawString(content);
        }

        public static void ShowDebugConsole()
        {
            if (debugConsoleWindow != null)
            {
                return;
            }

            string consoleHtml = @"
                <html>
                <head>
                    <style>
                        body { 
                            margin: 0; 
                            padding: 10px; 
                            background: #1e1e1e; 
                            color: #d4d4d4;
                            font-family: 'Consolas', monospace;
                            font-size: 13px;
                        }
                        #console { 
                            white-space: pre-wrap; 
                            word-wrap: break-word;
                            padding: 10px;
                        }
                        .error { color: #f48771; }
                        .warning { color: #dcdcaa; }
                        .info { color: #4ec9b0; }
                        input {
                            width: 100%;
                            background: #2d2d30;
                            border: 1px solid #3e3e42;
                            color: #d4d4d4;
                            padding: 5px;
                            font-family: 'Consolas', monospace;
                        }
                    </style>
                </head>
                <body>
                    <div id='console'></div>
                    <input type='text' id='input' placeholder='Digita un comando...' />
                    <script>
                        const consoleDiv = document.getElementById('console');
                        const input = document.getElementById('input');
                        
                        window.receiveConsoleUpdate = (message) => {
                            consoleDiv.innerHTML += message + '\n';
                            consoleDiv.scrollTop = consoleDiv.scrollHeight;
                        };
                        
                        input.addEventListener('keypress', (e) => {
                            if (e.key === 'Enter') {
                                window.external.sendMessage('consoleInput:' + input.value);
                                input.value = '';
                            }
                        });
                        
                        setInterval(() => {
                            window.external.sendMessage('getConsoleUpdates');
                        }, 500);
                    </script>
                </body>
                </html>";

            debugConsoleWindow = CreatePhotinoWindow()
                .SetTitle("Debug Console")
                .SetSize(800, 500)
                .RegisterWebMessageReceivedHandler(new EventHandler<string>((s, msg) =>
                {
                    if (msg.StartsWith("consoleInput:"))
                    {
                        string cmd = msg.Substring("consoleInput:".Length);
                        Console.WriteLine($"> {cmd}");
                        DebugConsole.ProcessConsoleCommand(cmd);
                    }
                    else if (msg.Contains("getConsoleUpdates"))
                    {
                        DebugConsole.SendConsoleUpdates(debugConsoleWindow);
                    }
                }))
                .LoadRawString(consoleHtml);
        }

        public static void ExecuteBackendCommand(string command)
        {
            Console.WriteLine($"Esecuzione comando: {command}");
            string result = $"Risultato del comando '{command}': OK";
            try
            {
                MainWindow.SendWebMessage($"commandResult('{EscapeJsString(result)}')");
            }
            catch { }
        }

        private static string EscapeJsString(string s)
        {
            return s.Replace("\\", "\\\\").Replace("'", "\\'").Replace("\n", "\\n");
        }

        // Crea dinamicamente un'istanza di PhotinoWindow usando reflection
        private static dynamic CreatePhotinoWindow()
        {
            Type? t = Type.GetType("Photino.NET.PhotinoWindow, Photino.NET");

            if (t == null)
            {
                t = AppDomain.CurrentDomain.GetAssemblies()
                    .Select(a => a.GetType("Photino.NET.PhotinoWindow"))
                    .FirstOrDefault(x => x != null);
            }

            if (t == null)
            {
                try
                {
                    var asm = Assembly.Load("Photino.NET");
                    t = asm.GetType("Photino.NET.PhotinoWindow");
                }
                catch { }
            }

            if (t == null) throw new InvalidOperationException("Tipo Photino.NET.PhotinoWindow non trovato.");

            var ctors = t.GetConstructors();
            if (ctors.Length == 0) throw new InvalidOperationException("Nessun costruttore pubblico disponibile per PhotinoWindow.");

            foreach (var ctor in ctors)
            {
                var ps = ctor.GetParameters();
                object?[] args = ps.Select(p => p.ParameterType.IsValueType ? Activator.CreateInstance(p.ParameterType) : null).ToArray();
                try
                {
                    return ctor.Invoke(args)!;
                }
                catch { }
            }

            try
            {
                return System.Runtime.Serialization.FormatterServices.GetUninitializedObject(t);
            }
            catch
            {
                throw new InvalidOperationException("Impossibile creare istanza di PhotinoWindow con i costruttori disponibili.");
            }
        }
    }
}

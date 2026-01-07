using Photino.NET;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace MultiPlatformWebViewApp
{
    class Program
    {
        private static dynamic mainWindow;
        private static dynamic debugConsole;
        private static readonly List<string> consoleBuffer = new List<string>();
        private static readonly object consoleLock = new object();

        static void Main(string[] args)
        {
            DebugConsole.InitRedirect();

            // Carica l'HTML dalla cartella wwwroot e crea la finestra principale
            string htmlContent = GetEmbeddedResource("index.html");
            
            // Se non riesce a caricare il file, usa HTML di fallback
            if (string.IsNullOrEmpty(htmlContent))
            {
                Console.WriteLine("ERRORE: Impossibile caricare index.html da wwwroot");
                Console.WriteLine($"BaseDirectory: {AppContext.BaseDirectory}");
                htmlContent = GetFallbackHtml();
            }

            WindowManager.ShowMain(htmlContent);

            // Attende la chiusura della finestra principale
            WindowManager.MainWindow.WaitForClose();
        }

        // Apre una finestra modale (dialog)
        private static void OpenModalWindow(string title)
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
                    <button onclick='window.external.sendMessage(""closeModal"")'>Chiudi</button>
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
            // modalWindow is shown by the underlying Photino implementation
        }

        // Mostra la console di debug
        private static void ShowDebugConsole()
        {
            if (debugConsole != null)
            {
                // Se già esiste, portala in primo piano
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
                        
                        // Riceve aggiornamenti dalla console C#
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
                        
                        // Poll per aggiornamenti
                        setInterval(() => {
                            window.external.sendMessage('getConsoleUpdates');
                        }, 500);
                    </script>
                </body>
                </html>";

            debugConsole = CreatePhotinoWindow()
                .SetTitle("Debug Console")
                .SetSize(800, 500)
                .RegisterWebMessageReceivedHandler(new EventHandler<string>((s, msg) =>
                {
                    if (msg.StartsWith("consoleInput:"))
                    {
                        string cmd = msg.Substring("consoleInput:".Length);
                        Console.WriteLine($"> {cmd}");
                        ProcessConsoleCommand(cmd);
                    }
                    else if (msg.Contains("getConsoleUpdates"))
                    {
                        SendConsoleUpdates();
                    }
                }))
                .LoadRawString(consoleHtml);
        }

        // Invia gli aggiornamenti del buffer alla console debug
        private static void SendConsoleUpdates()
        {
            if (debugConsole == null) return;

            lock (consoleLock)
            {
                if (consoleBuffer.Count > 0)
                {
                    foreach (var line in consoleBuffer)
                    {
                        string escaped = line.Replace("\\", "\\\\")
                                           .Replace("'", "\\'")
                                           .Replace("\n", "\\n")
                                           .Replace("\r", "");
                        try
                        {
                            debugConsole.SendWebMessage($"receiveConsoleUpdate('{escaped}')");
                        }
                        catch
                        {
                            // If sending fails, assume the debug console was closed and drop the reference
                            debugConsole = null;
                            break;
                        }
                    }
                    consoleBuffer.Clear();
                }
            }
        }

        // Processa comandi dalla console debug
        private static void ProcessConsoleCommand(string command)
        {
            if (command.ToLower() == "clear")
            {
                debugConsole?.SendWebMessage("receiveConsoleUpdate('--- Console cleared ---')");
            }
            else if (command.ToLower() == "help")
            {
                Console.WriteLine("Comandi disponibili: clear, help, status");
            }
            else if (command.ToLower() == "status")
            {
                Console.WriteLine($"Applicazione attiva - Thread: {Thread.CurrentThread.ManagedThreadId}");
            }
            else
            {
                Console.WriteLine($"Comando sconosciuto: {command}");
            }
        }

        // Esegue comandi dal backend
        private static void ExecuteBackendCommand(string command)
        {
            Console.WriteLine($"Esecuzione comando: {command}");

            // Esempio di logica backend
            string result = $"Risultato del comando '{command}': OK";

            // Invia il risultato alla finestra principale
            mainWindow.SendWebMessage($"commandResult('{result}')");
        }

        // Redirige Console.WriteLine verso il buffer
        private static void RedirectConsoleOutput()
        {
            var writer = new ConsoleWriter(line =>
            {
                lock (consoleLock)
                {
                    consoleBuffer.Add($"[{DateTime.Now:HH:mm:ss}] {line}");
                }
            });

            Console.SetOut(writer);
            Console.SetError(writer);
        }

        // Crea dinamicamente un'istanza di PhotinoWindow usando reflection
        private static dynamic CreatePhotinoWindow()
        {
            // Prova a risolvere il tipo caricando il nome completo
            Type? t = Type.GetType("Photino.NET.PhotinoWindow, Photino.NET");

            if (t == null)
            {
                // Cerca fra le assembly già caricate
                t = AppDomain.CurrentDomain.GetAssemblies()
                    .Select(a => a.GetType("Photino.NET.PhotinoWindow"))
                    .FirstOrDefault(x => x != null);
            }

            if (t == null)
            {
                // Prova a caricare l'assembly dal pacchetto NuGet
                try
                {
                    var asm = Assembly.Load("Photino.NET");
                    t = asm.GetType("Photino.NET.PhotinoWindow");
                }
                catch { }
            }

            if (t == null) throw new InvalidOperationException("Tipo Photino.NET.PhotinoWindow non trovato.");

            // Se non esiste il costruttore senza parametri, proviamo a trovare un costruttore utilizzabile
            var ctors = t.GetConstructors();
            if (ctors.Length == 0) throw new InvalidOperationException("Nessun costruttore pubblico disponibile per PhotinoWindow.");

            // Prova ogni costruttore usando valori di default per i parametri
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

            // Ultima risorsa: prova a creare un'istanza non inizializzata (potrebbe non funzionare)
            try
            {
                return System.Runtime.Serialization.FormatterServices.GetUninitializedObject(t);
            }
            catch
            {
                throw new InvalidOperationException("Impossibile creare istanza di PhotinoWindow con i costruttori disponibili.");
            }
        }

        // Nota: l'HTML referenzia file statici in wwwroot (assets/*.css, app.js)

        private static string GetFallbackHtml()
        {
            return @"<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <title>Photino WebView - Test Fallback</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            min-height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
        }
        .test-container {
            background: white;
            padding: 40px;
            border-radius: 8px;
            box-shadow: 0 10px 40px rgba(0, 0, 0, 0.3);
            text-align: center;
            max-width: 600px;
        }
        h1 {
            color: #d9534f;
            margin: 0 0 15px 0;
            font-size: 24px;
        }
        p {
            color: #666;
            margin: 10px 0;
            font-size: 16px;
        }
        .info {
            background: #f5f5f5;
            padding: 15px;
            border-radius: 4px;
            text-align: left;
            margin: 20px 0;
            font-family: monospace;
            font-size: 12px;
            color: #333;
        }
        .test-button-group {
            display: flex;
            gap: 15px;
            justify-content: center;
            margin: 20px 0;
        }
        button {
            padding: 12px 32px;
            font-size: 14px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            transition: all 0.3s;
        }
        .test-ok {
            background: #52c41a;
            color: white;
        }
        .test-ok:hover { background: #3fa814; }
        .test-close {
            background: #f5222d;
            color: white;
        }
        .test-close:hover { background: #cf1322; }
    </style>
</head>
<body>
    <div class='test-container'>
        <h1>⚠️ HTML di Fallback</h1>
        <p>I file in wwwroot non sono stati trovati</p>
        <div class='info'>
            <strong>Possibili soluzioni:</strong><br>
            1. Verificare che la cartella wwwroot esista nel progetto<br>
            2. Assicurarsi che index.html sia copiato nell'output<br>
            3. Controllare il percorso della cartella in AppContext.BaseDirectory
        </div>
        <p style='color: #333; font-weight: bold;'>Se questo HTML appare, significa che la comunicazione C#↔JavaScript funziona!</p>
        <div class='test-button-group'>
            <button class='test-ok' onclick='handleOK()'>OK</button>
            <button class='test-close' onclick='handleClose()'>Chiudi</button>
        </div>
    </div>
    <script>
        function handleOK() {
            alert('Button OK cliccato - JavaScript funziona!');
        }
        function handleClose() {
            // Callback al C#
            window.external.sendMessage(JSON.stringify({command: 'log', data: 'Chiusura...'}));
        }
    </script>
</body>
</html>";
        }

        // Carica una risorsa embedded
        private static string GetEmbeddedResource(string filename)
        {
            // Prima prova a leggere dal file system (wwwroot copiato in output)
            try
            {
                string baseDir = AppContext.BaseDirectory;
                string filePath = Path.Combine(baseDir, "wwwroot", filename.Replace('/', Path.DirectorySeparatorChar));
                if (File.Exists(filePath))
                {
                    return File.ReadAllText(filePath);
                }
            }
            catch { }

            // Fallback: prova come risorsa embedded
            var assembly = Assembly.GetExecutingAssembly();
            // Cerca una risorsa il cui nome finisca con il percorso del file convertito in punti
            string lookup = filename.Replace('/', '.');
            string resourceName = assembly.GetManifestResourceNames().FirstOrDefault(n => n.EndsWith(lookup, StringComparison.OrdinalIgnoreCase));
            if (resourceName != null)
            {
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }

            return string.Empty;
        }

        // TextWriter personalizzato per catturare Console.WriteLine
        public class ConsoleWriter : TextWriter
        {
            private readonly Action<string> writeAction;
            private readonly StringBuilder buffer = new StringBuilder();

            public ConsoleWriter(Action<string> writeAction)
            {
                this.writeAction = writeAction;
            }

            public override Encoding Encoding => Encoding.UTF8;

            public override void Write(char value)
            {
                if (value == '\n')
                {
                    writeAction(buffer.ToString());
                    buffer.Clear();
                }
                else if (value != '\r')
                {
                    buffer.Append(value);
                }
            }

            public override void WriteLine(string? value)
            {
                writeAction(value ?? string.Empty);
            }
        }
    }
}
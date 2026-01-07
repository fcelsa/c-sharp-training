using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MultiPlatformWebViewApp
{
    public static class DebugConsole
    {
        private static readonly List<string> consoleBuffer = new List<string>();
        private static readonly object consoleLock = new object();

        public static void InitRedirect()
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

        public static void SendConsoleUpdates(dynamic debugWindow)
        {
            if (debugWindow == null) return;

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
                            debugWindow.SendWebMessage($"receiveConsoleUpdate('{escaped}')");
                        }
                        catch
                        {
                            // ignore send errors
                        }
                    }
                    consoleBuffer.Clear();
                }
            }
        }

        public static void ProcessConsoleCommand(string command)
        {
            if (command.ToLower() == "clear")
            {
                // no direct access to the window here; the window polls for updates
                lock (consoleLock)
                {
                    consoleBuffer.Add("--- Console cleared ---");
                }
            }
            else if (command.ToLower() == "help")
            {
                Console.WriteLine("Comandi disponibili: clear, help, status");
            }
            else if (command.ToLower() == "status")
            {
                Console.WriteLine($"Applicazione attiva - Thread: {System.Threading.Thread.CurrentThread.ManagedThreadId}");
            }
            else
            {
                Console.WriteLine($"Comando sconosciuto: {command}");
            }
        }

        // TextWriter per reindirizzare Console.WriteLine nel buffer
        private class ConsoleWriter : TextWriter
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

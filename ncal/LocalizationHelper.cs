using System;
using System.Collections.Generic;
using System.Globalization;

namespace ncal
{
    /// <summary>
    /// Gestisce tutta la localizzazione e internazionalizzazione per ncal
    /// </summary>
    public static class LocalizationHelper
    {
        private static CultureInfo _culture = CultureInfo.CurrentCulture;
        // Supported country code -> culture name mapping
        private static readonly Dictionary<string, string> _supportedCultures = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "it", "it-IT" },
            { "en", "en-US" },
            { "fr", "fr-FR" },
            { "es", "es-ES" },
            { "de", "de-DE" }
        };

        // Predefined per-language error message dictionaries to avoid recreating them on each call
        private static readonly Dictionary<string, string> _errorsIt = new Dictionary<string, string>
        {
            { "invalid_year", "L'anno deve essere compreso tra 1 e 9999" },
            { "invalid_month", "Il mese deve essere compreso tra 1 e 12" },
            { "missing_arg", "Argomento mancante per l'opzione" },
            { "unknown_option", "Opzione sconosciuta" },
            { "invalid_date_format", "Formato data non valido" },
            { "invalid_number", "Numero non valido" },
            { "conflicting_options", "Opzioni incompatibili" },
            { "invalid_country_code", "Country code non valido" }
        };

        private static readonly Dictionary<string, string> _errorsFr = new Dictionary<string, string>
        {
            { "invalid_year", "L'année doit être comprise entre 1 et 9999" },
            { "invalid_month", "Le mois doit être compris entre 1 et 12" },
            { "missing_arg", "Argument manquant pour l'option" },
            { "unknown_option", "Option inconnue" },
            { "invalid_date_format", "Format de date invalide" },
            { "invalid_number", "Nombre invalide" },
            { "conflicting_options", "Options incompatibles" },
            { "invalid_country_code", "Code de pays invalide" }
        };

        private static readonly Dictionary<string, string> _errorsEs = new Dictionary<string, string>
        {
            { "invalid_year", "El año debe estar entre 1 y 9999" },
            { "invalid_month", "El mes debe estar entre 1 y 12" },
            { "missing_arg", "Falta argumento para la opción" },
            { "unknown_option", "Opción desconocida" },
            { "invalid_date_format", "Formato de fecha inválido" },
            { "invalid_number", "Número inválido" },
            { "conflicting_options", "Opciones en conflicto" },
            { "invalid_country_code", "Código de país no válido" }
        };

        private static readonly Dictionary<string, string> _errorsDe = new Dictionary<string, string>
        {
            { "invalid_year", "Das Jahr muss zwischen 1 und 9999 liegen" },
            { "invalid_month", "Der Monat muss zwischen 1 und 12 liegen" },
            { "missing_arg", "Fehlendes Argument für Option" },
            { "unknown_option", "Unbekannte Option" },
            { "invalid_date_format", "Ungültiges Datumsformat" },
            { "invalid_number", "Ungültige Zahl" },
            { "conflicting_options", "Konfliktierende Optionen" },
            { "invalid_country_code", "Ungültiger Ländercode" }
        };

        private static readonly Dictionary<string, string> _errorsEn = new Dictionary<string, string>
        {
            { "invalid_year", "Year must be between 1 and 9999" },
            { "invalid_month", "Month must be between 1 and 12" },
            { "missing_arg", "Missing argument for option" },
            { "unknown_option", "Unknown option" },
            { "invalid_date_format", "Invalid date format" },
            { "invalid_number", "Invalid number" },
            { "conflicting_options", "Conflicting options" },
            { "invalid_country_code", "Invalid country code" }
        };

        // Predefined usage message templates per language (use {0} to inject supported languages list)
        private static readonly string _usageIt =
            "Utilizzo: ncal [OPZIONI] [[mese] anno]\n" +
            "       ncal [OPZIONI] [anno]\n" +
            "Opzioni principali: -3  -C  -M  -p  -w  -y  -c  -A numero  -B numero\n" +
            "                 -d yyyy-mm  -H yyyy-mm-dd  -s country_code  -h | --help\n" +
            "Lingue supportate: {0}";

        private static readonly string _usageEn =
            "Usage: ncal [OPTIONS] [[month] year]\n" +
            "       ncal [OPTIONS] [year]\n" +
            "Main options: -3  -C  -M  -p  -w  -y  -c  -A number  -B number\n" +
            "              -d yyyy-mm  -H yyyy-mm-dd  -s country_code  -h | --help\n" +
            "Supported languages: {0}";

        private static readonly string _usageFr =
            "Utilisation: ncal [OPTIONS] [[mois] année]\n" +
            "       ncal [OPTIONS] [année]\n" +
            "Options principales: -3  -C  -M  -p  -w  -y  -c  -A nombre  -B nombre\n" +
            "                  -d aaaa-mm  -H aaaa-mm-jj  -s country_code  -h | --help\n" +
            "Langues prises en charge: {0}";

        private static readonly string _usageEs =
            "Uso: ncal [OPCIONES] [[mes] año]\n" +
            "       ncal [OPCIONES] [año]\n" +
            "Opciones principales: -3  -C  -M  -p  -w  -y  -c  -A numero  -B numero\n" +
            "                   -d aaaa-mm  -H aaaa-mm-dd  -s country_code  -h | --help\n" +
            "Idiomas soportados: {0}";

        private static readonly string _usageDe =
            "Verwendung: ncal [OPTIONEN] [[Monat] Jahr]\n" +
            "       ncal [OPTIONEN] [Jahr]\n" +
            "Hauptoptionen: -3  -C  -M  -p  -w  -y  -c  -A nummer  -B nummer\n" +
            "               -d jjjj-mm  -H jjjj-mm-tt  -s country_code  -h | --help\n" +
            "Unterstützte Sprachen: {0}";

        /// <summary>
        /// Imposta la cultura da usare per la localizzazione
        /// </summary>
        public static void SetCulture(CultureInfo culture)
        {
            _culture = culture ?? CultureInfo.CurrentCulture;
        }

        /// <summary>
        /// Ottiene la cultura corrente
        /// </summary>
        public static CultureInfo GetCulture() => _culture;

        /// <summary>
        /// Ottiene il nome completo del mese (es. "gennaio", "January")
        /// </summary>
        public static string GetMonthName(int month)
        {
            return _culture.DateTimeFormat.GetMonthName(month);
        }

        /// <summary>
        /// Ottiene il nome abbreviato del mese (es. "gen", "Jan")
        /// </summary>
        public static string GetMonthNameAbbreviated(int month)
        {
            return _culture.DateTimeFormat.GetAbbreviatedMonthName(month);
        }

        /// <summary>
        /// Ottiene i nomi dei giorni della settimana nel formato ncal
        /// ncal usa formato breve (2-3 caratteri) con domenica come primo giorno
        /// </summary>
        public static string[] GetDayNamesShort()
        {
            // ncal usa: Su Mo Tu We Th Fr Sa (in ordine Sunday=0, Monday=1, ..., Saturday=6)
            string[] dayNames = new string[7];
            for (int i = 0; i < 7; i++)
            {
                // i=0 is Sunday, i=1 is Monday, etc.
                dayNames[i] = _culture.DateTimeFormat.GetAbbreviatedDayName((DayOfWeek)i);
            }
            return dayNames;
        }

        /// <summary>
        /// Ottiene i nomi completi dei giorni della settimana
        /// </summary>
        public static string[] GetDayNamesFull()
        {
            string[] dayNames = new string[7];
            for (int i = 0; i < 7; i++)
            {
                dayNames[i] = _culture.DateTimeFormat.GetDayName((DayOfWeek)i);
            }
            return dayNames;
        }

        /// <summary>
        /// Ottiene i nomi dei giorni della settimana ordinati secondo il primo giorno della settimana della cultura
        /// </summary>
        public static string[] GetDayNamesShortLocalized()
        {
            DayOfWeek firstDay = GetFirstDayOfWeek();
            string[] allNames = GetDayNamesShort();
            string[] result = new string[7];

            for (int i = 0; i < 7; i++)
            {
                result[i] = allNames[((int)firstDay + i) % 7];
            }
            return result;
        }

        /// <summary>
        /// Converte un DayOfWeek a un indice della colonna in base al primo giorno della settimana
        /// </summary>
        public static int GetColumnIndex(DayOfWeek dayOfWeek)
        {
            DayOfWeek firstDay = GetFirstDayOfWeek();
            int dayValue = (int)dayOfWeek;
            int firstDayValue = (int)firstDay;
            
            return (dayValue - firstDayValue + 7) % 7;
        }

        /// <summary>
        /// Ottiene il primo giorno della settimana secondo la cultura corrente
        /// (ad es. lunedì in Europa, domenica negli USA)
        /// </summary>
        public static DayOfWeek GetFirstDayOfWeek()
        {
            return _culture.DateTimeFormat.FirstDayOfWeek;
        }

        /// <summary>
        /// Formatta una data secondo la cultura corrente
        /// </summary>
        public static string FormatDate(DateTime date, string format = "d")
        {
            return date.ToString(format, _culture);
        }

        /// <summary>
        /// Messaggio per "oggi"
        /// </summary>
        public static string TodayMarker => "*";

        /// <summary>
        /// Etichetta per la colonna della settimana ("W" per inglese, "S" per italiano)
        /// </summary>
        public static string WeekColumnLabel
        {
            get
            {
                return _culture.Name.StartsWith("it") ? "S " : "W ";
            }
        }

        /// <summary>
        /// Separatore di colonna nel calendario
        /// </summary>
        public static string ColumnSeparator => " ";

        /// <summary>
        /// Messaggio di utilizzo
        /// </summary>
        public static string GetUsageMessage()
        {
            var supportedList = string.Join(", ", GetSupportedCountryCodesDisplay());
            var lang = _culture.TwoLetterISOLanguageName;

            var template = lang switch
            {
                "it" => _usageIt,
                "fr" => _usageFr,
                "es" => _usageEs,
                "de" => _usageDe,
                _ => _usageEn
            };

            return string.Format(template, supportedList);
        }

        /// <summary>
        /// Returns the supported country codes (keys only)
        /// </summary>
        public static string[] GetSupportedCountryCodes()
        {
            var keys = new string[_supportedCultures.Count];
            _supportedCultures.Keys.CopyTo(keys, 0);
            return keys;
        }

        /// <summary>
        /// Returns display strings for supported country codes (e.g. "it -> Italian (Italy)")
        /// </summary>
        public static IEnumerable<string> GetSupportedCountryCodesDisplay()
        {
            foreach (var kv in _supportedCultures)
            {
                string cultureName;
                try
                {
                    cultureName = new CultureInfo(kv.Value).DisplayName;
                }
                catch
                {
                    cultureName = kv.Value;
                }
                yield return $"{kv.Key} -> {cultureName}";
            }
        }

        /// <summary>
        /// Try to set the current culture using a supported country code.
        /// Returns true if success, false if the code is unknown.
        /// </summary>
        public static bool TrySetCultureByCode(string code)
        {
            if (string.IsNullOrEmpty(code)) return false;
            if (!_supportedCultures.TryGetValue(code, out var cultureName)) return false;
            try
            {
                SetCulture(new CultureInfo(cultureName));
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Messaggi di errore
        /// </summary>
        public static string GetErrorMessage(string errorKey)
        {
            var lang = _culture.TwoLetterISOLanguageName;
            var dict = lang switch
            {
                "it" => _errorsIt,
                "fr" => _errorsFr,
                "es" => _errorsEs,
                "de" => _errorsDe,
                _ => _errorsEn
            };

            return dict.TryGetValue(errorKey, out var msg) ? msg : errorKey;
        }
    }
}

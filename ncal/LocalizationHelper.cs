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
            return _culture.Name.StartsWith("it")
                ? "Utilizzo: ncal [-3hjJpwy] [-A numero] [-B numero] [-s country_code] [[mese] anno]\n" +
                  "           ncal [-3hJeo] [-A numero] [-B numero] [anno]\n" +
                  "           ncal [-CN] [-H yyyy-mm-dd] [-d yyyy-mm]"
                : "Usage: ncal [-3hjJpwy] [-A number] [-B number] [-s country_code] [[month] year]\n" +
                  "       ncal [-3hJeo] [-A number] [-B number] [year]\n" +
                  "       ncal [-CN] [-H yyyy-mm-dd] [-d yyyy-mm]";
        }

        /// <summary>
        /// Messaggi di errore
        /// </summary>
        public static string GetErrorMessage(string errorKey)
        {
            var errors = _culture.Name.StartsWith("it")
                ? new Dictionary<string, string>
                {
                    { "invalid_year", "L'anno deve essere compreso tra 1 e 9999" },
                    { "invalid_month", "Il mese deve essere compreso tra 1 e 12" },
                    { "missing_arg", "Argomento mancante per l'opzione" },
                    { "unknown_option", "Opzione sconosciuta" },
                    { "invalid_date_format", "Formato data non valido" },
                    { "invalid_number", "Numero non valido" },
                    { "conflicting_options", "Opzioni incompatibili" }
                }
                : new Dictionary<string, string>
                {
                    { "invalid_year", "Year must be between 1 and 9999" },
                    { "invalid_month", "Month must be between 1 and 12" },
                    { "missing_arg", "Missing argument for option" },
                    { "unknown_option", "Unknown option" },
                    { "invalid_date_format", "Invalid date format" },
                    { "invalid_number", "Invalid number" },
                    { "conflicting_options", "Conflicting options" }
                };

            return errors.TryGetValue(errorKey, out var msg) ? msg : errorKey;
        }
    }
}

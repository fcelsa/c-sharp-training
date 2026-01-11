using System;
using System.Collections.Generic;
using System.Globalization;

namespace ncal
{
    /// <summary>
    /// ncal - displays a calendar in the terminal
    /// </summary>
    internal class Program
    {
        // Variabile globale per il primo giorno della settimana
        private static DayOfWeek _firstDayOfWeek = DayOfWeek.Sunday;

        // Classe per mantenere le opzioni parsate
        private class NcalOptions
        {
            public bool ThreeMonths { get; set; }        // -3: previous, current, next month
            public int MonthsAfter { get; set; }         // -A number: months after
            public int MonthsBefore { get; set; }        // -B number: months before
            public bool QuarterMode { get; set; }        // -C: switch to quarter mode
            public DateTime? CustomDate { get; set; }    // -d yyyy-mm: custom current date
            public DateTime? CustomHighlight { get; set; } // -H yyyy-mm-dd: custom highlight date
            public bool HelpRequested { get; set; }        // -h/--help requested
            public bool MondayFirst { get; set; }        // -M: Monday first (override locale first day)
            public bool PrintCountryCodes { get; set; }  // -p: print country codes
            public string? CountryCode { get; set; }     // -s country_code: switch date
            public bool InvalidCountryCode { get; set; }
            public string? InvalidCountryCodeValue { get; set; }
            public bool WeekNumbers { get; set; }        // -w: week numbers
            public bool YearDisplay { get; set; }        // -y: display year
            public bool Compact { get; set; }            // -c: compact year display
            public int? Month { get; set; }              // month (1-12) or null
            public int? Year { get; set; }               // year (1-9999) or null
        }

        static void Main(string[] args)
        {
            try
            {
                var options = ParseCommandLine(args);


                // If an invalid country code was detected during parsing, print a helpful message
                if (options.InvalidCountryCode)
                {
                    Console.Error.WriteLine($"{LocalizationHelper.GetErrorMessage("invalid_country_code")}: {options.InvalidCountryCodeValue}");
                    Console.Error.WriteLine(string.Join("\n", LocalizationHelper.GetSupportedCountryCodesDisplay()));
                    throw new ArgumentException("invalid_country_code");
                }

                if (options.HelpRequested)
                {
                    // if -s was provided, ensure culture is applied before printing usage
                    if (!string.IsNullOrEmpty(options.CountryCode))
                    {
                        LocalizationHelper.TrySetCultureByCode(options.CountryCode);
                    }
                    PrintUsage();
                    return;
                }

                // If -p is specified, print available country codes and exit
                if (options.PrintCountryCodes)
                {
                    foreach (var s in LocalizationHelper.GetSupportedCountryCodesDisplay())
                        Console.WriteLine(s);
                    return;
                }

                // If -s was used, set the requested culture before computing locale-dependent values
                if (!string.IsNullOrEmpty(options.CountryCode) && !options.InvalidCountryCode)
                {
                    LocalizationHelper.TrySetCultureByCode(options.CountryCode);
                }

                // Determina il primo giorno della settimana
                // Per default: usa il primo giorno della cultura (locale)
                // Con -M: forza lunedì SOLO se il locale usa domenica, altrimenti rimane come è
                DayOfWeek cultureFirstDay = LocalizationHelper.GetFirstDayOfWeek();
                
                if (options.MondayFirst && cultureFirstDay == DayOfWeek.Sunday)
                {
                    // -M forza lunedì solo se il locale usa domenica
                    _firstDayOfWeek = DayOfWeek.Monday;
                }
                else
                {
                    // Altrimenti usa il primo giorno della cultura
                    _firstDayOfWeek = cultureFirstDay;
                }

                // Determina la data "oggi" (per highlight)
                // -H (CustomHighlight) e -d (CustomDate) influiscono su "today"
                DateTime today = options.CustomHighlight ?? options.CustomDate ?? DateTime.Now;

                // Se -C (QuarterMode) è specificato, visualizziamo sempre il trimestre corrente
                if (options.QuarterMode)
                {
                    options.ThreeMonths = true;
                    // Calcola il primo mese del trimestre basandosi su 'today'
                    int quarterStart = ((today.Month - 1) / 3) * 3 + 1; // 1,4,7,10
                    int centerMonth = quarterStart + 1; // mese centrale del trimestre
                    options.Month = centerMonth;
                    options.Year = today.Year;
                }

                // Determina il mese/anno da visualizzare
                DateTime displayDate;
                if (options.Year.HasValue)
                {
                    int month = options.Month ?? today.Month;
                    displayDate = new DateTime(options.Year.Value, month, 1);
                }
                else
                {
                    // Se nessun anno specificato, usa oggi
                    displayDate = new DateTime(today.Year, today.Month, 1);
                }

                // Se l'opzione -y è specificata, visualizza l'anno intero
                if (options.YearDisplay)
                {
                    DisplayYear(displayDate.Year, today, options);
                }
                else if (options.ThreeMonths)
                {
                    // Visualizza 3 mesi (precedente, corrente, prossimo)
                    DisplayThreeMonths(displayDate, today, options);
                }
                else if (options.MonthsAfter > 0 || options.MonthsBefore > 0)
                {
                    // Visualizza mesi prima e dopo il mese corrente
                    DisplayMultipleMonths(displayDate, today, options);
                }
                else
                {
                    // Visualizza il calendario per il mese corrente
                    DisplayCalendar(displayDate, today, options);
                }
            }
            catch (Exception ex)
            {
                // If we've already printed a detailed message for invalid country code, don't duplicate it
                if (ex.Message == "invalid_country_code")
                {
                    Environment.Exit(1);
                }

                Console.Error.WriteLine($"ncal: {LocalizationHelper.GetErrorMessage(ex.Message)}");
                Environment.Exit(1);
            }
        }

        /// <summary>
        /// parsing della linea di comando
        /// </summary>
        private static NcalOptions? ParseCommandLine(string[] args)
        {
            var options = new NcalOptions();
            var positionalArgs = new List<string>();

            int i = 0;
            while (i < args.Length)
            {
                string arg = args[i];

                // Se non inizia con '-', è un argomento posizionale
                if (!arg.StartsWith("-"))
                {
                    positionalArgs.Add(arg);
                    i++;
                    continue;
                }

                // Argomenti singoli (flag)
                switch (arg)
                {
                    case "-3":
                        options.ThreeMonths = true;
                        i++;
                        break;
                    case "-C":
                        options.QuarterMode = true;
                        i++;
                        break;
                    case "-h":
                    case "--help":
                        // POSIX convention: -h or --help prints usage/help
                        options.HelpRequested = true;
                        i++;
                        break;
                    case "-M":
                        options.MondayFirst = true;
                        i++;
                        break;
                    case "-p":
                        options.PrintCountryCodes = true;
                        i++;
                        break;
                    case "-w":
                        options.WeekNumbers = true;
                        i++;
                        break;
                    case "-y":
                        options.YearDisplay = true;
                        i++;
                        break;
                    case "-c":
                        options.Compact = true;
                        i++;
                        break;

                    // Argomenti che richiedono un valore
                    case "-A":
                        if (i + 1 >= args.Length)
                            throw new ArgumentException("missing_arg");
                        if (!int.TryParse(args[i + 1], out int monthsAfter) || monthsAfter < 0)
                            throw new ArgumentException("invalid_number");
                        options.MonthsAfter = monthsAfter;
                        i += 2;
                        break;

                    case "-B":
                        if (i + 1 >= args.Length)
                            throw new ArgumentException("missing_arg");
                        if (!int.TryParse(args[i + 1], out int monthsBefore) || monthsBefore < 0)
                            throw new ArgumentException("invalid_number");
                        options.MonthsBefore = monthsBefore;
                        i += 2;
                        break;

                    case "-d":
                        if (i + 1 >= args.Length)
                            throw new ArgumentException("missing_arg");
                        if (!DateTime.TryParseExact(args[i + 1], "yyyy-MM", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime customDate))
                            throw new ArgumentException("invalid_date_format");
                        options.CustomDate = customDate;
                        i += 2;
                        break;

                    case "-H":
                        if (i + 1 >= args.Length)
                            throw new ArgumentException("missing_arg");
                        if (!DateTime.TryParseExact(args[i + 1], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime customHighlight))
                            throw new ArgumentException("invalid_date_format");
                        options.CustomHighlight = customHighlight;
                        i += 2;
                        break;

                    case "-s":
                        if (i + 1 >= args.Length)
                            throw new ArgumentException("missing_arg");
                        options.CountryCode = args[i + 1];
                        // Apply culture immediately so that errors and usage are localized
                            if (!LocalizationHelper.TrySetCultureByCode(options.CountryCode))
                            {
                                // mark invalid code; Main will handle printing and exiting
                                options.InvalidCountryCode = true;
                                options.InvalidCountryCodeValue = options.CountryCode;
                            }
                        i += 2;
                        break;

                    default:
                        throw new ArgumentException("unknown_option");
                }
            }

            // Parsing degli argomenti posizionali: [month] year
            // Se abbiamo 1 argomento: è l'anno
            // Se abbiamo 2 argomenti: mese e anno
            if (positionalArgs.Count > 2)
                throw new ArgumentException("conflicting_options");

            if (positionalArgs.Count == 1)
            {
                if (!int.TryParse(positionalArgs[0], out int year) || year < 1 || year > 9999)
                    throw new ArgumentException("invalid_year");
                options.Year = year;
                // If only a year is provided as positional argument, display the whole year
                // same behavior as the -y option
                options.YearDisplay = true;
            }
            else if (positionalArgs.Count == 2)
            {
                if (!int.TryParse(positionalArgs[0], out int month) || month < 1 || month > 12)
                    throw new ArgumentException("invalid_month");
                if (!int.TryParse(positionalArgs[1], out int year2) || year2 < 1 || year2 > 9999)
                    throw new ArgumentException("invalid_year");
                options.Month = month;
                options.Year = year2;
            }

            // Controllo conflitti sulle opzioni: -C non è consentito insieme a -A o -B
            if (options.QuarterMode && (options.MonthsAfter > 0 || options.MonthsBefore > 0))
            {
                throw new ArgumentException("conflicting_options");
            }

            return options;
        }

        /// <summary>
        /// Visualizza mesi multipli: MonthsBefore mesi prima, il mese corrente, e MonthsAfter mesi dopo
        /// </summary>
        private static void DisplayMultipleMonths(DateTime centerDate, DateTime today, NcalOptions options)
        {
            // Calcola il mese di inizio
            DateTime startDate = centerDate.AddMonths(-options.MonthsBefore);

            // Calcola il numero totale di mesi da visualizzare
            int totalMonths = options.MonthsBefore + 1 + options.MonthsAfter;

            // Organizza i mesi in righe di 3 mesi
            for (int row = 0; row < (totalMonths + 2) / 3; row++)
            {
                List<List<string>> rowMonths = new List<List<string>>();

                for (int col = 0; col < 3; col++)
                {
                    int monthIndex = row * 3 + col;
                    if (monthIndex < totalMonths)
                    {
                        DateTime monthDate = startDate.AddMonths(monthIndex);
                        rowMonths.Add(GenerateMonthFullLines(monthDate.Month, monthDate.Year, today, options));
                    }
                }

                // Stampa i mesi affiancati riga per riga
                if (rowMonths.Count > 0)
                {
                    int maxLines = rowMonths.Max(m => m.Count);
                    // Full month width: user requirement -> 28 without week column, 32 with week column
                    int widthMonth = options.WeekNumbers ? 32 : 28;
                    for (int lineIdx = 0; lineIdx < maxLines; lineIdx++)
                    {
                        var line = new List<string>();
                        for (int col = 0; col < rowMonths.Count; col++)
                        {
                            if (lineIdx < rowMonths[col].Count)
                            {
                                line.Add(rowMonths[col][lineIdx]);
                            }
                            else
                            {
                                line.Add("".PadRight(widthMonth));
                            }
                        }
                        Console.WriteLine(string.Join("   ", line));
                    }
                    Console.WriteLine();
                }
            }
        }

        /// <summary>
        /// Visualizza 3 mesi affiancati: precedente, corrente, prossimo
        /// </summary>
        private static void DisplayThreeMonths(DateTime centerDate, DateTime today, NcalOptions options)
        {
            // Calcola i 3 mesi
            DateTime prevMonth = centerDate.AddMonths(-1);
            DateTime nextMonth = centerDate.AddMonths(1);

            // Genera le linee per i tre mesi
            List<string> prevLines = GenerateMonthFullLines(prevMonth.Month, prevMonth.Year, today, options);
            List<string> currLines = GenerateMonthFullLines(centerDate.Month, centerDate.Year, today, options);
            List<string> nextLines = GenerateMonthFullLines(nextMonth.Month, nextMonth.Year, today, options);

            // Determina il numero massimo di righe
            int maxLines = Math.Max(Math.Max(prevLines.Count, currLines.Count), nextLines.Count);

            // Stampa i tre mesi affiancati
            // Usa la larghezza del "full month" richiesta: 28 senza week column, 32 con week column
            int widthMonth = options.WeekNumbers ? 32 : 28;
            for (int i = 0; i < maxLines; i++)
            {
                string prevLine = i < prevLines.Count ? prevLines[i] : "".PadRight(widthMonth);
                string currLine = i < currLines.Count ? currLines[i] : "".PadRight(widthMonth);
                string nextLine = i < nextLines.Count ? nextLines[i] : "".PadRight(widthMonth);

                // separatore fra blocchi mese: 3 spazi
                Console.WriteLine($"{prevLine}   {currLine}   {nextLine}");
            }
        }

        /// <summary>
        /// Genera le linee di testo per un mese in formato completo (per visualizzazione di 3 mesi)
        /// </summary>
        private static List<string> GenerateMonthFullLines(int month, int year, DateTime today, NcalOptions options)
        {
            var lines = new List<string>();
            
            // Intestazione mese (centrata)
            // Common part: month header centered in the block width
            string monthName = LocalizationHelper.GetMonthName(month);
            string header = $"{monthName} {year}";
            // Full month width requirement from user: 28 chars (no week), 32 chars (with week)
            int widthMonth = options.WeekNumbers ? 32 : 28;
            int paddingMonth = (widthMonth - header.Length) / 2;
            string centeredHeader = header.PadLeft(header.Length + paddingMonth).PadRight(widthMonth);
            lines.Add(centeredHeader);

            // Riga giorni della settimana
            // Common: build 3-char day cells (2-letter + trailing space)
            var headerParts = new List<string>();
            string[] dayNames = GetLocalizedDayNames();

            string weekLabelCell = string.Empty;
            if (options.WeekNumbers)
            {
                // Specific: week column label (2 chars)
                weekLabelCell = LocalizationHelper.WeekColumnLabel.PadRight(2);
            }

            // Build day cells (each 4 chars): up to 3-letter abbreviation + trailing space
            var dayCells = new List<string>();
            foreach (var dayName in dayNames)
            {
                string dayAbbr = dayName.Length >= 3 ? dayName.Substring(0, 3) : dayName;
                dayCells.Add(dayAbbr.PadRight(3) + " ");
            }

            // Assemble header: optional week label (2 chars) + 2 spaces + concatenated 4-char day cells
            string dayHeader = options.WeekNumbers ? weekLabelCell + "  " + string.Join("", dayCells) : string.Join("", dayCells);
            lines.Add(dayHeader.PadRight(widthMonth));
            
            // Ottiene il primo giorno del mese
            DateTime firstDay = new DateTime(year, month, 1);
            int firstDayColumnIndex = GetDayColumnIndex((int)firstDay.DayOfWeek);
            
            // Ottiene il numero di giorni nel mese
            int daysInMonth = DateTime.DaysInMonth(year, month);
            
            // Crea la griglia del calendario per questo mese
            List<string>[] columns = new List<string>[7];
            for (int i = 0; i < 7; i++)
            {
                columns[i] = new List<string>();
            }
            
            // Popola le colonne
            for (int day = 1; day <= daysInMonth; day++)
            {
                DateTime cellDate = new DateTime(year, month, day);
                int dayOfWeek = (int)cellDate.DayOfWeek;
                int columnIndex = GetDayColumnIndex(dayOfWeek);
                
                string dayStr;
                if (day == today.Day && month == today.Month && year == today.Year)
                {
                    // Highlight today: place marker in the 4th position
                    dayStr = day.ToString().PadLeft(3, ' ') + LocalizationHelper.TodayMarker;
                }
                else
                {
                    // Regular day: right-align in 3 chars, then trailing space -> total 4 chars
                    dayStr = day.ToString().PadLeft(3, ' ') + " ";
                }

                columns[columnIndex].Add(dayStr);
            }
            
            // Aggiunge spazi vuoti prima del primo giorno (una cella = 3 char)
            for (int i = 0; i < firstDayColumnIndex; i++)
            {
                // Empty day cell is 4 spaces now
                columns[i].Insert(0, "    ");
            }
            
            // Stampa le settimane (righe)
            int maxRows = columns.Max(c => c.Count);
            for (int weekRow = 0; weekRow < maxRows; weekRow++)
            {
                var weekParts = new List<string>();
                
                // Se -w è specificato, stampa il numero di settimana
                if (options.WeekNumbers)
                {
                    int weekNumber = -1;
                    for (int col = 0; col < 7; col++)
                    {
                        if (weekRow < columns[col].Count && !columns[col][weekRow].Trim().Equals(""))
                        {
                            if (int.TryParse(columns[col][weekRow].Trim().TrimEnd('*'), out int dayOfMonth))
                            {
                                DateTime cellDate = new DateTime(year, month, dayOfMonth);
                                weekNumber = ISO8601Helper.GetISOWeekNumber(cellDate);
                                break;
                            }
                        }
                    }

                    if (weekNumber > 0)
                    {
                        // Week number occupies 2 chars (no trailing space here)
                        weekParts.Add(weekNumber.ToString().PadLeft(2, '0'));
                    }
                    else
                    {
                        weekParts.Add("  ");
                    }

                    // Specific: two spaces between week column and days
                    weekParts.Add("  ");
                }

                // Common: append each day cell (already 3 chars)
                for (int col = 0; col < 7; col++)
                {
                    if (weekRow < columns[col].Count)
                    {
                        // columns store strings like "DD*" or "DD " (3 chars)
                        weekParts.Add(columns[col][weekRow]);
                    }
                    else
                    {
                        weekParts.Add("   ");
                    }
                }

                // Join without extra separator: elements include the two-space separator when week numbers active
                lines.Add(string.Join("", weekParts).PadRight(widthMonth));
            }
            
            return lines;
        }

        /// <summary>
        /// Visualizza un anno intero (12 mesi su 3 colonne x 4 righe)
        /// </summary>
        private static void DisplayYear(int year, DateTime today, NcalOptions options)
        {
            // Stampa intestazione con l'anno
            Console.WriteLine();
            string centered = year.ToString().PadLeft((80 + year.ToString().Length) / 2);
            Console.WriteLine(centered);
            Console.WriteLine();

            // Organizza i 12 mesi in 4 righe di 3 mesi ciascuno
            for (int row = 0; row < 4; row++)
            {
                // Per ogni riga, visualizza 3 mesi affiancati
                List<string>[] monthsLines = new List<string>[3];

                for (int col = 0; col < 3; col++)
                {
                    int month = row * 3 + col + 1;
                    if (options.Compact)
                    {
                        // compact: usa le linee compatte (vecchio comportamento)
                        monthsLines[col] = GenerateMonthLines(month, year, today, options);
                    }
                    else
                    {
                        // full: usa il formato "full month" (larghezza 28/32)
                        monthsLines[col] = GenerateMonthFullLines(month, year, today, options);
                    }
                }

                // Stampa i mesi affiancati riga per riga
                int maxLines = monthsLines.Max(m => m.Count);
                for (int lineIdx = 0; lineIdx < maxLines; lineIdx++)
                {
                    var line = new List<string>();

                    if (options.Compact)
                    {
                        // compact mini width: 21 (no week) o 24 (with week)
                        int miniWidth = options.WeekNumbers ? 24 : 21;
                        for (int col = 0; col < 3; col++)
                        {
                            if (lineIdx < monthsLines[col].Count)
                            {
                                line.Add(monthsLines[col][lineIdx]);
                            }
                            else
                            {
                                line.Add("".PadRight(miniWidth)); // Larghezza dei mesi compatti
                            }
                        }
                    }
                    else
                    {
                        // full width: 28 (no week) o 32 (with week)
                        int fullWidth = options.WeekNumbers ? 32 : 28;
                        for (int col = 0; col < 3; col++)
                        {
                            if (lineIdx < monthsLines[col].Count)
                            {
                                line.Add(monthsLines[col][lineIdx]);
                            }
                            else
                            {
                                line.Add("".PadRight(fullWidth));
                            }
                        }
                    }

                    // separatore fra blocchi mese: 3 spazi
                    Console.WriteLine(string.Join("   ", line));
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Genera le linee di testo per un mese in formato compatto (per visualizzazione annuale)
        /// </summary>
        private static List<string> GenerateMonthLines(int month, int year, DateTime today, NcalOptions options)
        {
            var lines = new List<string>();

            // Intestazione mese (solo nome, centrato - anno già visualizzato in cima)
            // NOTE: this function generates the "compact" month block used for -c (compact)
            // Common parts (header/day cell generation) are similar to full-month generator
            // Specific: compact block width is smaller (21/24) and does not include extra spacing between week column and days
            string monthName = LocalizationHelper.GetMonthNameAbbreviated(month);
            // Larghezza coerente per formato compatto: 3 char per colonna -> 7*3 = 21
            // se -w è attivo, si aggiunge la colonna settimana (3 char) -> 24
            int widthMonth = options.WeekNumbers ? 24 : 21;
            int padding = (widthMonth - monthName.Length) / 2;
            lines.Add(monthName.PadLeft(monthName.Length + padding).PadRight(widthMonth));

            // Riga giorni della settimana (due caratteri per abbreviazione breve, 3 caratteri per colonna)
            string[] dayNames = GetLocalizedDayNames();
            var dayHeaderParts = new List<string>();

            if (options.WeekNumbers)
            {
                dayHeaderParts.Add(LocalizationHelper.WeekColumnLabel.PadRight(2));
            }

            for (int i = 0; i < 7; i++)
            {
                // preserve up to 3 characters for the weekday header (e.g., "lun", "mar")
                string dayAbbr = dayNames[i].Length >= 3 ? dayNames[i][..2] : dayNames[i];
                // each day header occupies 3 chars
                dayHeaderParts.Add(dayAbbr.PadRight(3));
            }
            string dayHeader = string.Join("", dayHeaderParts);
            lines.Add(dayHeader.PadRight(widthMonth));

            // Ottiene il primo giorno del mese
            DateTime firstDay = new DateTime(year, month, 1);
            int firstDayColumnIndex = GetDayColumnIndex((int)firstDay.DayOfWeek);

            // Ottiene il numero di giorni nel mese
            int daysInMonth = DateTime.DaysInMonth(year, month);

            // Crea la griglia del calendario per questo mese
            List<string>[] columns = new List<string>[7];
            for (int i = 0; i < 7; i++)
            {
                columns[i] = new List<string>();
            }

            // Popola le colonne
            for (int day = 1; day <= daysInMonth; day++)
            {
                DateTime cellDate = new DateTime(year, month, day);
                int dayOfWeek = (int)cellDate.DayOfWeek;
                int columnIndex = GetDayColumnIndex(dayOfWeek);

                string dayStr;
                if (day == today.Day && month == today.Month && year == today.Year)
                {
                    dayStr = day.ToString().PadLeft(2, ' ') + LocalizationHelper.TodayMarker;
                }
                else
                {
                    dayStr = day.ToString().PadLeft(2, ' ') + " ";
                }

                columns[columnIndex].Add(dayStr);
            }

            // Aggiunge spazi vuoti prima del primo giorno (una cella = 3 char)
            for (int i = 0; i < firstDayColumnIndex; i++)
            {
                columns[i].Insert(0, "   ");
            }

            // Stampa le settimane (righe)
            int maxRows = columns.Max(c => c.Count);
            for (int weekRow = 0; weekRow < maxRows; weekRow++)
            {
                var weekParts = new List<string>();

                // Se -w è specificato, stampa il numero di settimana
                if (options.WeekNumbers)
                {
                    int weekNumber = -1;
                    for (int col = 0; col < 7; col++)
                    {
                        if (weekRow < columns[col].Count && !columns[col][weekRow].Trim().Equals(""))
                        {
                            if (int.TryParse(columns[col][weekRow].Trim().TrimEnd('*'), out int dayOfMonth))
                            {
                                DateTime cellDate = new DateTime(year, month, dayOfMonth);
                                weekNumber = ISO8601Helper.GetISOWeekNumber(cellDate);
                                break;
                            }
                        }
                    }

                    if (weekNumber > 0)
                    {
                        weekParts.Add(weekNumber.ToString().PadLeft(2, '0') + " ");
                    }
                    else
                    {
                        weekParts.Add("   ");
                    }
                }

                for (int col = 0; col < 7; col++)
                {
                    if (weekRow < columns[col].Count)
                    {
                        weekParts.Add(columns[col][weekRow]);
                    }
                    else
                    {
                        weekParts.Add("   ");
                    }
                }

                // Common: join 3-char cells directly; compact does not add extra two-space separator
                lines.Add(string.Join("", weekParts).PadRight(widthMonth));
            }

            return lines;
        }

        /// <summary>
        /// Visualizza il calendario nel formato ncal per il mese specificato
        /// </summary>
        private static void DisplayCalendar(DateTime displayDate, DateTime today, NcalOptions options)
        {
            int month = displayDate.Month;
            int year = displayDate.Year;
            // Reuse the full-month generator for single-month display to ensure consistent formatting
            // This centralizes the formatting (common) and keeps DisplayCalendar simple (specific: single-month output)
            var lines = GenerateMonthFullLines(month, year, today, options);
            foreach (var l in lines)
            {
                Console.WriteLine(l);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Stampa le opzioni parsate per debug/verifica
        /// </summary>

        /// <summary>
        /// Ottiene i nomi dei giorni della settimana in base al primo giorno della settimana impostato
        /// </summary>
        private static string[] GetLocalizedDayNames()
        {
            string[] allNames = LocalizationHelper.GetDayNamesShort();
            string[] result = new string[7];

            for (int i = 0; i < 7; i++)
            {
                result[i] = allNames[((int)_firstDayOfWeek + i) % 7];
            }
            return result;
        }

        /// <summary>
        /// Converte un DayOfWeek (0=Sunday) a un indice di colonna (0=primo giorno della settimana)
        /// </summary>
        private static int GetDayColumnIndex(int dayOfWeek)
        {
            return (dayOfWeek - (int)_firstDayOfWeek + 7) % 7;
        }

        /// <summary>
        /// Stampa il messaggio di utilizzo
        /// </summary>
        private static void PrintUsage()
        {
            Console.WriteLine(LocalizationHelper.GetUsageMessage());
        }
    }
}
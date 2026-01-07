using System;

namespace ncal
{
    /// <summary>
    /// Helper per il calcolo del numero di settimana secondo lo standard ISO 8601
    /// </summary>
    public static class ISO8601Helper
    {
        /// <summary>
        /// Calcola il numero di settimana ISO 8601 per una data specifica.
        /// 
        /// La settimana 01 è definita come:
        /// - La settimana che contiene il primo giovedì dell'anno
        /// - La settimana che contiene il 4 gennaio
        /// - La prima settimana che contiene quattro o più giorni del nuovo anno
        /// - La settimana che inizia con lunedì fra il 29 dicembre e il 4 gennaio
        /// 
        /// Se il 1º gennaio è lunedì-giovedì, è nella settimana 01.
        /// Se è venerdì-domenica, è nell'ultima settimana dell'anno precedente (52 o 53).
        /// </summary>
        public static int GetISOWeekNumber(DateTime date)
        {
            // ISO 8601 assume che la settimana inizi di lunedì (DayOfWeek.Monday = 1)
            DayOfWeek dayOfWeek = date.DayOfWeek;
            
            // Converti da formato .NET (Sunday=0, Monday=1..Saturday=6) a ISO (Monday=1, Tuesday=2...Sunday=7)
            int isoDayOfWeek = dayOfWeek == DayOfWeek.Sunday ? 7 : (int)dayOfWeek;
            
            // Ottieni il giovedì della settimana (ISO8601 usa il giovedì come riferimento)
            DateTime thursday = date.AddDays(4 - isoDayOfWeek);
            
            // Ottieni l'anno del giovedì (potrebbe essere diverso dall'anno di partenza)
            int year = thursday.Year;
            
            // Ottieni il primo giovedì dell'anno
            DateTime firstThursdayOfYear = new DateTime(year, 1, 4);
            while (firstThursdayOfYear.DayOfWeek != DayOfWeek.Thursday)
            {
                firstThursdayOfYear = firstThursdayOfYear.AddDays(-1);
            }
            
            // Ottieni il lunedì della settimana del primo giovedì (inizio della settimana 1)
            DateTime firstMondayOfWeek1 = firstThursdayOfYear.AddDays(-(int)firstThursdayOfYear.DayOfWeek + 1);
            
            // Calcola il numero di settimane tra il lunedì della settimana 1 e il lunedì della settimana di thursday
            DateTime thursdayMonday = thursday.AddDays(-(int)thursday.DayOfWeek + 1);
            int weekNumber = (thursdayMonday - firstMondayOfWeek1).Days / 7 + 1;
            
            return weekNumber;
        }

        /// <summary>
        /// Calcola l'anno ISO 8601 per una data specifica.
        /// Potrebbe essere diverso dall'anno civile (es. 31 dicembre potrebbe essere settimana 1 dell'anno prossimo)
        /// </summary>
        public static int GetISOYear(DateTime date)
        {
            DayOfWeek dayOfWeek = date.DayOfWeek;
            int isoDayOfWeek = dayOfWeek == DayOfWeek.Sunday ? 7 : (int)dayOfWeek;
            
            DateTime thursday = date.AddDays(4 - isoDayOfWeek);
            return thursday.Year;
        }

        /// <summary>
        /// Restituisce il lunedì della settimana ISO per una data specifica
        /// </summary>
        public static DateTime GetMondayOfISOWeek(DateTime date)
        {
            DayOfWeek dayOfWeek = date.DayOfWeek;
            int isoDayOfWeek = dayOfWeek == DayOfWeek.Sunday ? 7 : (int)dayOfWeek;
            
            return date.AddDays(1 - isoDayOfWeek);
        }

        /// <summary>
        /// Restituisce la domenica della settimana ISO per una data specifica
        /// </summary>
        public static DateTime GetSundayOfISOWeek(DateTime date)
        {
            DayOfWeek dayOfWeek = date.DayOfWeek;
            int isoDayOfWeek = dayOfWeek == DayOfWeek.Sunday ? 7 : (int)dayOfWeek;
            
            return date.AddDays(7 - isoDayOfWeek);
        }
    }
}

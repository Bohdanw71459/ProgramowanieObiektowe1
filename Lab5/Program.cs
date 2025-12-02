using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Xml.Serialization;

namespace FileExercises
{ 
    public class Student
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public List<int> Oceny { get; set; }

        public override string ToString()
        {
            string ocenyTxt = Oceny != null ? string.Join(", ", Oceny) : "-";
            return $"{Imie} {Nazwisko}, Oceny: [{ocenyTxt}]";
        }
    }

    internal class Program
    {
        // ścieżki do plików
        private const string TekstFilePath = "user_input.txt";
        private const string JsonFilePath = "students.json";
        private const string XmlFilePath = "students.xml";
        private const string IrisFilePath = "iris.csv";
        private const string IrisFilteredFilePath = "iris_filtered.csv";

        private static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Task2_WriteTextToFile();
            Task3_ReadTextFromFile();
            Task4_AppendTextToFile();
            Task6_SerializeStudentsToJson();
            Task7_DeserializeStudentsFromJson();
            Task8_SerializeStudentsToXml();
            Task9_DeserializeStudentsFromXml();
            Task10_ReadIrisCsv();
            Task11_IrisColumnMeans();
            Task12_IrisFilterAndSave();

     

            Console.WriteLine();
            Console.WriteLine("Koniec. Naciśnij dowolny klawisz...");
            Console.ReadKey();
        }

        // Zadanie 2 wczytanie tekstu z konsoli i zapis do pliku
        private static void Task2_WriteTextToFile()
        {
            Console.WriteLine("=== Zadanie 2 ===");
            Console.WriteLine("Podawaj linie tekstu. Pusta linia kończy.");

            var lines = new List<string>();

            while (true)
            {
                Console.Write("Tekst: ");
                string input = Console.ReadLine();

                
                if (string.IsNullOrWhiteSpace(input))
                {
                    break;
                }

                lines.Add(input);
            }

            if (lines.Count == 0)
            {
                Console.WriteLine("Brak danych, nie zapisuję.");
                Console.WriteLine();
                return;
            }

            try
            {
                // zapis wszystkich linii do pliku
                File.WriteAllLines(TekstFilePath, lines);
                Console.WriteLine($"Zapisano {lines.Count} linii do pliku: {TekstFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd zapisu: {ex.Message}");
            }

            Console.WriteLine();
        }

        // Zadanie 3 odczyt pliku linia po linii
        private static void Task3_ReadTextFromFile()
        {
            Console.WriteLine("=== Zadanie 3 ===");

            if (!File.Exists(TekstFilePath))
            {
                Console.WriteLine($"Brak pliku {TekstFilePath} (najpierw zadanie 2).");
                Console.WriteLine();
                return;
            }

            try
            {
                // odczyt wszystkich linii
                string[] lines = File.ReadAllLines(TekstFilePath);
                Console.WriteLine($"Zawartość pliku: {TekstFilePath}");

                // wypisanie każdej linii
                foreach (string line in lines)
                {
                    Console.WriteLine(line);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd odczytu: {ex.Message}");
            }

            Console.WriteLine();
        }

        // Zadanie 4 dopisywanie nowych linii na końcu pliku
        private static void Task4_AppendTextToFile()
        {
            Console.WriteLine("=== Zadanie 4 ===");
            Console.WriteLine("Podawaj linie do dopisania. Pusta linia kończy.");

            var lines = new List<string>();

            while (true)
            {
                Console.Write("Tekst: ");
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    break;
                }

                lines.Add(input);
            }

            if (lines.Count == 0)
            {
                Console.WriteLine("Brak danych do dopisania.");
                Console.WriteLine();
                return;
            }

            try
            {
                // dopisanie do istniejącego pliku
                File.AppendAllLines(TekstFilePath, lines);
                Console.WriteLine($"Dopisano {lines.Count} linii do pliku: {TekstFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd dopisywania: {ex.Message}");
            }

            Console.WriteLine();
        }

        // Zadanie 6 utworzenie listy studentów i zapis do JSON
        private static void Task6_SerializeStudentsToJson()
        {
            Console.WriteLine("=== Zadanie 6 (JSON) ===");

            
            var students = CreateSampleStudents();

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            try
            {
                // serializacja do JSON
                string json = JsonSerializer.Serialize(students, options);
                // zapis JSON do pliku
                File.WriteAllText(JsonFilePath, json);
                Console.WriteLine($"Zapisano {students.Count} studentów do pliku: {JsonFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd JSON: {ex.Message}");
            }

            Console.WriteLine();
        }

        // Zadanie 7 odczyt JSON i wypisanie studentów
        private static void Task7_DeserializeStudentsFromJson()
        {
            Console.WriteLine("=== Zadanie 7 (JSON) ===");

            if (!File.Exists(JsonFilePath))
            {
                Console.WriteLine($"Brak pliku {JsonFilePath} (najpierw zadanie 6).");
                Console.WriteLine();
                return;
            }

            try
            {
                // odczyt JSON z pliku
                string json = File.ReadAllText(JsonFilePath);
                // deserializacja do listy Student
                var students = JsonSerializer.Deserialize<List<Student>>(json);

                if (students == null)
                {
                    Console.WriteLine("Lista studentów jest null.");
                }
                else
                {
                    Console.WriteLine($"Odczytano {students.Count} studentów:");
                    // wypisanie studentów
                    foreach (var student in students)
                    {
                        Console.WriteLine(student.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd JSON: {ex.Message}");
            }

            Console.WriteLine();
        }

        // Zadanie 8 zapis listy studentów do XML
        private static void Task8_SerializeStudentsToXml()
        {
            Console.WriteLine("=== Zadanie 8 (XML) ===");

            
            var students = CreateSampleStudents();

            try
            {
                // serializer XML dla List<Student>
                var serializer = new XmlSerializer(typeof(List<Student>));
                // zapis do pliku
                using (FileStream fs = new FileStream(XmlFilePath, FileMode.Create))
                {
                    serializer.Serialize(fs, students);
                }

                Console.WriteLine($"Zapisano {students.Count} studentów do pliku: {XmlFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd XML: {ex.Message}");
            }

            Console.WriteLine();
        }

        // Zadanie 9 odczyt XML i wypisanie studentów
        private static void Task9_DeserializeStudentsFromXml()
        {
            Console.WriteLine("=== Zadanie 9 (XML) ===");

            if (!File.Exists(XmlFilePath))
            {
                Console.WriteLine($"Brak pliku {XmlFilePath} (najpierw zadanie 8).");
                Console.WriteLine();
                return;
            }

            try
            {
                // serializer XML
                var serializer = new XmlSerializer(typeof(List<Student>));
                // odczyt pliku
                using (FileStream fs = new FileStream(XmlFilePath, FileMode.Open))
                {
                    var students = serializer.Deserialize(fs) as List<Student>;
                    if (students == null)
                    {
                        Console.WriteLine("Lista studentów jest null.");
                    }
                    else
                    {
                        Console.WriteLine($"Odczytano {students.Count} studentów:");
                        foreach (var student in students)
                        {
                            Console.WriteLine(student.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd XML: {ex.Message}");
            }

            Console.WriteLine();
        }

        // pomocnicza funkcja przykładowi studenci 
        private static List<Student> CreateSampleStudents()
        {
            return new List<Student>
            {
                new Student
                {
                    Imie = "Jan",
                    Nazwisko = "Kowalski",
                    Oceny = new List<int> { 5, 4, 3 }
                },
                new Student
                {
                    Imie = "Anna",
                    Nazwisko = "Nowak",
                    Oceny = new List<int> { 5, 5, 4, 5 }
                },
                new Student
                {
                    Imie = "Piotr",
                    Nazwisko = "Zielinski",
                    Oceny = new List<int> { 3, 3, 4 }
                }
            };
        }

        // Zadanie 10 odczyt pliku iris.csv i wypisanie kilku wierszy
        private static void Task10_ReadIrisCsv()
        {
            Console.WriteLine("=== Zadanie 10 (CSV) ===");

            if (!File.Exists(IrisFilePath))
            {
                Console.WriteLine($"Brak pliku {IrisFilePath}.");
                Console.WriteLine();
                return;
            }

            try
            {
                // odczyt wszystkich linii
                string[] lines = File.ReadAllLines(IrisFilePath);

                Console.WriteLine($"Odczytano {lines.Length} wierszy z pliku.");
                Console.WriteLine("Pierwsze wiersze:");

                // wypisanie kilku pierwszych
                int maxToShow = Math.Min(10, lines.Length);
                for (int i = 0; i < maxToShow; i++)
                {
                    Console.WriteLine(lines[i]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd odczytu CSV: {ex.Message}");
            }

            Console.WriteLine();
        }

        // Zadanie 11 obliczenie średnich wartości numerycznych kolumn
        private static void Task11_IrisColumnMeans()
        {
            Console.WriteLine("=== Zadanie 11 (średnie kolumn) ===");

            if (!File.Exists(IrisFilePath))
            {
                Console.WriteLine($"Brak pliku {IrisFilePath}.");
                Console.WriteLine();
                return;
            }

            try
            {
                // odczyt wszystkich linii
                string[] lines = File.ReadAllLines(IrisFilePath);
                if (lines.Length <= 1)
                {
                    Console.WriteLine("Za mało danych w CSV.");
                    Console.WriteLine();
                    return;
                }

                // nagłówek
                string header = lines[0];
                string[] headerCols = header.Split(',');

                // założenie: ostatnia kolumna to klasa (nienumeryczna)
                int numNumericCols = headerCols.Length - 1;
                double[] sums = new double[numNumericCols];
                long count = 0;

                var culture = CultureInfo.InvariantCulture;

                // przejście po wierszach danych
                for (int i = 1; i < lines.Length; i++)
                {
                    string line = lines[i].Trim();
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    string[] cols = line.Split(',');
                    if (cols.Length != headerCols.Length)
                        continue;

                    bool rowOk = true;

                    // parsowanie kolumn numerycznych
                    for (int c = 0; c < numNumericCols; c++)
                    {
                        if (double.TryParse(cols[c], NumberStyles.Float, culture, out double value))
                        {
                            sums[c] += value;
                        }
                        else
                        {
                            rowOk = false;
                            break;
                        }
                    }

                    if (rowOk)
                    {
                        count++;
                    }
                }

                if (count == 0)
                {
                    Console.WriteLine("Brak poprawnych wierszy.");
                    Console.WriteLine();
                    return;
                }

                Console.WriteLine($"Liczba rekordów: {count}");
                // wypisanie średnich
                for (int c = 0; c < numNumericCols; c++)
                {
                    double mean = sums[c] / count;
                    Console.WriteLine($"Średnia \"{headerCols[c]}\": {mean.ToString("F3", CultureInfo.InvariantCulture)}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd obliczeń CSV: {ex.Message}");
            }

            Console.WriteLine();
        }

        // Zadanie 12 filtrowanie iris.csv i zapis iris_filtered.csv
        private static void Task12_IrisFilterAndSave()
        {
            Console.WriteLine("=== Zadanie 12 (filtrowanie) ===");

            if (!File.Exists(IrisFilePath))
            {
                Console.WriteLine($"Brak pliku {IrisFilePath}.");
                Console.WriteLine();
                return;
            }

            try
            {
                // odczyt wszystkich linii
                string[] lines = File.ReadAllLines(IrisFilePath);
                if (lines.Length <= 1)
                {
                    Console.WriteLine("Za mało danych w CSV.");
                    Console.WriteLine();
                    return;
                }

                // nagłówek
                string header = lines[0];
                string[] headerCols = header.Split(',');

                // szukanie kolumn po nazwach
                int idxSepalLength = FindColumnIndex(headerCols, "sepal length");
                int idxSepalWidth = FindColumnIndex(headerCols, "sepal width");
                int idxClass = FindColumnIndex(headerCols, "class");

                if (idxSepalLength == -1 || idxSepalWidth == -1 || idxClass == -1)
                {
                    Console.WriteLine("Nie znaleziono wymaganych kolumn.");
                    Console.WriteLine();
                    return;
                }

                var culture = CultureInfo.InvariantCulture;
                var filteredLines = new List<string>();

                // nagłówek nowego pliku
                string newHeader = $"{headerCols[idxSepalLength]},{headerCols[idxSepalWidth]},{headerCols[idxClass]}";
                filteredLines.Add(newHeader);

                int passed = 0;

                // filtrowanie wierszy
                for (int i = 1; i < lines.Length; i++)
                {
                    string line = lines[i].Trim();
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    string[] cols = line.Split(',');
                    if (cols.Length != headerCols.Length)
                        continue;

                    // sepal length < 5
                    if (!double.TryParse(cols[idxSepalLength], NumberStyles.Float, culture, out double sepalLengthValue))
                        continue;

                    if (sepalLengthValue < 5.0)
                    {
                        string newLine = $"{cols[idxSepalLength]},{cols[idxSepalWidth]},{cols[idxClass]}";
                        filteredLines.Add(newLine);
                        passed++;
                    }
                }

                // zapis przefiltrowanych danych
                File.WriteAllLines(IrisFilteredFilePath, filteredLines);
                Console.WriteLine($"Zapisano {passed} rekordów do pliku: {IrisFilteredFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd filtrowania CSV: {ex.Message}");
            }

            Console.WriteLine();
        }

        // pomocnicza funkcja – indeks kolumny po nazwie (bez wielkości liter)
        private static int FindColumnIndex(string[] headerCols, string expectedName)
        {
            for (int i = 0; i < headerCols.Length; i++)
            {
                if (string.Equals(headerCols[i].Trim(), expectedName, StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }

            return -1;
        }


    }
}
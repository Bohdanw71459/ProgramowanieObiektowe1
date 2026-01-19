using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

public class Student
{
    public int StudentId;
    public string Imie = "";
    public string Nazwisko = "";
    public List<Ocena> Oceny = new();
}

public class Ocena
{
    public int OcenaId;
    public double Wartosc;
    public string Przedmiot = "";
    public int StudentId;
}

public class Program
{
    public static void Main()
    {
        string connectionString =
            "Data Source=10.200.2.28;" + //"(LocalDb)\\MSSQLLocalDB;" - dla lokalnej bazy
            "Initial Catalog=studenci_71459;" +
            "Integrated Security=True;" +
            "Encrypt=True;" +
            "TrustServerCertificate=True";

        try
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            Console.WriteLine("Połączono z bazą.\n");

            Zadanie4(connection);
            Zadanie5(connection);
            Zadanie6(connection);
            Zadanie7(connection);
            Zadanie8(connection);
            Zadanie9(connection);
            Zadanie10(connection);
        }
        catch (Exception e)
        {
            Console.WriteLine("Błąd: " + e.Message);
        }
    }

    // ZADANIA

    public static void Zadanie4(SqlConnection connection)
    {
        Console.WriteLine("ZADANIE 4");

        string sql = "SELECT StudentId, Imie, Nazwisko FROM Student";
        using SqlCommand cmd = new SqlCommand(sql, connection);
        using SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"{reader.GetInt32(0)} | {reader.GetString(1)} {reader.GetString(2)}");
        }
    }

    public static void Zadanie5(SqlConnection connection)
    {
        Console.WriteLine("\n ZADANIE 5 ");

        string sql = "SELECT Imie, Nazwisko FROM Student WHERE StudentId = @id";
        using SqlCommand cmd = new SqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("@id", 1);

        using SqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
            Console.WriteLine($"{reader.GetString(0)} {reader.GetString(1)}");
        else
            Console.WriteLine("Brak studenta.");
    }

    public static void Zadanie6(SqlConnection connection)
    {
        Console.WriteLine("\n ZADANIE 6 ");

        string sql = @"
        SELECT s.StudentId, s.Imie, s.Nazwisko,
               o.OcenaId, o.Wartosc, o.Przedmiot
        FROM Student s
        LEFT JOIN Ocena o ON s.StudentId = o.StudentId";

        using SqlCommand cmd = new SqlCommand(sql, connection);
        using SqlDataReader reader = cmd.ExecuteReader();

        List<Student> studenci = new();

        while (reader.Read())
        {
            int id = reader.GetInt32(0);
            Student s = studenci.FirstOrDefault(x => x.StudentId == id);

            if (s == null)
            {
                s = new Student
                {
                    StudentId = id,
                    Imie = reader.GetString(1),
                    Nazwisko = reader.GetString(2)
                };
                studenci.Add(s);
            }

            if (!reader.IsDBNull(3))
            {
                s.Oceny.Add(new Ocena
                {
                    OcenaId = reader.GetInt32(3),
                    Wartosc = reader.GetDouble(4),
                    Przedmiot = reader.GetString(5),
                    StudentId = id
                });
            }
        }

        foreach (var s in studenci)
        {
            Console.WriteLine($"\n{s.StudentId}: {s.Imie} {s.Nazwisko}");
            foreach (var o in s.Oceny)
                Console.WriteLine($"  {o.Przedmiot} – {o.Wartosc}");
        }
    }

    public static void Zadanie7(SqlConnection connection)
    {
        Console.WriteLine("\n ZADANIE 7 ");

        string sql = "INSERT INTO Student(Imie, Nazwisko) VALUES ('Adam', 'Nowak')";
        using SqlCommand cmd = new SqlCommand(sql, connection);
        cmd.ExecuteNonQuery();

        Console.WriteLine("Dodano studenta.");
    }

    public static void Zadanie8(SqlConnection connection)
    {
        Console.WriteLine("\n ZADANIE 8 ");

        double ocena = 4.5;
        if (!PoprawnaOcena(ocena))
        {
            Console.WriteLine("Niepoprawna ocena.");
            return;
        }

        string sql = "INSERT INTO Ocena(Wartosc, Przedmiot, StudentId) VALUES (4.5, 'matematyka', 1)";
        using SqlCommand cmd = new SqlCommand(sql, connection);
        cmd.ExecuteNonQuery();

        Console.WriteLine("Dodano ocenę.");
    }

    public static void Zadanie9(SqlConnection connection)
    {
        Console.WriteLine("\n ZADANIE 9 ");

        string sql = "DELETE FROM Ocena WHERE Przedmiot = 'geografia'";
        using SqlCommand cmd = new SqlCommand(sql, connection);
        cmd.ExecuteNonQuery();

        Console.WriteLine("Usunięto oceny z geografii.");
    }

    public static void Zadanie10(SqlConnection connection)
    {
        Console.WriteLine("\n ZADANIE 10 ");

        double nowa = 5.0;
        if (!PoprawnaOcena(nowa))
        {
            Console.WriteLine("Niepoprawna ocena.");
            return;
        }

        string sql = "UPDATE Ocena SET Wartosc = 5.0 WHERE OcenaId = 1";
        using SqlCommand cmd = new SqlCommand(sql, connection);
        cmd.ExecuteNonQuery();

        Console.WriteLine("Zaktualizowano ocenę.");
    }

    // WALIDACJA 

    public static bool PoprawnaOcena(double ocena)
    {
        return ocena >= 2 && ocena <= 5 &&
               (ocena * 10) % 5 == 0 &&
               ocena != 2.5;
    }
}

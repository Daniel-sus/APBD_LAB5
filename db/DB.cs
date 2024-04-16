using APBD05.Models;

namespace APBD05.DataBase;

public class StaticDb
{
    public static List<Animal> Animals { get; set; } = new List<Animal>();
    public static List<Visit> Visits { get; set; } = new List<Visit>();
}
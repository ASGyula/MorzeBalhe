using System.Text.Json;
// ReSharper disable InconsistentNaming

namespace MorzeBalhe;

public class Configuration{
    public char SpaceCharacter{get;set;}
    public char ShortCharacter{get;set;}
    public char LongCharacter{get;set;}
    public Dictionary<char, String> MorseCodeDictionary{get;set;}

    public static Configuration? loadFromFile(String path){
        if(!File.Exists(path)){
            Console.WriteLine("Nem található konfigurációs fájl. A kilépéshez nyomj le egy billentyűt.");
            Console.ReadKey();
            Environment.Exit(1);
            return null;
        }else{
            String json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<Configuration>(json);
        }
    }
}

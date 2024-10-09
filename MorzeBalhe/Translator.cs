using System.Text;
// ReSharper disable InconsistentNaming

namespace MorzeBalhe;

public class Translator(Configuration configuration){

    private readonly Dictionary<char, String> _morzeAbcDictionary = configuration.MorseCodeDictionary;
    private readonly char _spaceCharacter = configuration.SpaceCharacter;
    private readonly char _shortCharacter = configuration.ShortCharacter;
    private readonly char _longCharacter = configuration.LongCharacter;

    public String translateToMorse(String value, Boolean isSilent=false){
        StringBuilder result = new StringBuilder();
        Boolean isHasWrongCharacter = false;
        value = value.Replace(" ", "/");
        foreach(char VARIABLE in value.ToUpper()){
            if(_morzeAbcDictionary.TryGetValue(VARIABLE, out String morseChar)){
               String convertedLetter = "";
               
                foreach(char beep in morseChar){
                    char convertedChar = beep switch{
                        ' ' => _spaceCharacter,
                        '.' => _shortCharacter,
                        '-' => _longCharacter,
                        _ => ' '
                    };

                    convertedLetter += convertedChar;

                    if(isSilent) continue;
                    Console.Write(convertedChar);
                    if(beep == ' ')Thread.Sleep(500);
                    else if(beep == '.')Console.Beep(1000, 200);
                    else if(beep == '-')Console.Beep(1000, 500);
                }
                
                result.Append(convertedLetter);
                result.Append(' ');
                if(!isSilent)Console.Write(" ");
            }else{
                isHasWrongCharacter = true;
                textColorChangeToRed(VARIABLE + " ");
            }
        }
        
        if(isHasWrongCharacter){
            textColorChangeToRed("\nNem minden karaktert sikerült átfordítanunk.");
        }
    
        return result.ToString().TrimEnd();
    }

    public String translateFromMorse(String morseCode, Boolean isSilent=false){
        StringBuilder result = new StringBuilder();
        String currentMorse = "";
        Boolean isHasWrongCharacter = false;

        char? key;
        foreach(char character in morseCode){
            if(character == _spaceCharacter){
                result.Append(" ");
                if(isSilent)continue;
                Console.Write(" ");
                Thread.Sleep(500);
            }else if(character == _longCharacter){
                currentMorse += "-";
                if(!isSilent)Console.Beep(1000, 500);
            }else if(character == _shortCharacter){
                currentMorse += ".";
                if(!isSilent)Console.Beep(1000, 200);
            }else if(character == ' '){
                key = _morzeAbcDictionary.FirstOrDefault(x => x.Value == currentMorse).Key;
                if(key != null){
                    result.Append(key);
                    if(!isSilent)Console.Write(key);
                }
                currentMorse = "";
            }else{
                isHasWrongCharacter = true;
                textColorChangeToRed(character + "");
            }
        }
        
        key = _morzeAbcDictionary.FirstOrDefault(x => x.Value == currentMorse).Key;
        if(key != null){
            result.Append(key);
            if(!isSilent)Console.Write(key);
        }

        if(isHasWrongCharacter){
            textColorChangeToRed("\nNem minden karaktert sikerült dekódolnunk.");
        }
        
        return result.ToString();
    }
    
    private static void textColorChangeToRed(String value){
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write(value);
        Console.ForegroundColor = ConsoleColor.White;
    }
}

// ReSharper disable InconsistentNaming

namespace MorzeBalhe;

internal abstract class Program{
    private static Translator _translator;
    
    private static String[] items = {"Szövegről Morze-kódra", "Morze kódról szövegre", "Kilépés"};
    private static int selectedIndex = 0;

    private static void Main(string[] args){
        Configuration config = Configuration.loadFromFile("../../../config.json")!;

        _translator = new Translator(config);

        // <summary>
        //Fortnite Battle Royale | ha D=szóköz, p=rövid és l=hosszú (alapértelmezett konfiguráció)
        // </summary>
        String fortniteBattleRoyale = _translator.translateFromMorse("pplp lll plp l lp pp l p D lppp pl l l plpp p D plp lll lpll pl plpp p", true);
        // Console.WriteLine(fortniteBattleRoyale);
        
        Console.WriteLine("Üdv!");
        Console.WriteLine("A fordító a következő karaktereket veszi figyelembe:\n\tSzóköz karakter: " + config.SpaceCharacter);
        Console.WriteLine("\tRövid karakter: " + config.ShortCharacter);
        Console.WriteLine("\tHosszú karakter: " + config.LongCharacter);
        Console.Write("Ezek a karakterek a konfigurációs fájlban módosíthatóak.");

        pressAButtonToContinue();
        
        chooseAnOption();
    }

    private static void chooseAnOption(){
        writeCurrentChooseScreen();
        ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
        switch(consoleKeyInfo.Key){
            case ConsoleKey.UpArrow:
                manageOptionIndex(false);
                break;
            case ConsoleKey.DownArrow:
                manageOptionIndex(true);
                break;
            case ConsoleKey.Enter:
                executeCurrentSelectedCommand();
                break;
        }
        chooseAnOption();
    }
    
    private static void writeCurrentChooseScreen(){
        Console.ForegroundColor = ConsoleColor.White;
        Console.Clear();
        Console.Write("A következő lehetőségek állnak rendelkezésre (");
        textColorChangeToGreen("[Felnyíl] [Lenyíl] [Enter]");
        Console.WriteLine("):");
        for(int i = 0; i < items.Length; i++){
            Console.Write("(");
            if(selectedIndex == i){
                textColorChangeToGreen("x");
            }else{
                Console.Write(" ");
            }
            Console.WriteLine(") " + items[i]);
        }
        
    }
    
    private static void manageOptionIndex(Boolean isIncreasing){
        if(isIncreasing){
            if(selectedIndex == items.Length-1)selectedIndex = 0;
            else selectedIndex++;
        }else{
            if(selectedIndex == 0)selectedIndex = items.Length-1;
            else selectedIndex--;
        }
    }
    
    private static void executeCurrentSelectedCommand(){
        Console.Clear();
        textColorChangeToGreen(items[selectedIndex]);
        Console.WriteLine(" - várakozás a karakterláncra!");

        if(selectedIndex == 0)transleteToMorze();
        else if(selectedIndex == 1)translateFromMorze();
        else Environment.Exit(0);

    }
    
    private static void transleteToMorze(){
        String value = Console.ReadLine();
        Console.Clear();
        textColorChangeToGreen(value);
        Console.WriteLine(" folyamatban...");
        
        if(value.Length != 0){
            // Nem kell kiiratni, hiszen az isSilent=false, magától kiírja a program;
            _translator.translateToMorse(value);
            pressAButtonToContinue();
        }else{
            Console.WriteLine("Bip-bup-bip! Vissza a menübe...");
        }
    }

    private static void translateFromMorze(){
        String morze = Console.ReadLine();
        if(morze.Length == 0){
            Console.WriteLine("Vissza a menübe...");
        }else{
            Console.Clear();
            textColorChangeToGreen(morze);
            Console.WriteLine(" dekódolása...");
            // Nem kell kiiratni, hiszen az isSilent=false, magától kiírja a program;
            _translator.translateFromMorse(morze);
            pressAButtonToContinue();    
        }

    }

    private static void pressAButtonToContinue(){
        textColorChangeToGreen("\nRendicsek? A folytatáshoz nyomj meg egy gombot!");
        Console.ReadKey();
    }

    private static void textColorChangeToGreen(String value){
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(value);
        Console.ForegroundColor = ConsoleColor.White;
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour
{
    // Game config
    enum Screen { MainMenu, Password, Win };
    List<int> validLevels = new List<int>() { 1, 2, 3 };
    string[,] levelPasswords = new string[3,6] {
        { "books", "aisle", "shelf", "password", "font", "borrow" },
        { "prisoner", "handcuffs", "holster", "uniform", "arrest", "jailtime" },
        { "onomatopeia", "obnoxious", "redemption", "physiognomy", "hyacinth", "chargerpad" }
    };

    // Game state
    int level;
    Screen currentScreen;
    string currentPassword;

    // Start is called before the first frame update
    void Start()
    {
        this.ShowMainMenu();
    }

    void OnUserInput(string input) {
        if(input == "menu") {
            this.ShowMainMenu();
        } else if(this.currentScreen == Screen.MainMenu) {
            if(input == "exit" || input == "close" || input == "quit") {
                Application.Quit();
            }
            this.RunMainMenu(input);
        } else if(this.currentScreen == Screen.Password) {
            this.RunGame(input);
        } else { // only on win screen
            if(input == "") this.ShowMainMenu();
        }
    }

    void ShowMainMenu() {
        this.currentScreen = Screen.MainMenu;
        Terminal.ClearScreen();
        Terminal.WriteLine("Hello Princess");
        Terminal.WriteLine("");
        Terminal.WriteLine("What would you like to hack into?");
        Terminal.WriteLine("Press 1 for Fiddle-leaf fig");
        Terminal.WriteLine("Press 2 for Mango");
        Terminal.WriteLine("Press 3 for Princess");
        Terminal.WriteLine("Type 'quit' to exit");
    }

    void RunMainMenu(string input) {
        int temp = 0;
        if(int.TryParse(input, out temp) && this.validLevels.Contains(int.Parse(input))) {
            this.level = int.Parse(input);
            this.SetPassword();
        } else {
            Terminal.WriteLine("Unrecognized input. Try again.");
        }
    }

    void SetPassword() {
        this.currentScreen = Screen.Password;
        this.currentPassword = this.levelPasswords[this.level-1,
            Random.Range(0, levelPasswords.GetLength(1))];
        Terminal.ClearScreen();
        Terminal.WriteLine("Level: " + this.level + ". What is the password?");
        this.DisplayHint();
    }

    void RunGame(string input) {
        if(input == this.currentPassword) {
            this.currentScreen = Screen.Win;
            this.Win();
        } else {
            Terminal.WriteLine("Incorrect. Try again.");
            this.DisplayHint();
        }
    }

    void DisplayHint() {
        Terminal.WriteLine("Hint: " + this.currentPassword.Anagram());
    }

    void Win() {
        Terminal.ClearScreen();
        switch(this.level) {
            case 1:
                Terminal.WriteLine(@"
      _  _
     | || | _
    -| || || |
     | || || |-
      \_  || |
        |  _/
       -| | \
        |_|-  
                ");
                break;
            case 2:
                Terminal.WriteLine(@"
     _._     _,-'""""`-._
    (,-.`._,'(       |\`-/|
        `-.-' \ )-`( , o o)
              `-    \`_`""'-        
                ");
                break;
            case 3:
                Terminal.WriteLine(@"
          o 
       o^/|\^o
    o_^|\/*\/|^_o
   o\*`'.\|/.'`*/o
    \\\\\\|//////
     {><><@><><}
     `""""""""""""""""""`           
                ");
                break;
        }

    }
}

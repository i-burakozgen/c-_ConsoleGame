
using System;
using System.Runtime.CompilerServices;
using System.Xml.XPath;

Random random = new Random();
Console.CursorVisible = false;
int height = Console.WindowHeight - 1;
int width = Console.WindowWidth - 5;
bool shouldExit = false;

// Console position of the player
int playerX = 0;
int playerY = 0;

// Console position of the food
int foodX = 0;
int foodY = 0;

// Available player and food strings
string[] states = {"('-')", "(^-^)", "(X_X)"};
string[] foods = {"@@@@@", "$$$$$", "#####"};

// Current player string displayed in the Console
string player = states[0];

// Index of the current food
int food = 0;
bool food_consumed = false;
int score = 0;

info();
InitializeGame();

while (!shouldExit) 
{
    if(state() == 3)
    {
        FreezePlayer();
    }
    else
    {
        Move(ref height, ref width);
    }
}


bool TerminalResized() 
{
    return height != Console.WindowHeight - 1 || width != Console.WindowWidth - 5;
}

void ShowFood() 
{
    food = random.Next(0, foods.Length);

    foodX = random.Next(0, width - player.Length);
    foodY = random.Next(0, height - 1);

    Console.SetCursorPosition(foodX, foodY);
    Console.Write(foods[food]);
}

// Changes the player to match the food consumed
void ChangePlayer() 
{
    player = states[food];
    Console.SetCursorPosition(playerX, playerY);
    Console.Write(player);
}

// Temporarily stops the player from moving
void FreezePlayer() 
{
    System.Threading.Thread.Sleep(1000);
    player = states[0];
}

// Reads directional input from the Console and moves the player
void Move(ref int height, ref int width) 
{

    int lastX = playerX;
    int lastY = playerY;
    
    
    switch (Console.ReadKey(true).Key) 
    {
        case ConsoleKey.UpArrow:
            if (state() == 2){
                playerY-=3;
            }
            if (consumeFood() == 1){
                ChangePlayer();
                ShowFood();
            }
            playerY--;

            
            break;
		case ConsoleKey.DownArrow: 
            playerY++;
            if (state() == 2){
                playerY+=3;
            }
            if (consumeFood() == 1){
                ChangePlayer();
                ShowFood();
            } 
            break;
		case ConsoleKey.LeftArrow:  
            playerX--;
            if (state() == 2){
                playerX-=3;
            }
            if (consumeFood() == 1){
                ChangePlayer();
                ShowFood();
            } 
            break;
		case ConsoleKey.RightArrow: 
            playerX++;
            if (state() == 2){
                playerX+=3;
            }
            if (consumeFood() == 1){
                ChangePlayer();
                ShowFood();
            } 
            break;
		case ConsoleKey.Escape:     
            shouldExit = true;
            break;
        case ConsoleKey.Enter:
            resizeTerminal(ref height, ref width);            
            break;
        default:
            Console.WriteLine("Exiting from program, press any key to continue");
            Console.ReadKey();
            Console.Clear();
            shouldExit = true;
            break;

        
    }
    
    // Clear the characters at the previous position
    Console.SetCursorPosition(lastX, lastY);
    for (int i = 0; i < player.Length; i++) 
    {
        Console.Write(" ");
    }

    // Keep player position within the bounds of the Terminal window
    playerX = (playerX < 0) ? 0 : (playerX >= width ? width : playerX);
    playerY = (playerY < 0) ? 0 : (playerY >= height ? height : playerY);

    // Draw the player at the new location
    Console.SetCursorPosition(playerX, playerY);
    Console.Write(player);
}

// Clears the console, displays the food and player
void InitializeGame() 
{
    Console.Clear();
    ShowFood();
    Console.SetCursorPosition(0, 0);
    Console.Write(player);
}
void resizeTerminal(ref int height, ref int width)
{
    Console.WriteLine("enter height and width parameters, with comma separated");
    string [] values = Console.ReadLine().Trim().Split(",");
    var foo_h = values[0];
    var foo_w = values[1];
    if (values.Length == 2)
    {
        if (int.TryParse(foo_h, out height) && int.TryParse(foo_w, out width))
        {
        Console.WriteLine($"Height and width is fixed to: {height}, {width}");
        Console.WriteLine("Console is resized. Press enter to exit program");
        Console.ReadLine();
        Console.Clear();

        shouldExit = true;

        }
        else {
        Console.WriteLine("Please enter valid parameters, press enter to exit");
        Console.ReadKey();
        Console.Clear();
        shouldExit = true;

        }
    }
    else {
        Console.WriteLine("Please enter height and width, press enter to exit");
        Console.ReadKey();
        Console.Clear();

        shouldExit = true;

    }
    

}
void info(){
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("=======================================================");
    Console.WriteLine("   Welcome to GTA6: Burak Edition");
    Console.WriteLine("=======================================================");
    Console.WriteLine();
    
    Console.ResetColor();
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("Game Instructions:");
    Console.WriteLine("-------------------");
    Console.WriteLine("1. Use the arrow keys to navigate.");
    Console.WriteLine("2. Press 'Enter' to resize the terminal window.");
    Console.WriteLine("3. Press 'Esc' to exit the game.");
    Console.WriteLine();
    
    Console.ResetColor();
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Press 'Enter' to start the game!");
    Console.WriteLine("=======================================================");
    Console.ResetColor();
    Console.ReadLine();
}
int consumeFood()
{
    while (playerX == foodX && playerY == foodY){
        score++;
        return 1;
    }
    return 0;  
}
int state()
{
    int statePlayer = 0;
    if (player == states[0]){
        statePlayer = 1;
    }
    else if (player == states[1]){
        statePlayer = 2;
    }
    else if (player == states[2]){
        statePlayer = 3;
    }
    return statePlayer;

}
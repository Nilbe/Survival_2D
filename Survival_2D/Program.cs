using System;
using Raylib_cs;
using System.Numerics;
using System.Collections;


Raylib.InitWindow(800, 800, "SURVIVE");
Raylib.SetTargetFPS(60);

int HP = 100;
int water = 10;
int food = 10;

float speed = 2f;

Vector2 textPos = new Vector2(375, 450);
Vector2 textPos2 = new Vector2(50,600);

Texture2D playerImage = Raylib.LoadTexture("pixelGuy_Survival2D.png");
Texture2D enemyImage = Raylib.LoadTexture("monster_Survival2D.png");
Texture2D backgroundImage = Raylib.LoadTexture("camp_Survival2D.png");
Texture2D backgroundImage2 = Raylib.LoadTexture("lake_Survival2D.png");
Texture2D heartImage = Raylib.LoadTexture("heart.png");
Texture2D waterDropImage = Raylib.LoadTexture("waterdrop.png");
Texture2D burgerImage = Raylib.LoadTexture("bigMac.png");


ArrayList wallList = new ArrayList();

ArrayList enemyList = new ArrayList();
Rectangle enemyRect = new Rectangle(0,0, enemyImage.width, enemyImage.height);




Rectangle playerRect = new Rectangle(600, 400, playerImage.width, playerImage.height);
Rectangle campRect = new Rectangle(0, 0, backgroundImage.width, backgroundImage.height);
Rectangle lakeRect = new Rectangle(0, 0, backgroundImage2.width, backgroundImage2.height);
Rectangle campFireRect = new Rectangle(400, 450, 200, 100);
Rectangle waterRect = new Rectangle(100, 700, 200, 100);
Rectangle heartRect = new Rectangle(10, 25, heartImage.width, heartImage.height);
Rectangle waterDropRect = new Rectangle(50, 25, waterDropImage.width, waterDropImage.height);
Rectangle burgerRect = new Rectangle(100, 25, burgerImage.width, burgerImage.height);


Font tale = Raylib.LoadFont("Milonga-Regular.ttf");

string room = "camp";


while(!Raylib.WindowShouldClose())
{
    if(room == "camp")
    {
        Vector2 movement = ReadMovement(speed);

            playerRect.x += movement.X;
            playerRect.y += movement.Y;

            if(playerRect.x < 0 || playerRect.x + playerRect.width > Raylib.GetScreenWidth())
            {
                playerRect.x -= movement.X;
            }
            if(playerRect.y < 0 || playerRect.y + playerRect.height > Raylib.GetScreenHeight())
            {
                room = "lake";

                playerRect.y = 0;
            }

        if(Raylib.CheckCollisionRecs(playerRect, campFireRect))
        {
            if(Raylib.IsKeyPressed(KeyboardKey.KEY_E)) food ++;
        }
    }
    else if(room == "lake")
    {
        Vector2 movement = ReadMovement(speed);

            playerRect.x += movement.X;
            playerRect.y += movement.Y;

            if(playerRect.x < 0 || playerRect.x + playerRect.width > Raylib.GetScreenWidth())
            {
                playerRect.x -= movement.X;
            }
            if(playerRect.y < 0 || playerRect.y + playerRect.height > Raylib.GetScreenHeight())
            {
                room = "camp";

                playerRect.x = 480;
                playerRect.y = Raylib.GetScreenHeight() - playerRect.height;
            }
        if(Raylib.CheckCollisionRecs(playerRect, waterRect))
        {
            if(Raylib.IsKeyPressed(KeyboardKey.KEY_E)) water ++;
        }
    }
    if(room == "camp" || room == "lake")
    {
        Vector2 movement = ReadMovement(speed);

            playerRect.x += movement.X;
            playerRect.y += movement.Y;
    }

    //påbörjar ritandet av spelet
    Raylib.BeginDrawing();

    if(room == "camp")
    {
        Raylib.DrawTexture(backgroundImage, (int)campRect.x, (int)campRect.y, Color.WHITE);

        if(Raylib.CheckCollisionRecs(playerRect, campFireRect))
        {
            Raylib.DrawTextEx(tale, "Press E to eat", textPos, 20, 10, Color.WHITE);
        }
    }
    else if(room =="lake")
    {
        Raylib.DrawTexture(backgroundImage2, (int)lakeRect.x, (int)lakeRect.y, Color.WHITE);

        if(Raylib.CheckCollisionRecs(playerRect, waterRect))
        {
            Raylib.DrawTextEx(tale, "Press E to drink", textPos2, 20, 10, Color.WHITE);
        }
    }
    if(room == "camp" || room == "lake")
    {
        Raylib.DrawTexture(playerImage, (int)playerRect.x, (int)playerRect.y, Color.WHITE); 

        Raylib.DrawText(HP.ToString(), 50, 10, 25, Color.WHITE);
        Raylib.DrawText(water.ToString(), 150, 10, 25, Color.WHITE);
        Raylib.DrawText(food.ToString(), 225, 10, 25, Color.WHITE);

        Raylib.DrawTexture(heartImage, (int)heartRect.x, (int)heartRect.y, Color.WHITE);
        Raylib.DrawTexture(waterDropImage, (int)waterDropRect.x, (int)waterDropRect.y, Color.WHITE);
        Raylib.DrawTexture(burgerImage, (int)burgerRect.x, (int)burgerRect.y, Color.WHITE);
    }
    Raylib.EndDrawing();

    static Vector2 ReadMovement(float speed)
    {
        Vector2 movement = new Vector2();
        if(Raylib.IsKeyDown(KeyboardKey.KEY_W)) movement.Y = -speed;
        if(Raylib.IsKeyDown(KeyboardKey.KEY_S)) movement.Y = speed;
        if(Raylib.IsKeyDown(KeyboardKey.KEY_A)) movement.X = -speed;
        if(Raylib.IsKeyDown(KeyboardKey.KEY_D)) movement.X = speed;

        return movement;
    }
}
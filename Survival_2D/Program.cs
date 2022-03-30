using System;
using Raylib_cs;
using System.Numerics;

Raylib.InitWindow(800, 800, "SURVIVE");
Raylib.SetTargetFPS(60);

int HP = 100;
int water = 10;
int food = 10;
int heat = 3;

float speed = 3f;

Vector2 textPos = new Vector2(800, 300);

Texture2D playerImage = Raylib.LoadTexture("pixelGuy_Survival2D.png");
Texture2D backgroundImage = Raylib.LoadTexture("camp_Survival2D.png");

Rectangle playerRect = new Rectangle(600, 400, playerImage.width, playerImage.height);
Rectangle campRect = new Rectangle(0,0, backgroundImage.width, backgroundImage.height);
Rectangle campFireRect = new Rectangle(400, 400, 200, 100);
Rectangle waterRect = new Rectangle(200, 300, 200, 100);

string room = "camp";

while(!Raylib.WindowShouldClose())
{
    if(room == "camp" || room == "lake")
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
                 playerRect.y -= movement.Y;
            }
    }
    if(room == "camp")
    {
        if(Raylib.CheckCollisionRecs(playerRect, campFireRect))
        {

        }

    }


    Raylib.BeginDrawing();

    if(room == "camp" || room == "lake")
    {
        Raylib.DrawTexture(backgroundImage, (int)campRect.x, (int)campRect.y, Color.WHITE);
        Raylib.DrawTexture(playerImage, (int)playerRect.x, (int)playerRect.y, Color.WHITE);
        Raylib.DrawText(HP.ToString(), 10, 10, 20, Color.WHITE);
        Raylib.DrawText(water.ToString(), 40, 10, 20, Color.WHITE);
        Raylib.DrawText(food.ToString(), 60, 10, 20, Color.WHITE);
        Raylib.DrawText(heat.ToString(), 80, 10, 20, Color.WHITE);
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
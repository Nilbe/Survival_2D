using System;
using Raylib_cs;
using System.Numerics;
using System.Collections.Generic;


Raylib.InitWindow(800, 800, "SURVIVE");
Raylib.SetTargetFPS(60);

int HP = 100;
int water = 10;
int food = 10;

float speed = 1.5f;

Vector2 textPos = new Vector2(375, 450);
Vector2 textPos2 = new Vector2(50, 600);

Texture2D playerImage = Raylib.LoadTexture("pixelGuy_Survival2D.png");
Texture2D enemyImage = Raylib.LoadTexture("monster_Survival2D.png");
Texture2D backgroundImage = Raylib.LoadTexture("camp_Survival2D.png");
Texture2D backgroundImage2 = Raylib.LoadTexture("lake_Survival2D.png");
Texture2D heartImage = Raylib.LoadTexture("heart.png");
Texture2D waterDropImage = Raylib.LoadTexture("waterdrop.png");
Texture2D burgerImage = Raylib.LoadTexture("bigMac.png");

List<Rectangle> wallRects = new List<Rectangle>();
for (int i = 0; i < 1; i++)
{
wallRects.Add(new Rectangle(0, 0, 800, 300));
wallRects.Add(new Rectangle(0,0, 200, 800));
wallRects.Add(new Rectangle(0, 700, 450, 300));
wallRects.Add(new Rectangle(700, 0, 100, 800));
wallRects.Add(new Rectangle(550, 700, 300, 300));
}

List<Rectangle> wallRects2 = new List<Rectangle>();
for (int i = 0; i < 1; i++)
{
    wallRects2.Add(new Rectangle(0, 0, 1, 800));
    wallRects2.Add(new Rectangle(670, 0, 670, 800));
    wallRects2.Add(new Rectangle(0, 600, 100, 800));
    wallRects2.Add(new Rectangle(250, 600, 600, 800));
    wallRects2.Add(new Rectangle(0, 750, 800, 50));
    wallRects2.Add(new Rectangle(200, 50, 250, 200));
}

Rectangle playerRect = new Rectangle(500, 380, playerImage.width, playerImage.height);
Rectangle campRect = new Rectangle(0, 0, backgroundImage.width, backgroundImage.height);
Rectangle lakeRect = new Rectangle(0, 0, backgroundImage2.width, backgroundImage2.height);
Rectangle campFireRect = new Rectangle(400, 525, 200, 25);
Rectangle waterRect = new Rectangle(100, 700, 200, 100);
Rectangle heartRect = new Rectangle(10, 10, heartImage.width, heartImage.height);
Rectangle waterDropRect = new Rectangle(90, 10, waterDropImage.width, waterDropImage.height);
Rectangle burgerRect = new Rectangle(170, 10, burgerImage.width, burgerImage.height);
Rectangle enemyRect = new Rectangle(200, 400, enemyImage.width, enemyImage.height);

Font tale = Raylib.LoadFont("Milonga-Regular.ttf");

//all logik för spelet
string room = "camp";

while (!Raylib.WindowShouldClose())
{//logik för room camp
    if (room == "camp")
    {
        Vector2 movement = ReadMovement(speed);

        playerRect.x += movement.X;
        playerRect.y += movement.Y;

        if (playerRect.x < 0 || playerRect.x + playerRect.width > Raylib.GetScreenWidth())
        {
            playerRect.x -= movement.X;
        }
        if (playerRect.y < 0 || playerRect.y + playerRect.height > Raylib.GetScreenHeight())
        {
            room = "lake";

            playerRect.x = 480;
            playerRect.y = 0;
        }

        if (Raylib.CheckCollisionRecs(playerRect, campFireRect))
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_E)) food++; 
        }

         for (int i = 0; i < wallRects.Count; i++)
        {
            Rectangle rectangle = wallRects[i];

            wallRects[i] = rectangle;

            Raylib.DrawRectangleRec(wallRects[i], Color.BLANK);

            if(Raylib.CheckCollisionRecs(playerRect,wallRects[i]))
            {
                playerRect.x -= movement.X *2;
                playerRect.y -= movement.Y *2;
            }
        }
    }//logit för room lake
    else if (room == "lake")
    {
        Vector2 movement = ReadMovement(speed);

        playerRect.x += movement.X;
        playerRect.y += movement.Y;

        if (playerRect.x < 0 || playerRect.x + playerRect.width > Raylib.GetScreenWidth())
        {
            playerRect.x -= movement.X;
        }
        if (playerRect.y < 0 || playerRect.y + playerRect.height > Raylib.GetScreenHeight())
        {
            room = "camp";

            playerRect.x = 480;
            playerRect.y = Raylib.GetScreenHeight() - playerRect.height;
        }
        if (Raylib.CheckCollisionRecs(playerRect, waterRect))
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_E)) water++;
        }

         for (int i = 0; i < wallRects2.Count; i++)
        {
            Rectangle rectangle = wallRects2[i];

            wallRects2[i] = rectangle;

            Raylib.DrawRectangleRec(wallRects2[i], Color.BLANK);

            if(Raylib.CheckCollisionRecs(playerRect,wallRects2[i]))
            {
                playerRect.x -= movement.X *2;
                playerRect.y -= movement.Y *2;
            }
        }
    }//logik för båda rumen
    if (room == "camp" || room == "lake")
    {
        Vector2 movement = ReadMovement(speed);

        playerRect.x += movement.X;
        playerRect.y += movement.Y;
    }


    //påbörjar ritandet av spelet
    Raylib.BeginDrawing();
    //rotandet av room camp
    if (room == "camp")
    {
        Raylib.DrawTexture(backgroundImage, (int)campRect.x, (int)campRect.y, Color.WHITE);

        if (Raylib.CheckCollisionRecs(playerRect, campFireRect))
        {
            Raylib.DrawTextEx(tale, "Press E to eat", textPos, 20, 10, Color.WHITE);
        }
    }//ritkandet av room lake
    else if (room == "lake")
    {
        Raylib.DrawTexture(backgroundImage2, (int)lakeRect.x, (int)lakeRect.y, Color.WHITE);

        if (Raylib.CheckCollisionRecs(playerRect, waterRect))
        {
            Raylib.DrawTextEx(tale, "Press E to drink", textPos2, 20, 10, Color.WHITE);
        }
    }//ritandet av båda
    if (room == "camp" || room == "lake")
    {
        Raylib.DrawTexture(playerImage, (int)playerRect.x, (int)playerRect.y, Color.WHITE);

        Raylib.DrawTexture(heartImage, (int)heartRect.x, (int)heartRect.y, Color.WHITE);
        Raylib.DrawTexture(waterDropImage, (int)waterDropRect.x, (int)waterDropRect.y, Color.WHITE);
        Raylib.DrawTexture(burgerImage, (int)burgerRect.x, (int)burgerRect.y, Color.WHITE);

        Raylib.DrawText(HP.ToString(), 50, 20, 25, Color.WHITE);
        Raylib.DrawText(water.ToString(), 130, 20, 25, Color.WHITE);
        Raylib.DrawText(food.ToString(), 235, 20, 25, Color.WHITE);
    }
    Raylib.EndDrawing();
//kontroller för spelet
    static Vector2 ReadMovement(float speed)
    {
        Vector2 movement = new Vector2();
        if (Raylib.IsKeyDown(KeyboardKey.KEY_W)) movement.Y = -speed;
        if (Raylib.IsKeyDown(KeyboardKey.KEY_S)) movement.Y = speed;
        if (Raylib.IsKeyDown(KeyboardKey.KEY_A)) movement.X = -speed;
        if (Raylib.IsKeyDown(KeyboardKey.KEY_D)) movement.X = speed;

        return movement;
    }
}
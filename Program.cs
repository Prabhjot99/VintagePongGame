using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using GDIDrawer;
using System.Drawing;


namespace Lab02
{
    class Program
    {
        
        static void Main(string[] args)
        {
            //to remove the default setup
            CDrawer canvas = new CDrawer(bContinuousUpdate: false);
            //give a scale of 5
            canvas.Scale = 5;
            bool valid = false;
        

            
            //loop to repeat the game until user wishes
            do
            {

                //to add a text to the starting window
                canvas.AddText("Click to Play Game", 25, 30, 10, 100, 100, Color.Red);

                //initialise and declare the variables
                valid = false;
                bool runagain = false;
                Random number = new Random();
                
                //give the velocity of x position
                int xVel = 6;
                //velocity of y position
                int yVel = 4;
                // give the start point of the ball
                int xLoc = 0;
                int yLoc = 60;
                int score = 0;
                Point paddle;



                Random rng = new Random();

                //draw the border on the three sides of the drawer window
                //draw the border horizonatally at the top of the window
                for (int i = 0; i < canvas.ScaledWidth; ++i)
                    canvas.SetBBScaledPixel(i, 0, Color.Aqua);
                canvas.Render();
                //draw the border ast the bottom of the window
                for (int i = 0; i < canvas.ScaledWidth; ++i)
                    canvas.SetBBScaledPixel(i, 119, Color.Aqua);
                canvas.Render();
                //draw the border vertically on the right side of the window
                for (int i = 0; i < canvas.ScaledHeight; ++i)
                    canvas.SetBBScaledPixel(159, i, Color.Aqua);
                canvas.Render();
                //initialise paddle
             

                // used for retrieving drawer window coordinates
                bool clickDetected = false;     // use354d for detecting whether a mouse position was returned



             
                // retrieve a mouse click from the drawer

                do
                {

                    clickDetected = canvas.GetLastMouseLeftClickScaled(out paddle);
                }
                while (!clickDetected);

                do
                {
                    //draw the paddle
                    canvas.Clear();
                    canvas.GetLastMousePositionScaled(out paddle);
                    
                   canvas.AddLine(0, paddle.Y + 20, 0, paddle.Y, Color.Red, 20);
                   
                    //relate the location with velocity
                 

                    //return the ball when it touches the boundaries drawn on the right end of  the window
                    if ((xLoc + xVel) >= canvas.ScaledWidth - 1)
                    {

                        xVel = -xVel;

                    }
                    if ((yLoc + yVel) >= canvas.ScaledHeight - 1)
                    {
                        yVel = -yVel;
                    }
                    if ((xLoc + xVel == 0) && (yLoc + yVel == 0))


                    {
                        xVel = -xVel;
                        yVel = -yVel;
                    }

                    if (yLoc + yVel >= canvas.ScaledHeight - 2 || yLoc + yVel <= 0)
                    {
                        yVel = -yVel;
                    }
                    //ball changes its velocity when it touches the paddle
                    if (xLoc >= 0 && xLoc <2 && yLoc >= paddle.Y  && yLoc < paddle.Y + 20)
                    {
                        ++score;
                        //ball reverses its direction after touching the paddle
                        xVel = -xVel;

                    }
                    //relating velocity with the location
                    xLoc += xVel;
                    yLoc += yVel;
                    //draw the ball
                    canvas.AddRectangle(xLoc, yLoc, 2, 2, Color.Green);
                    canvas.Render();
                    Thread.Sleep(100);
                }
                while (yLoc < canvas.ScaledHeight && xLoc < canvas.ScaledWidth && xLoc > -10);
                //clear the screen 
                canvas.Clear();

                Point button;
                //condition to ask user if he wants to play the game again or not
                while (!runagain)
                {
                    //display the sinal scores
                    canvas.AddText($"Final Score : {score}", 40, 30, 10, 100, 100, Color.Red);

                    //angle to draw rectangle include the text to give user the option to play the game again
                    canvas.AddRectangle(80, 90, 30, 10, Color.Black, 3, Color.Green);
                    canvas.AddText("Play Again", 15, 75, 75, 40, 40, Color.Green);

                    //angle to draw rectangle include the text to give user the option to quit the game
                    canvas.AddRectangle(120, 90, 30, 10, Color.Black, 3, Color.Gray);
                    canvas.AddText("Quit", 15, 115, 75, 40, 40, Color.Gray);


                    canvas.GetLastMouseLeftClickScaled(out button);
                    //if the user wants to run the game again
                    if (button.X >= 80 && button.X <= 110 && button.Y >= 90 && button.Y <= 100)
                    {

                        // canvas.GetLastMouseLeftClickScaled(out button);
                        valid = false;
                        runagain = true;
                    }
                    
                
                    //if the user wants to quit
                    else if (button.X >= 120 && button.X <= 150 && button.Y >= 90 && button.Y <= 100)
                    {
                      
                        valid = true;
                        runagain = true;
                       
                    }
                    canvas.Render();
                    Thread.Sleep(30);
                    canvas.Clear();
                }
               
            }
            while (!valid);
        }
    }
}

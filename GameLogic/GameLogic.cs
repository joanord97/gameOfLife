using System;
using System.Threading;

namespace GameLogic
{

    public class Board{
        public Board(int width, int height)
        {
            Width = width;
            Height = height;

            board = new int[Width, Height];
            boardNeighbors = new int[Width, Height];
            countNeighbors();
            randomState();

            nCells = 0;

        }

        public Board(string file)
        {
            //TODO
            loadState(file);
        }

        public void randomState(){
            Random rnd = new Random();

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    board[j,i] = Convert.ToInt16(rnd.Next(2) == 0);
                }
            }
        }

        public void printState(){
            string topBottomBorder = new string('=', Width);
            topBottomBorder = "+" + topBottomBorder + "+";

            for (int i = 0; i < Height; i++)
            {
                if (i == 0)
                    System.Console.WriteLine(topBottomBorder);
                for (int j = 0; j < Width; j++)
                {
                    if (j == 0)
                        System.Console.Write("|");
                    if(board[j,i] == 1)
                        System.Console.Write("#");
                    else
                        System.Console.Write(" ");

                    if (j == Width-1)
                        System.Console.Write("|");
                }
                System.Console.WriteLine("");
                if(i == Height-1)
                    System.Console.WriteLine(topBottomBorder);
            }

            System.Console.WriteLine("number of cells: {0}", nCells);
        }

        public void printNeighborsCount(){
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    System.Console.Write(boardNeighbors[j,i]);
                }
                System.Console.WriteLine("");
            }
        }
        public void countNeighbors(){
            
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    boardNeighbors[x,y] = 0;
                    for (int y2 = y-1; y2 <= y+1; y2++)
                    {
                        //edge/of the board
                        if (y2 < 0 || y2 >= Height) continue;

                        for (int x2 = x-1; x2 <= x+1; x2++)
                        {
                            //edge/of the board
                            if (x2 < 0 || x2 >= Width) continue;

                            //node should not cout itself
                            if (x2 == x && y2 == y) continue;

                            if (board[x2,y2] == 1)
                                boardNeighbors[x,y]++;
                            
                        }
                        
                    }

                }
            }
        }

        public void updateState(){
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if(board[x,y] == 1){
                        if(boardNeighbors[x,y] <= 1)
                            board[x,y] = 0;
                        else if(boardNeighbors[x,y] == 2 || boardNeighbors[x,y] == 3)
                            board[x,y] = 1;
                        else if(boardNeighbors[x,y] > 3)
                            board[x,y] = 0;
                    }
                    else{
                        if(boardNeighbors[x,y] == 3)
                            board[x,y] = 1;
                        else
                            board[x,y] = 0;
                    }
                }
            }
        }

        public void countBoard(){
            nCells = 0;
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (board[x,y] == 1)
                        nCells++;
                }
            }
        }

        public void loadState(string file){
            //TODO
        }

        int Width;
        int Height;

        int[,] board;
        int[,] boardNeighbors;

        int nCells;
    }


    public class Game
    {
        public Game(int width, int height)
        {
            board = new Board(width, height);
        }

        public void run(){
            running = true;
            do
            {
                Console.Clear();

                board.countBoard();

                board.countNeighbors();
                
                board.printState();

                // System.Console.WriteLine("------");
                // board.printNeighborsCount();
                
                board.updateState();

                Thread.Sleep(300);                

            } while (running);
        }


        Board board;
        bool running;
        
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace PR_3._5_Surgai
{
    class SticksGame
    {
        public delegate void GameStateHandler(object sender, SticksGameStateArgs e);
        public event GameStateHandler TakeSticks;

        private int SticksNumber;
        private GameState GameState;
        private bool IsPersonWin = true;
        private bool IsComputerWin = true;
            
            

            public SticksGame(int sticksNumber)
            {
                this.SticksNumber = sticksNumber;

                GameState = GameState.READY;
            }

            public bool GetSticksByPerson(int sticksRequiredQuantity)
            {
                if (sticksRequiredQuantity > SticksNumber || sticksRequiredQuantity < 1 || sticksRequiredQuantity > 3)
                {
                    Console.WriteLine("Введiть кiлькiсть паличок вiд 1 до 3, та є меншою або дорiвнює наявнiй кількостi паличок = " + SticksNumber);

                    return false;
                }

                SticksNumber -= sticksRequiredQuantity;

                if (SticksNumber == 0)
                {
                    IsPersonWin = false;
                    GameState = GameState.FINISHED;
                }

                if (TakeSticks != null)
                {
                    TakeSticks(this, new SticksGameStateArgs($"Користувач узяв {sticksRequiredQuantity} палички", sticksRequiredQuantity, SticksNumber));
                }

                return true;
            }

            public bool GetSticksByComputer()
            {
                int SticksRequiredQuantity = 0;

                int MinValue = 1;
                int MaxValue = 4;

                Random random = new Random();

                while (SticksRequiredQuantity == 0 || SticksRequiredQuantity > SticksNumber)
                {
                    SticksRequiredQuantity = (int)random.Next(MinValue, MaxValue);
                }

                SticksNumber -= SticksRequiredQuantity;

                if (SticksNumber == 0)
                {
                    IsComputerWin = false;
                    GameState = GameState.FINISHED;
                }

                if (TakeSticks != null)
                {
                    TakeSticks(this, new SticksGameStateArgs($"Комп'ютер узяв {SticksRequiredQuantity} палички", SticksRequiredQuantity, SticksNumber));
                }

                return true;
            }

            public void startGame()
            {
                GameState = GameState.INPROGRESS;

                this.TakeSticks += showMessage;

                while (GameState == GameState.INPROGRESS)
                {
                    Console.WriteLine("Введiть кiлькiсть паличок --> ");

                    int Sticks = Int16.Parse(Console.ReadLine());

                    if (GetSticksByPerson(Sticks) && SticksNumber != 0)
                    {
                        GetSticksByComputer();
                    }
                        

                    Console.WriteLine("Кiлькiсть паличок, що залишилася = " + SticksNumber);
                }

                if (IsPersonWin)
                {
                    Console.WriteLine("Перемiг користувач!");
                }
                else
                {
                    Console.WriteLine("Перемiг комп'ютер!");
                }
            }

            private static void showMessage(object sender, SticksGameStateArgs e)
            {
                Console.WriteLine(e.Message);
            }
        }

        enum GameState
        {
            READY, INPROGRESS, FINISHED
        }

   }
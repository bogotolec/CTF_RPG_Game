﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace CTF_RPG_Game_Client
{
    class Game
    {
        ConnectionManager Manager;

        // CONSTS

        const int HEIGHT = 32;
        const int WIDTH = 82;

        const int BIG_WINDOW_WIDTH = 53;
        const int BIG_WINDOW_HEIGHT = HEIGHT - MESSAGE_WINDOW_HEIGHT;

        const int MESSAGE_WINDOW_WIDTH = BIG_WINDOW_WIDTH;
        const int MESSAGE_WINDOW_HEIGHT = 5;

        const int COMMAND_WINDOW_WIDTH = WIDTH - BIG_WINDOW_WIDTH;
        const int COMMAND_WINDOW_HEIGHT = 20;

        const int INFO_WINDOW_WIDTH = COMMAND_WINDOW_WIDTH;
        const int INFO_WINDOW_HEIGHT = HEIGHT - COMMAND_WINDOW_HEIGHT;

        // Simple constructor
        public Game(ConnectionManager ConnectionManager)
        {
            Manager = ConnectionManager;
        }

        public void Start()
        {
            JsonAnswer Json = Authorization();

            Console.Clear();
            while (true)
            {
                DrowGame(Json);
                Manager.Send(Console.ReadLine());
                Json = JsonConvert.DeserializeObject<JsonAnswer>(Manager.Get());
            }
        }

        // This function returns first Json answer
        private JsonAnswer Authorization()
        {
            string Answer = Manager.Get();

            do
            {
                Console.Write(Answer);

                string Message = Console.ReadLine();
                Manager.Send(Message);

                Answer = Manager.Get();
            }
            while (!IsJson(Answer));

            return JsonConvert.DeserializeObject<JsonAnswer>(Answer);
        }

        // Bad Json checker
        private bool IsJson(string String)
        {
            if (String.StartsWith("{\""))
                return true;
            return false;
        }

        // Very hard function
        // FU*K THIS SHIT
        public void DrowGame(JsonAnswer Answer)
        {
            Console.CursorTop = 0;
            Console.CursorLeft = 0;
            
            // Create message window
            StringBuilder MessageWindowBuilder = new StringBuilder();
            string[] MessageStrings = new string[MESSAGE_WINDOW_HEIGHT - 2];

            for (int i = 0; i < MESSAGE_WINDOW_HEIGHT - 2; i++)
            {
                MessageStrings[i] = "";
            }

            if (Answer.Message != null)
            {
                string[] MessageWords = Answer.Message.Split();

                int WordNumber = 0;
                for (int i = 0; i < MESSAGE_WINDOW_HEIGHT - 2; i++)
                {
                    int SymbolNumber = 0;

                    while (WordNumber != MessageWords.Length && 
                        SymbolNumber + (MessageWords[WordNumber]).Length < MESSAGE_WINDOW_WIDTH - 3)
                    {
                        MessageStrings[i] += " " + MessageWords[WordNumber];
                        SymbolNumber += MessageWords[WordNumber].Length + 1;
                        WordNumber++;
                    }

                    MessageStrings[i] = MessageStrings[i].PadRight(MESSAGE_WINDOW_WIDTH - 2);
                }
            }
            else
            {
                for (int i = 0; i < MESSAGE_WINDOW_HEIGHT - 2; i++)
                {
                    MessageStrings[i] = MessageStrings[i].PadRight(MESSAGE_WINDOW_WIDTH - 2);
                }
            }

            MessageWindowBuilder.Append('―', MESSAGE_WINDOW_WIDTH);
            for (int i = 0; i < MESSAGE_WINDOW_HEIGHT - 2; i++)
            {
                MessageWindowBuilder.Append('|' + MessageStrings[i] + '|');
            }
            MessageWindowBuilder.Append('―', MESSAGE_WINDOW_WIDTH);

            string MessageWindow = MessageWindowBuilder.ToString();
            // End of create message window


            // Create commands window
            StringBuilder CommandsWindowBuilder = new StringBuilder();
            string[] CommandStrings = new string[COMMAND_WINDOW_HEIGHT - 2];

            for (int i = 0; i < CommandStrings.Length; i++)
            {
                CommandStrings[i] = "";
            }

            if (Answer.Commands != null)
            {
                string[] CommandStringsTemp = Answer.Commands.Split('\n');
                
                for (int i = 0; i < COMMAND_WINDOW_HEIGHT - 2; i++)
                {
                    if (i < CommandStringsTemp.Length)
                    {
                        CommandStrings[i] = CommandStringsTemp[i].PadRight(COMMAND_WINDOW_WIDTH - 2);
                    }
                    else
                    {
                        CommandStrings[i] = "".PadRight(COMMAND_WINDOW_WIDTH - 2);
                    }
                }
            }
            else
            {
                for (int i = 0; i < COMMAND_WINDOW_HEIGHT - 2; i++)
                {
                    CommandStrings[i] = "".PadRight(COMMAND_WINDOW_WIDTH - 2);
                }
            }

            CommandsWindowBuilder.Append('―', COMMAND_WINDOW_WIDTH);
            for (int i = 0; i < COMMAND_WINDOW_HEIGHT - 2; i++)
            {
                CommandsWindowBuilder.Append('|' + CommandStrings[i] + '|');
            }
            CommandsWindowBuilder.Append('―', COMMAND_WINDOW_WIDTH);

            string CommandWindow = CommandsWindowBuilder.ToString();
            // End of create command window


            // Create info window
            StringBuilder InfoWindowBuilder = new StringBuilder();
            string[] InfoStrings = new string[INFO_WINDOW_HEIGHT - 2];

            for (int i = 0; i < InfoStrings.Length; i++)
            {
                InfoStrings[i] = "";
            }

            if (Answer.Info != null)
            {
                string[] InfoStringsTemp = Answer.Info.Split('\n');

                for (int i = 0; i < INFO_WINDOW_HEIGHT - 2; i++)
                {
                    if (i < InfoStringsTemp.Length)
                    {
                        InfoStrings[i] = InfoStringsTemp[i].PadRight(INFO_WINDOW_WIDTH - 2);
                    }
                    else
                    {
                        InfoStrings[i] = "".PadRight(INFO_WINDOW_WIDTH - 2);
                    }
                }
            }
            else
            {
                for (int i = 0; i < INFO_WINDOW_HEIGHT - 2; i++)
                {
                    InfoStrings[i] = "".PadRight(INFO_WINDOW_WIDTH - 2);
                }
            }

            InfoWindowBuilder.Append('―', INFO_WINDOW_WIDTH);
            for (int i = 0; i < INFO_WINDOW_HEIGHT - 2; i++)
            {
                InfoWindowBuilder.Append('|' + InfoStrings[i] + '|');
            }
            InfoWindowBuilder.Append('―', INFO_WINDOW_WIDTH);

            string InfoWindow = InfoWindowBuilder.ToString();
            // End of create info window

            // Create big window frame
            StringBuilder BigWindowFrameBuilder = new StringBuilder();

            BigWindowFrameBuilder.Append('―', BIG_WINDOW_WIDTH);
            for (int i = 0; i < BIG_WINDOW_HEIGHT - 2; i++)
            {
                BigWindowFrameBuilder.Append("||");
            }
            BigWindowFrameBuilder.Append('―', BIG_WINDOW_WIDTH);
            string BigWindowFrame = BigWindowFrameBuilder.ToString();
            // End of create big window frame

            int MessageWindowIndex = 0;
            int CommandsWindowIndex = 0;
            int InfoWindowIndex = 0;
            int BigWindowIndex = 0;
            int BigWindowFrameIndex = 0;

            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    Window window = AreaOf(i, j);

                    if (window == Window.Message)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(MessageWindow.Substring(MessageWindowIndex, MESSAGE_WINDOW_WIDTH));
                        MessageWindowIndex += MESSAGE_WINDOW_WIDTH;
                        j += MESSAGE_WINDOW_WIDTH - 1;
                    }

                    if (window == Window.Commands)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(CommandWindow.Substring(CommandsWindowIndex, COMMAND_WINDOW_WIDTH));
                        CommandsWindowIndex += COMMAND_WINDOW_WIDTH;
                        j += COMMAND_WINDOW_WIDTH - 1;
                    }

                    if (window == Window.Info)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(InfoWindow.Substring(InfoWindowIndex, INFO_WINDOW_WIDTH));
                        InfoWindowIndex += INFO_WINDOW_WIDTH;
                        j += INFO_WINDOW_WIDTH - 1;
                    }

                    if (window == Window.BigFrame)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(BigWindowFrame[BigWindowFrameIndex]);
                        BigWindowFrameIndex++;
                    }

                    if (window == Window.BigData && Answer.Type == "Map")
                    {
                        ConsoleColor color;
                        byte symbol = byte.Parse(Answer.BigWindow.Substring(BigWindowIndex * 2, 2),
                            System.Globalization.NumberStyles.HexNumber);

                        switch ((symbol & 0xF0) >> 4)
                        {
                            case 0:
                                color = ConsoleColor.Red;
                                break;
                            case 1:
                                color = ConsoleColor.Green;
                                break;
                            case 2:
                                color = ConsoleColor.DarkBlue;
                                break;
                            case 3:
                                color = ConsoleColor.Yellow;
                                break;
                            case 4:
                                color = ConsoleColor.Black;
                                break;
                            case 5:
                                color = ConsoleColor.White;
                                break;
                            case 7:
                                color = ConsoleColor.Blue;
                                break;
                            case 8:
                                color = ConsoleColor.Gray;
                                break;
                            case 9:
                                color = ConsoleColor.Magenta;
                                break;
                            default:
                                color = ConsoleColor.White;
                                break;
                        }
                        Console.ForegroundColor = color;

                        char c;
                        bool hasWritten = false;
                        switch (symbol & 0x0F)
                        {
                            case 0:
                                c = '>';
                                break;
                            case 1:
                                c = '<';
                                break;
                            case 2:
                                c = '^';
                                break;
                            case 3:
                                c = 'v';
                                break;
                            case 4:
                                c = '$';
                                break;
                            case 5:
                                c = 'X';
                                break;
                            case 6:
                                c = '!';
                                break;
                            case 7:
                                c = '@';
                                break;
                            case 8:
                                c = '#';
                                Console.BackgroundColor = color;
                                Console.Write(" ");
                                Console.BackgroundColor = ConsoleColor.Black;
                                hasWritten = true;
                                break;
                            case 9:
                                c = ' ';
                                break;
                            case 10:
                                c = '-';
                                break;
                            default:
                                c = ' ';
                                break;
                        }
                        
                        if (!hasWritten)
                            Console.Write(c);

                        BigWindowIndex++;
                    }

                    if (window == Window.BigData && Answer.Type == "Inventory")
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(Answer.BigWindow.Substring(BigWindowIndex, (BIG_WINDOW_WIDTH - 2) / 2));
                        BigWindowIndex += (BIG_WINDOW_WIDTH - 2) / 2;
                        Console.Write('|');
                        Console.Write(Answer.BigWindow.Substring(BigWindowIndex, (BIG_WINDOW_WIDTH - 2) / 2));
                        BigWindowIndex += (BIG_WINDOW_WIDTH - 2) / 2;
                        j += BIG_WINDOW_WIDTH - 3;
                    }
                }
                Console.Write('\n');
            }
            Console.Write((new StringBuilder()).Append(' ', 256));
            Console.CursorLeft = 0;
        }

        private Window AreaOf(int i, int j)
        {
            if (i < MESSAGE_WINDOW_HEIGHT && j < MESSAGE_WINDOW_WIDTH)
                return Window.Message;

            if ((i == HEIGHT - BIG_WINDOW_HEIGHT && j < BIG_WINDOW_WIDTH) ||
                (i == HEIGHT - 1 && j < BIG_WINDOW_WIDTH) ||
                (j == 0 && i >= HEIGHT - BIG_WINDOW_HEIGHT) ||
                (j == BIG_WINDOW_WIDTH - 1 && i >= HEIGHT - BIG_WINDOW_HEIGHT))
                return Window.BigFrame;

            if (i >= HEIGHT - BIG_WINDOW_HEIGHT && j < BIG_WINDOW_WIDTH)
                return Window.BigData;

            if (i < COMMAND_WINDOW_HEIGHT && j >= WIDTH - COMMAND_WINDOW_WIDTH)
                return Window.Commands;

            if (i >= HEIGHT - INFO_WINDOW_HEIGHT && j >= WIDTH - INFO_WINDOW_WIDTH)
                return Window.Info;

            return Window.None;
        }
    }

    enum Window { Message, BigData, BigFrame, Commands, Info, None };

    // Struct of Json answer
    class JsonAnswer
    {
        public string Message;
        public string BigWindow;
        public string Commands;
        public string Info;
        public string Level;
        public string Type;
    }
}

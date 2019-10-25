using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Question1
{
    class Program
    {
        //GIVEN: Default sentence
        const string givenSentence = "Please replace all characters eqauls to the letter 'A' with an underscore (_)";
        //Boolean varaible used to toggle case sensitivity
        static bool blcaseSensitive = false;
        //GIVEN: Default character
        static char chReplace = 'A';

        static void Main(string[] args)
        {
            string strResponse = null;
            bool blExit = false;

            while (!blExit)
            {
                Console.WriteLine("Please inidcate which sentence's characters must be replace:\n(Y) - Use the default given sentence\n(N) - Input your own sentence\n(C) - Change the replacement character [Current: "+chReplace+"]\n(T) - Toggle case sensitivity");
                //Display case sensitivity
                if (blcaseSensitive)
                {
                    Console.WriteLine("[Case sensitiviy: ON]");
                }
                else
                {
                    Console.WriteLine("[Case sensitiviy: OFF]");
                }
                //Get the users response
                strResponse = getLine();
                //Case statement to compute user response
                switch (strResponse)
                {
                    case "Y":
                        Console.WriteLine(replaceLetter());
                        blExit = checkExit();
                        break;
                    case "N":
                        string newSentence = null;
                        Console.WriteLine("Please enter your sentence:");
                        newSentence = Console.ReadLine();
                        Console.WriteLine(replaceLetter(defaultSentence: newSentence));
                        blExit = checkExit();
                        break;
                    case "T":
                        if (blcaseSensitive)
                        {
                            blcaseSensitive = false;
                        }else
                        {
                            blcaseSensitive = true;
                        }
                        break;
                    case "C":
                        Console.WriteLine("Please enter the new character to be replaced:");
                        string newChar = getLine();
                        setCharacter(newChar);
                        break;
                    default:
                        Console.WriteLine("Incorrect input please try again!");
                        break;
                }
                Console.Clear();
            }

        }

        /*The replaceLetter function will be used to replace the characters inside the string 
            -Note: The function has a optional parameter 'defaultSentence' which can be
                   overwritten to use a user defined sentence.
         */
        static string replaceLetter(string defaultSentence = givenSentence)
        {
            char oldChar = chReplace;
            string newSentence = "";

            /*The following code is what I would have used:
            if (!blcaseSensitive)
            {
               newSentence = Regex.Replace(defaultSentence, oldChar.ToString(), "_", RegexOptions.IgnoreCase);
            }
            else
            {
                newSentence = Regex.Replace(defaultSentence, oldChar.ToString(), "_");
            }
             -but the question stipulates to use a loop.
            */


            /*a For-loop function that can be used to replace the characters in the string for both case sensitive
              and case insensitive sentences
             */
            for(int i = 0; i < defaultSentence.Length; i++)
            {
                if (!blcaseSensitive)
                {
                    if (Char.ToUpper(defaultSentence[i]) == oldChar)
                    {
                        newSentence = newSentence + '_';
                    }
                    else
                    {
                        newSentence = newSentence + defaultSentence[i];
                    }
                }
                else
                {
                    if (defaultSentence[i] == oldChar)
                    {
                        newSentence = newSentence + '_';
                    }
                    else
                    {
                        newSentence = newSentence + defaultSentence[i];
                    }
                }
            }

            return "\nOutput sentence:\n" +newSentence+"\n";
        }

        // Basic helper function to ask the user if he/she wants to terminate the program.
        static bool checkExit()
        {
            string strConfirm = null;
            bool blResponse = false;
            bool blClose = false;

            while (!blResponse)
            {
                Console.WriteLine("Do you want to exit? [Y/N]:");

                strConfirm = getLine();

                switch (strConfirm)
                {
                    case "Y":
                        blResponse = true;
                        blClose = true;
                        break;
                    case "N":
                        blResponse = true;
                        blClose = false;
                        break;
                    default:
                        Console.WriteLine("Incorrect input please try again!");
                        break;
                }
            }
            return blClose;
        }

        //Helper function to set a new character to replace the current default character.
        static void setCharacter(string strLine)
        {
            if (strLine.Length == 1)
            {
                chReplace = strLine[0];
                Console.WriteLine("Replacement charater set to "+chReplace);
            }
            else
            {
                Console.WriteLine("Incorrect input, a character can only be a single letter/number. Please try again");
            }
        }

        //Basic helper function to read the users response in uppercase.
        static string getLine()
        {
            return Console.ReadLine().ToUpper();
        }
    }
}

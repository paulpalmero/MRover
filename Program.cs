using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover_V2
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                Console.WriteLine("Enter Rover Data -  Sample Data is as Follows");
                Console.WriteLine("******************************************************");
                Console.WriteLine("Bound1(positive int) Bound2 (positive int)");
                Console.WriteLine("5 5");
                Console.WriteLine("PosX(positive int) PosY(positive int) Heading(N,S,E,O)");
                Console.WriteLine("1 2 N");
                Console.WriteLine("Instruction1(M, R, L) Instruction2(M, R, L) ..... ");
                Console.WriteLine("L M L M L M L M M");
                Console.WriteLine("Press enter twice after your last input ");
                Console.WriteLine("EXPECTED OUTPUT = 1 3 N");
                Console.WriteLine("Each rover is deployed individually. Error does not stop the explorations");
                Console.WriteLine("******************************************************");


                //prepare console for reading input
                //each time the user enters a line its added to the list
                string s = "0";
                List<string> userInput = new List<string> { };
                while (!string.IsNullOrEmpty(s))
                {
                    s = Console.ReadLine();
                    
                    //add the line except if it is blank (for example the last line after the input
                    if (s.Trim() != "")
                    {
                        userInput.Add(s);
                    }
                    
                }

                //Define 2 Key/Value dictionaries to hold the records containing the position and the instructions
                IDictionary<int, string> PositionRecords = new Dictionary<int, string>();
                IDictionary<int, string> InstructionRecords = new Dictionary<int, string>();

                //set counter to use as Key in the dictionaries
                int MasterCounter = 1;
                int DetailCounter = 1;

                //loop thru all lines from the console application
                for (int x = 1; x < userInput.Count; x++)
                {
                    //perform some cleaning before adding the lines. This removes all spaces between characters and also trims the begining and end of the line
                    string CleansedRecord = Validation.CleanLines(userInput[x]);

                    // we assume that odd records have the position data an the even ones have the isntructions
                    if (x % 2 != 0) //odd
                    {
                        //add the string to the dictionary 
                        PositionRecords.Add(MasterCounter, CleansedRecord);
                        MasterCounter++;
                    }
                    else
                    {
                        //add the string to the dictionary 
                        InstructionRecords.Add(DetailCounter, CleansedRecord);
                        DetailCounter++;
                    }
                }

                //we are ready to start deploying our Rovers
                //we assume that the input records are ok, but we will perform a validation anyway

                //Get First Line of Input from User and clean it
                string CleansedBound = Validation.CleanLines(userInput[0]);
                string[] bounds = CleansedBound.Split();

                //Validate if bounds are correct: 2 values of type Integer each
                //bounds are thrown as an exception and not as a string message
                Validation.Validate(bounds);

                int bound1 = int.Parse(bounds[0]);
                int bound2 = int.Parse(bounds[1]);

                //iterate all elements in the header dictionary
                // we assume that each entry is a Rover
                foreach (KeyValuePair<int, string> item in PositionRecords)
                {

                    //create a new rover for each entry
                    Rover rover = new Rover();
                    rover.Id = item.Key;
                    //assign the position record to the position property
                    rover.Positions = item.Value;

                    //set bounds
                    rover.Bounds.x = bound1;
                    rover.Bounds.y = bound2;

                    // look for the corresponding detail lines by searching by key in the dictionary
                    if (InstructionRecords.ContainsKey(item.Key)){
                        //assign the instruction records to the instruction property
                        rover.Instructions = InstructionRecords[item.Key];
                    }

                    //Deploy rover in Plateau
                    //Also several validations are done in order to check if the instruccions and data are enough to allow
                    //the rover to start exloring
                    rover.DeployRover();

                    //check if all was ok
                    if (rover.CanBeDeployed)
                    {
                        //call the start exploring method
                        string result = rover.StartExploring();

                        //print error if something went wrong or print final coordinates
                        if (result != "")
                        {
                            Console.WriteLine(result + " For Rover " + rover.Id );
                            //continue loop for next rover
                            continue;
                        }
                        else
                        {
                            Console.WriteLine(rover.XPos + " " + rover.YPos + " " + rover.Heading);
                        }
                    }
                    
                }
               

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Pulse Enter para Salir");
            Console.Read();


        }
    }
}

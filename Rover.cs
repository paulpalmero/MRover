using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover_V2
{
    /// <summary>
    /// Class to describe a rover
    /// </summary>
    class Rover 
    {

        private string _heading;
        int _xPos;
        int _yPos;
        public Coords Bounds;
        int _Id;
        private string position;
        private string instructions;
        private bool canBeDeployed;


        /// <summary>
        /// Struct that contains the Bounds
        /// </summary>
        public struct Coords
        {
            public int x, y;

            public Coords(int p1, int p2)
            {
                x = p1;
                y = p2;
            }
        }

        /// <summary>
        /// Heading Property (N, S, E, O)
        /// </summary>
        public String Heading
        {
            get { return _heading; }
            set { _heading = value; }
        }

        /// <summary>
        /// X Position Property
        /// </summary>
        public int XPos
        {
            get { return _xPos; }
            set { _xPos = value; }
        }

        /// <summary>
        /// Y Position Property
        /// </summary>
        public int YPos
        {
            get { return _yPos; }
            set { _yPos = value; }
        }

        /// <summary>
        /// ID Setter and Getter
        /// </summary>
        public int Id { get => _Id; set => _Id = value; }
        
        /// <summary>
        /// Positions Getter and Setters
        /// </summary>
        public string Positions { get => position; set => position = value; }

        /// <summary>
        /// Instructions Getter and Setters
        /// </summary>
        public string Instructions { get => instructions; set => instructions = value; }


        /// <summary>
        /// Seeter for the Can be deployed property
        /// </summary>
        public bool CanBeDeployed { get => canBeDeployed; set => canBeDeployed = value; }

        /// <summary>
        /// Method to deploy rover
        /// </summary>
        public void DeployRover()
        {

            string ValidateInitialPosition = Validation.ValidateInitialPosition(this.Positions, this);
            string ValidateInstructions = Validation.ValidateInstructions(this.Instructions);

            if (ValidateInitialPosition == "" && ValidateInstructions == "")
            {
                this.CanBeDeployed = true;
                SetInitialPosition();
            }
            else
            {
                Console.WriteLine("Rover " + this.Id + ":" + ValidateInitialPosition + "-" + ValidateInstructions);
            }

        }

        /// <summary>
        /// Set to rover to its starting point
        /// </summary>
        private void SetInitialPosition()
        {
            string [] splittedstring = this.Positions.Split();

            XPos = int.Parse(splittedstring[0]);
            YPos = int.Parse(splittedstring[1]);
            Heading = splittedstring[2].ToString().ToUpper();
        }

        /// <summary>
        /// Perform INSTRUCTION
        /// </summary>
        /// <param name="action">(M)Move or (S)Spin</param>
        public string StartExploring()
        {

            string[] actions = this.Instructions.Split();
            string errorMessage = "";

            foreach (string action in actions)
            {
                if (action.ToUpper() == "M")
                {
                   errorMessage = Move();
                    if (errorMessage != "")
                    {
                        break;
                    }
                }
                else
                {
                     
                    errorMessage = Spin(action.ToUpper());
                    if (errorMessage != "")
                    {
                        break;
                    }
                }

            }

            return errorMessage;
            
        }


  
        /// <summary>
        /// Move Rover. Depending on Heading update X or Y value
        /// </summary>
        public string Move()
        {
           
            switch (Heading.ToUpper())
            {
                case "N":
                    YPos++;
                    break;
                case "S":
                    YPos--;
                    break;
                case "E":
                    XPos++;
                    break;
                default:
                    XPos--;
                    break;                      
            }

            //Check if rover has gone out of bounds after the movement
            return Validation.ValidateInstruction(this);
            
        }


        /// <summary>
        /// Spin Rover Left or Right
        /// Update Heading with new value
        /// </summary>
        /// <param name="direction">Direction to whivh the rover will tourn to</param>
        public string Spin(string direction)
        {
            string error = "";
            //validation can also be called from the program itself in order to
            //separate concerns: if a new validation is added then we have to update the rover class as well
            //as the validation class. This is not optimal.:-(
            //error = Validation.ValidateDirection(direction);

            switch (direction.ToUpper())
            {
                case "L":
                    if (Heading == "N")
                    {
                        Heading = "O";
                    }
                    else if (Heading == "O")
                    {
                        Heading = "S";
                    }
                    else if (Heading == "S")
                    {
                        Heading = "E";
                    }
                    else
                    {
                        Heading = "N";
                    }
                    break;
                case "R":
                    if (Heading == "N")
                    {
                        Heading = "E";
                    }
                    else if (Heading == "E")
                    {
                        Heading = "S";
                    }
                    else if (Heading == "S")
                    {
                        Heading = "O";
                    }
                    else
                    {
                        Heading = "N";
                    }
                    break;
            }

            return error;
            

        }

    }


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover_V2
{
    class Validation
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputdata"></param>
        /// <returns></returns>
        public static string CleanLines(string inputdata)
        {
            return inputdata = string.Join(" ", inputdata.Split(new char[0], StringSplitOptions.RemoveEmptyEntries).ToList().Select(x => x.Trim()));
        }
        
        
        /// <summary>
        /// Validate bounds
        /// </summary>
        /// <param name="bounds">bounds from fisrt line of input</param>
        public static void Validate(string[] bounds)
        {

            if (bounds.Count() != 2)
            {

                throw new Exception("Bounds are not Correct. Please enter 2 Integer values (watch spaces ;-)");

            }

            foreach (string s in bounds)
            {
                try
                {
                    int.Parse(s);
                } catch
                {
                    throw new Exception ("Bounds must be of Type Integer");
                }
            }

            if (int.Parse(bounds[0]) < 1)
            {
                throw new Exception("First Digit of Bounds must be a positive Integer");
            }

            if (int.Parse(bounds[1]) < 1)
            {
                throw new Exception("Second Digit of Bounds must be a positive Integer");
            }


        }

     
    
        /// <summary>
        /// Validate Instruction Type
        /// </summary>
        /// <param name="direction">turn left or right</param>
        public static string ValidateInstructions(string instructions)
        {
            
            string errormessage = "";

            bool test = string.IsNullOrEmpty(instructions);

            if (test)
            {
                return errormessage = "instructions are empty";
            }
           
            string [] splittedString = instructions.Split();

            if (splittedString.Count() < 0)
            {
                return errormessage = "instructions are empty";
            }

            foreach (string s in splittedString)
            {
                if (s.ToUpper() != "R" && s.ToUpper() != "L" && s.ToUpper() != "M")
                {

                    return errormessage = "Invalid Instructions. Only L,R or M are allowed";
                }

            }

            return errormessage;

        }

        /// <summary>
        /// Validate the rovers initial position
        /// </summary>
        /// <param name="data"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static string ValidateInitialPosition(string data, Rover r)
        {

            string returnerror = "";


            bool test = string.IsNullOrEmpty(data);

            if (test)
            {
                return returnerror = "Initial Position is empty";
            }


            string[] splittedString = data.Split();

            if (splittedString.Count() != 3)
            {
                return returnerror = "Initial Position needs 3 values";
            }

            try
            {
                int.Parse(splittedString[0]);

            }
            catch
            {
                return returnerror = "First Digit must be a positive Integer";
            }

            if (int.Parse(splittedString[0]) < 0)
            {
                return returnerror = "First Digit must be a positive Integer";

            }

            if (int.Parse(splittedString[0]) > r.Bounds.x)
            {
                return returnerror = "First Digit must be within the bounds";
            }


            try
            {
                int.Parse(splittedString[1]);
            }
            catch
            {
                return returnerror = "Second Digit must be a positive Integer";

            }

            if (int.Parse(splittedString[1]) < 0)
            {
                return returnerror = "Second Digit must be a positive Integer";

            }

            if (int.Parse(splittedString[1]) > r.Bounds.y)
            {
                return returnerror = "Second Digit must be within the bounds";

            }


            if (splittedString[2].ToUpper() != "N" && splittedString[2].ToUpper() != "S" && splittedString[2].ToUpper() != "E" && splittedString[2].ToUpper() != "O")
            {

                return returnerror = "Heading is not correct";

            }




            return returnerror;

        }


        /// <summary>
        /// VAlidate if Rovers goes out of bound resulting form a MOve action
        /// </summary>
        /// <param name="r">rover object</param>
        public static string ValidateInstruction(Rover r)
        {

            string result= "";


            if (r.XPos > r.Bounds.x)
            {
                result = "Rover went out of Bounds X";
                return result;
            }

            if (r.XPos < 0)
            {
                result = "Rover went out of Bounds X (-)";
                return result;
            }

            if (r.YPos > r.Bounds.y)
                {
                result = "Rover went out of Bounds Y";
                return result;
                }

            if (r.YPos < 0)
            {
                result = "Rover went out of Bounds Y (-)";
                return result;
            }


            return result;

           
        }
    }
}

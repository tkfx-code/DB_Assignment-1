using System.Security.Cryptography;

/*
 * Create a simple Student class with:
 * int StudentID
 * string FirstName
 * string LastName
 * string City
 * 
 * Use EF Core to connec to DB, and let the user choose:
 * Register new student
 * Change a student
 * List all students
 * 
 * Try to keep your program organized and follow OOP
 */
namespace ConsoleApp6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //create database
            var dbContext = new StudentDBContext();

            //Create a list for easier LINQ
            var Students = new List<Student>();

            //Save database
            dbContext.SaveChanges();

            var run = true;

            while (run) {
                //Show Menu
                ShowMenu();
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    //Register new student
                    case 1:
                        Console.WriteLine("Du har valt att registrera en ny student.");
                        AddStudent(dbContext);
                        break;

                    //Change information of existing student 
                    case 2:
                        Console.WriteLine("Du har valt att ändra information av en existerande student");
                        ChangeStudent(dbContext);
                        break;

                    //List all students
                    case 3:
                        Console.WriteLine("Du har valt att lista alla nuvarande studenter.");
                        PrintStudents(dbContext);
                        break;

                    //End program
                    default:
                        Console.WriteLine("Du har valt att avsluta programmet");
                        Console.WriteLine();
                        run = false;
                        break;
                }
            }
            Environment.Exit(0);
        }
        public static void ShowMenu()
        {
            Console.WriteLine("MENY");
            Console.WriteLine("1. Registerera ny student: ");
            Console.WriteLine("2. Ändra information om existerande student");
            Console.WriteLine("3. Lista alla nuvarande studenter");
            Console.WriteLine("Tryck 0 för att avsluta programmet.");
        }
        public static void AddStudent(StudentDBContext dbContext)
        {
            //Student user input
            Console.WriteLine("Vänligen ange studentens förnamn: ");
            var firstName = Console.ReadLine();
            Console.WriteLine("Ange studentens efternamn: ");
            var lastName = Console.ReadLine();
            Console.WriteLine("Ange vilken ort studenten bor på: ");
            var city = Console.ReadLine();

            //Create student from input and add to DB 
            var newStudent = new Student() { FirstName = firstName, LastName = lastName, City = city };
            dbContext.Add(newStudent);
            dbContext.SaveChanges();
            //Empty line for esthetics
            Console.WriteLine();
        }
        public static void ChangeStudent(StudentDBContext dbContext)
        {
            Console.WriteLine("Ange studentens För- och Efternamn");
            string name = Console.ReadLine();
            //Create array to read user input first and last name
            string[] fullName = name.Split(' ');
            var fName = fullName[0];
            var lName = fullName[1];
            bool found = false;

            //Run loop to find student in DB
            foreach (var person in dbContext.Students.Where(x=>x.FirstName != null))
            {
                //Find exact match to user input
                if (person.FirstName.Contains(fName) && person.LastName.Contains(lName))
                {
                    //Ask what user wants to change
                    Console.WriteLine("Välj vilken information du vill ändra: ");
                    Console.WriteLine("1. Studentens förnamn");
                    Console.WriteLine("2. Studentens efternamn");
                    Console.WriteLine("3. Studentens ort");
                    int changeChoice = Convert.ToInt32(Console.ReadLine());

                    if (changeChoice == 1)
                    {
                        Console.WriteLine("Vad är studentens nya förnamn?");
                        //Change student First name to user input
                        var fornamn = Console.ReadLine();
                        person.FirstName = fornamn;
                        found = true;
                    }
                    if (changeChoice == 2)
                    {
                        Console.WriteLine("Vad är studentens nya efternamn?");
                        //Change student Last name to user input
                        var efternamn = Console.ReadLine();
                        person.LastName = efternamn;
                        found = true;
                    }
                    if (changeChoice == 3)
                    {
                        Console.WriteLine("Vilken stad bor studenten i");
                        //Change student City to user input
                        var std = Console.ReadLine();
                        person.City = std;
                        found = true;
                    }
                    dbContext.SaveChanges();
                }
                //If person not found write error message.
                if (!found)
                {
                    Console.WriteLine("Studenten existerar inte. Vänligen försök igen");
                    break;
                }
                //Clear array so we start with empty each time
                Array.Clear(fullName);
                //Empty line for esthetics
                Console.WriteLine();
            }
        }
        public static void PrintStudents(StudentDBContext dbContext)
        {
            //for loop lists one student per row 
            Console.WriteLine("Här är alla listade studenter: ");
            string firstName = "Förnamn";
            string lastName = "Efternamn";
            string city = "Ort";
            //Print header
            Console.WriteLine($"{firstName.PadRight(10)}|{lastName.PadRight(10)}|{city.PadRight(10)}");
            //Print student info, skip if null
            foreach (var item in dbContext.Students.Where(x=>x.FirstName != null))
            {
                Console.WriteLine($"{item.FirstName.PadRight(10)} {item.LastName.PadRight(10)} {item.City.PadRight(10)}");
            }
            //Empty line for estethics
            Console.WriteLine();
        }
    }
}

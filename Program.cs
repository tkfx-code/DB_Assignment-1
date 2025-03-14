using System.Security.Cryptography;

namespace ConsoleApp6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //testdata
            //var s1 = new Student() { FirstName = "Märta", LastName = "Dahlberg", City = "Stockholm" };
            //var s2 = new Student() { FirstName = "Student", LastName = "Efternamn", City = "Y" };
            var Students = new List<Student>();

            var dbContext = new StudentDBContext();
            //dbContext.Add(s1);
            //dbContext.Add(s2);
            //spara innan du försöker accessa
            dbContext.SaveChanges();

            var run = true;

            while (run) { 
            //Lägg till menyval att ; registrera ny student, ändra existerande student, lista alla studenter
            Console.WriteLine("MENY");
            Console.WriteLine("1. Registerera ny student: ");
            Console.WriteLine("2. Ändra information om existerande student");
            Console.WriteLine("3. Lista alla nuvarande studenter");
            Console.WriteLine("Tryck 0 för att avsluta programmet.");
            int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    //Registrera ny student
                    case 1:
                        //Be User om input
                        Console.WriteLine("Du har valt att registrera ny student.\n Vänligen ange studentens förnamn: ");
                        var firstName = Console.ReadLine();
                        Console.WriteLine("Ange studentens efternamn: ");
                        var lastName = Console.ReadLine();
                        Console.WriteLine("Ange vilken ort studenten bor på: ");
                        var city = Console.ReadLine();

                        //Skapa student från input och lägg till i databasen, spara
                        var newStudent = new Student() { FirstName = firstName, LastName = lastName, City = city };
                        dbContext.Add(newStudent);
                        dbContext.SaveChanges();
                        Console.WriteLine();
                        break;

                    //Ändra information om existerande student
                    case 2:
                        Console.WriteLine("Du har valt att ändra information av en existerande student");
                        Console.WriteLine("Ange studentens För- och Efternamn");
                        string name = Console.ReadLine();
                        string[] fullName = name.Split(' ');
                        var fName = fullName[0];
                        var lName = fullName[1];

                        //Kör en for loop för att hitta studenten i listan 
                        foreach (var person in dbContext.Students)
                        {
                            //Linq hitta vart name matchar för och efternamn (Letar inte efter dupliceringar)
                            if (person.FirstName.Contains(fName) && person.LastName.Contains(lName))
                            {
                                //Fråga vad som ska ändras - för/efternamn/stad
                                Console.WriteLine("Välj vilken information du vill ändra: ");
                                Console.WriteLine("1. Studentens förnamn");
                                Console.WriteLine("2. Studentens efternamn");
                                Console.WriteLine("3. Studentens ort");
                                int changeChoice = Convert.ToInt32(Console.ReadLine());
                                //Sätt ny variabel på user input data
                                if (changeChoice == 1)
                                {
                                    Console.WriteLine("Vad är studentens nya förnamn?");
                                    //byt studentens förnamn till input
                                    var fornamn = Console.ReadLine();
                                    person.FirstName = fornamn;
                                    break;
                                }
                                if (changeChoice == 2)
                                {
                                    Console.WriteLine("Vad är studentens nya efternamn?");
                                    //byt studentens efternamn till input
                                    var efternamn = Console.ReadLine();
                                    person.LastName = efternamn;
                                    break;
                                }
                                if (changeChoice == 3)
                                {
                                    Console.WriteLine("Vilken stad bor studenten i");
                                    //byt den students stad till input
                                    var std = Console.ReadLine();
                                    person.City = std;
                                    break;
                                }
                                //!! Byta till for loop? Eller lägga till en mening "om inget matchar"?
                            }
                        }
                        dbContext.SaveChanges();
                        //Clear array 
                        Array.Clear(fullName);
                        Console.WriteLine();
                        break;

                    //Lista alla studenter
                    case 3:
                        //for loop som skrivver en rad för varje student, med en header av för/efter/stadsnamn
                        //Hämta data från fil
                        Console.WriteLine("Här är alla listade studenter: ");
                        string f = "Förnamn";
                        string l = "Efternamn";
                        string o = "Ort";
                        Console.WriteLine($"{f.PadRight(10)}|{l.PadRight(10)}|{o.PadRight(10)}");
                        foreach (var item in dbContext.Students)
                        {
                            Console.WriteLine($"{item.FirstName.PadRight(10)} {item.LastName.PadRight(10)} {item.City.PadRight(10)}");
                            Console.WriteLine("");
                        }
                        Console.WriteLine();
                        break;

                    //Avsluta programmet
                    default:
                        Console.WriteLine("Du har valt att avsluta programmet");
                        Console.WriteLine();
                        run = false;
                        break;
                }
            }
            Environment.Exit(0);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;

namespace Lab4._2
{
    public class Movie
    {
        // only code within this Movie class will be able to access the title and category

        private string title;
        private string category;
        private int runtime;
        private int year;
        private int metacritic;


        // think of constructor as CONSTRUCTOR METHOD

        // this is a constructor
        //public Movie()
        //{

        //}

        // this is also a constructor
        public Movie(string aTitle, string aCategory, int aRuntime, int aYear, int aMetacritic)
        {
            Title = aTitle;
            Category = aCategory;
            Runtime = aRuntime;
            Year = aYear;
            Metacritic = aMetacritic;
        }

        // these are properties, they are basically methods, but specific to getters and setters

        //public string Title
        //{
        //    get { return title; }
        //    set { title = value; }

        //}

        //public string Category
        //{
        //    get { return category; }
        //    set { category = value; }
        //}

        public string Title { get; set; }

        public string Category { get; set; }

        public int Runtime { get; set; }

        public int Year { get; set; }

        public int Metacritic
        {
            get { return metacritic; }
            set
            {
                if (value >= 0 && value <= 100)
                {
                    metacritic = value;
                }
                else
                {
                    metacritic = 0;
                }
            }
        }

    }

    class Program
    {
        static string UpdateUserCat(string userCat, List<string> catList)
        {
            int inputLenth = userCat.Length;

            Regex rx = new Regex(@"[a-zA-Z]+\s[a-zA-Z]+");

            int result;
            bool isInt = Int32.TryParse(userCat, out result);

            if (isInt && result >= 1 && result <= catList.Count)
            {
                userCat = catList[result - 1];
            }
            else if (rx.IsMatch(userCat))
            {
                // could have split here

                int spaceIndex = userCat.IndexOf(" ");
                string firstWord = userCat.Substring(0, spaceIndex);
                string secondWord = userCat.Substring(spaceIndex + 1, userCat.Length - spaceIndex - 1);
                firstWord = firstWord[0].ToString().ToUpper() + firstWord.Substring(1, firstWord.Length - 1).ToLower();
                secondWord = secondWord[0].ToString().ToUpper() + secondWord.Substring(1, secondWord.Length - 1).ToLower();

                // Console.WriteLine(firstWord);
                // Console.WriteLine(secondWord);
                userCat = firstWord + " " + secondWord;

            }
            else if (userCat.Length > 1)
            {
                userCat = userCat[0].ToString().ToUpper() + userCat.Substring(1, userCat.Length - 1).ToLower();
            }
            return userCat;

            //Console.WriteLine(userCat);
        }

        static int GetLongest(List<string> movieNames)
        {
            int longest = 0;

            foreach (string movie in movieNames)
            {
                if (movie.Length > longest)
                {
                    longest = movie.Length;
                }
            }
            return longest;
        }

        static bool Continue()
        {
            bool isValid = false;
            while (!isValid)
            {

                Console.Write("\nWould you like to continue? (y/n)  ");
                string ans = Console.ReadLine().ToLower();
                if (ans == "y" || ans == "n")
                {
                    isValid = true;
                    if (ans == "n")
                    {
                        Console.Clear();
                        return true;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid entry. Please try again.");
                }

            }
            return false;
        }

        public static List<string> putCatsInList(List<Movie> movieList)
        {
            List<string> catList = new List<string>();

            foreach (Movie movie in movieList)
            {
                if (!catList.Contains(movie.Category))
                {
                    catList.Add(movie.Category);
                }
            }
            return catList;
        }

        public static void DisplayCats(List<string> catList)
        {
            for (int i = 0; i < catList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {catList[i]}");
            }
        }

        static void PrintOutAll(string userCat, List<Movie> movieList)
        {
            List<string> movieNames = new List<string>();
            foreach (Movie movie in movieList)
            {
                if (movie.Category == userCat)
                {
                    movieNames.Add(movie.Title);
                    //Console.WriteLine(movie.Title);
                }
            }

            movieNames.Sort();
            List<Movie> sortedMovies = new List<Movie>();

            // need to create a new list with just the objects of the category selected

            foreach (string movName in movieNames)
            {
                foreach (Movie mov in movieList)
                {
                    if (mov.Title == movName)
                    {
                        sortedMovies.Add(mov);
                    }
                }
            }

            int titleSpacing = GetLongest(movieNames) + 5;

            string catTitle = userCat.ToUpper() + " MOVIES";
            catTitle = String.Format("{0, " + (-1 * titleSpacing) + "}", catTitle);

            Console.WriteLine($"\n{catTitle}{"RUNTIME",10}{"YEAR",6}{"RATING",8}");


            // need to replace this with looping through sortedMovies
            foreach (Movie mov in sortedMovies)
            {
                string titlePintout = String.Format("{0, " + (-1 * titleSpacing) + "}", mov.Title);
                string runtimePrintout = $"{mov.Runtime} mins";
                string yearPrintout = $"{mov.Year}";

                Console.WriteLine($"{titlePintout}{runtimePrintout,10}{mov.Year,6}{mov.Metacritic,8}");
            }
            Console.WriteLine();
        }

        static void Goodbye()
        {
            string goodbye = "Goodbye! Have a beautiful time!";
            //string good
            int halfWindWidth = Console.WindowWidth / 2;
            int halfGoodbye = goodbye.Length / 2;
            int spacing = halfWindWidth + halfGoodbye;
            string goodbyeStr = String.Format("{0, " + spacing + "}", goodbye);
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine();
            }
            Console.WriteLine($"{goodbyeStr}");
            for (int i = 0; i < 15; i++)
            {
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            // Application stored a list of at least 10 movies and displays them by category
            // The user can enter any of the following categories to display the films in the list that match the category
            // Categories: animated, drama, horror, science fiction, comedy, fantasy

            List<Movie> movieList = new List<Movie>
            {
                new Movie("Harry Potter and the Sorcerer's Stone", "Fantasy", 152, 2001, 64),
                new Movie("Harry Potter and the Chamber of Secrets", "Fantasy", 161, 2002, 63),
                new Movie("Harry Potter and the Prisoner of Azkaban", "Fantasy", 141, 2004, 82),
                new Movie("Interstellar", "Sci Fi", 169 ,2014, 74),
                new Movie("Step Brothers", "Comedy", 98, 2008, 51),
                new Movie("Shawshank Redemption", "Drama", 142, 1994, 81),
                new Movie("Saw", "Horror", 103, 2004, 46),
                new Movie("The Lion King", "Animated", 89, 1994, 88),
                new Movie("The Incredibles", "Animated", 115, 2004, 90),
                new Movie("Citizen Kane", "Drama",119, 1941, 100),
                new Movie("Edge of Tomorrow", "Sci Fi", 113, 2014, 71),
                new Movie("Snowpiercer", "Sci Fi", 126, 2014, 84),
                new Movie("Annihilation", "Sci Fi", 115, 2018, 79),
                new Movie("Blade Runner 2049", "Sci Fi", 164,2017,81),
                new Movie("Superbad", "Comedy", 113, 2007, 76),
                new Movie("Hot Fuzz", "Comedy", 121, 2007, 81),
                new Movie("The Hangover", "Comedy", 100, 2009, 73),
                new Movie("Anchorman: The Legend of Ron Burgundy", "Comedy", 94, 2004, 94)
            };

            Console.WriteLine("Welcome to the Movie List Application!\n");
            List<string> catList = putCatsInList(movieList);

            bool done = false;
            while (!done)
            {

                bool valid = false;
                while (!valid)
                {

                    Console.WriteLine("Here are the categories to select from: ");
                    DisplayCats(catList);


                    Console.WriteLine($"\nThere are {movieList.Count} movies in our collection.");
                    Console.Write("What category are you interested in?:  ");
                    string userCat = Console.ReadLine();

                    userCat = UpdateUserCat(userCat, catList);
                    if (catList.Contains(userCat))
                    {
                        valid = true;
                        PrintOutAll(userCat, movieList);                        
                    }
                    else
                    {
                        Console.Write("Ivalid category. Try again in...");
                        Thread.Sleep(1000);
                        for (int i = 3; i >= 1; i--)
                        {
                            Console.Write($"{i} ");
                            Thread.Sleep(1000);
                        }
                        Console.Clear();
                    }
                }
                done = Continue();
                Console.Clear();
            }
            Goodbye();
        }
    }
}

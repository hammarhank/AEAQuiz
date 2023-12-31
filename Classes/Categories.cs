﻿namespace AEAQuiz.Classes
{
    public class Categories
    {
        private Dictionary<string, string> _category = new Dictionary<string, string>()
        {
            { "any", "Any Category" },
            { "9", "General Knowledge" },
            { "10", "Entertainment: Books" },
            { "11", "Entertainment: Film" },
            { "12", "Entertainment: Music" },
            { "13", "Entertainment: Musicals & Theatres" },
            { "14", "Entertainment: Television" },
            { "15", "Entertainment: Video Games" },
            { "16", "Entertainment: Board Games" },
            { "17", "Science & Nature" },
            { "18", "Science: Computers" },
            { "19", "Science: Mathematics" },
            { "20", "Mythology" },
            { "21", "Sports" },
            { "22", "Geography" },
            { "23", "History" },
            { "24", "Politics" },
            { "25", "Art" },
            { "26", "Celebrities" },
            { "27", "Animals" },
            { "28", "Vehicles" },
            { "29", "Entertainment: Comics" },
            { "30", "Science: Gadgets" },
            { "31", "Entertainment: Japanese Anime & Manga" },
            { "32", "Entertainment: Cartoon & Animations" },
        };

        public List<string> CategorysToList { get { return _category.Values.ToList(); } }

        public static int GetCategoryId(int index)
        {
            string ret = new Categories()._category.Keys.ToList()[index];
            if (ret == "any")
            {
                return 0;
            }
            return int.Parse(ret);
        }
        public static int GetCategoryIdByName(string categoryName)
        {
            var categories = new Categories()._category;
            var key = categories.FirstOrDefault(x => x.Value == categoryName).Key;

            if (key == "any" || string.IsNullOrEmpty(key))
            {
                return 0;
            }

            return int.Parse(key);
        }

    }
}


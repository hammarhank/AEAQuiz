using AEAQuiz.Classes;
using System.Net;

namespace AEAQuiz.Pages
{
    public partial class GamePage : ContentPage
    {
        private Quiz quiz;

        private int numberOfQuestions;

        private int currentIndex = 0;

        private List<Button> buttonsToDelete;

        public GamePage()
        {
            InitializeComponent();

            numberOfQuestions = AppSettings.NumQuestionsSelected;

            _ = LoadTriviaQuestion();
        }


        private async Task LoadTriviaQuestion()
        {
            int? catId = null;
            if (AppSettings.CategorySelected != 0) catId = Categories.GetCategoryId(AppSettings.CategorySelected);
            // TODO: Spara token i Preferences.Default.Set() och ett datum som kontrolleras f�rnyas efter 6 timmar 
            // TODO: Create �r ett ganksa vilseledande namn. Det borde vara Fetch() eller Get() eller likande
            ImageSource old = questionImage.Source;
            questionImage.Source = ImageSource.FromFile("loading_spinner.gif");
            quiz = await Quiz.Create(
                catId,
                (QType)AppSettings.TypeSelected,
                (Difficulty)AppSettings.DifficultySelected,
                numberOfQuestions,
                await Token.Get());
            questionImage.Source = old;
            NextQuastion();
        }


        private void NextQuastion()
        {
            if (quiz.Results.Count > 0 && currentIndex < quiz.Results.Count)
            {
                // TODO: N�gon slags timer b�r startas h�r s� att anv�ndaren inte har f�r l�ng bet�nke tid
                // EXEMPEL: 
                /*if (AppSettings.UseTimerToThink)
                {
                    int sec = AppSettings.TimeToThinkSeconds;
                    var myTimer = new Timer((e) =>
                    {
                        TimerLable.Text = TimeSpan.FromSeconds(sec).ToString("mm':'ss");
                        sec--;
                    }, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
                }*/


                //questonLabel.Text = quiz.Results[currentIndex].Question;
                questonLabel.Text = WebUtility.HtmlDecode(quiz.Results[currentIndex].Question);

                var answers = new List<string>
                {
                    quiz.Results[currentIndex].CorrectAnswer
                };
                answers.AddRange(quiz.Results[currentIndex].IncorrectAnswers);


                buttonsToDelete = new List<Button>();
                Button btn;
                Random r = new Random();
                foreach (string answer in answers.OrderBy(x => r.Next()))
                {
                    btn = new Button();
                    btn.Text = WebUtility.HtmlDecode(answer);
                    btn.Clicked += OnAnswerButtonClicked;
                    StackLayoutQ.Add(btn);
                    buttonsToDelete.Add(btn);
                }

            }


        }

        private void OnAnswerButtonClicked(object sender, EventArgs e)
        {
            var selectedButton = sender as Button;
            if (selectedButton != null)
            {
                bool isCorrect = CheckAnswer(selectedButton.Text);
                if (isCorrect)
                {
                    // TODO: Hantera r�tt svar
                    // EXEMPEL: results.CorrectAnswer(userId, questionId);
                }
                else
                {
                    // TODO: Hantera fel svar
                    // EXEMPEL: results.IncorrectAnswer(userId, questionId, (answer: default = "F you"));
                }

                if (currentIndex < quiz.Results.Count - 1)
                {
                    currentIndex++;
                    buttonsToDelete.ForEach(x => { StackLayoutQ.Remove(x); });
                    NextQuastion();
                }
                else
                {
                    DebugLabel.Text = "Vann jag???? Vad fick jag f�r resultat???? Hall�.... svara d�!!!!!";
                    // TODO: Hantera n�r fr�gorna �r slut
                    // EXAMPLE: if (numberOfQuestions <= 0) results.Save() och sedan skicka anv�ndaren tillbaka till en resultat sida
                    // med typ: await Navigation.PushAsync(new ResaultPage(results)); eller liknande
                }
            }
        }

        private bool CheckAnswer(string answer)
        {
            return answer == quiz.Results[currentIndex].CorrectAnswer;
        }
    }
}



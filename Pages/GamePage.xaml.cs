using AEAQuiz.Classes;
using System.Diagnostics;
using System.Timers;

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

            LoadTriviaQuestion();
        }


        private async Task LoadTriviaQuestion()
        {
            int? catId = null;
            if (AppSettings.CategorySelected != 0) catId = Categories.GetCategoryId(AppSettings.CategorySelected);
            // TODO: Spara token i Preferences.Default.Set() och ett datum som kontrolleras f�rnyas efter 6 timmar 
            // TODO: Create �r ett ganksa vilseledande namn. Det borde vara Fetch() eller Get() eller likande
            quiz = await Quiz.Create(
                catId,
                (QType)AppSettings.TypeSelected,
                (Difficulty)AppSettings.DifficultySelected,
                numberOfQuestions,
                await Token.Get());

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
                    var timer = new System.Timers.Timer();
                    timer.Interval = 1000;
                    timer.Elapsed += (object sender, ElapsedEventArgs e) => 
                    { 
                        TimerLable.Text = TimeSpan.FromSeconds(sec).ToString("mm':'ss");
                        sec--;
                    };
                }*/

                questonLabel.Text = quiz.Results[currentIndex].Question;
            
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
                    btn.Text = answer;
                    btn.Clicked += OnAnswerButtonClicked;
                    StackLayoutQ.Add(btn);
                    buttonsToDelete.Add(btn);
                }

            }

            //Svar i samma labels /Amir
            //Random r = new Random();
            //answers = answers.OrderBy(x => r.Next()).ToList();

            // Uppdatera knapparnas texter ist�llet f�r att skapa knappar

            //answerButton1.Text = answers[0];
            //answerButton2.Text = answers[1];
            //answerButton3.Text = answers[2];
            //answerButton4.Text = answers[3];
            //answerButton1.Clicked += OnAnswerButtonClicked;
            //answerButton2.Clicked += OnAnswerButtonClicked;
            //answerButton3.Clicked += OnAnswerButtonClicked;
            //answerButton4.Clicked += OnAnswerButtonClicked;

            //BUG: Vid val av type true/false s� laddas inte fr�gorna alls! VIKTIGT
            //S�h�r har jag t�nkt att man ska l�sa knapparna beroende p� vilken sorts fr�ga det �r
            ///////////////////////////////////////////////////////////////////////////////////////
            /*if (quiz.Results[numberOfQuestions - 1].Type == "boolean")
            {
                //answers.Add("True");
                //answers.Add("False");

                answerButton1.Text = answers[0];
                answerButton2.Text = answers[1];
                answerButton1.Clicked += OnAnswerButtonClicked;
                answerButton2.Clicked += OnAnswerButtonClicked;

                // D�lj de andra knapparna
                answerButton3.IsVisible = false;
                answerButton4.IsVisible = false;
            }
            else // multiple choice
            {
              //  answers.Add(quiz.Results[numberOfQuestions - 1].CorrectAnswer);
              //  answers.AddRange(quiz.Results[numberOfQuestions - 1].IncorrectAnswers);

                Random r = new Random();
                answers = answers.OrderBy(x => r.Next()).ToList();

                answerButton1.Text = answers[0];
                answerButton2.Text = answers[1];
                answerButton3.Text = answers[2];
                answerButton4.Text = answers[3];

                answerButton1.Clicked += OnAnswerButtonClicked;
                answerButton2.Clicked += OnAnswerButtonClicked;
                answerButton3.Clicked += OnAnswerButtonClicked;
                answerButton4.Clicked += OnAnswerButtonClicked;

                // Visa alla knappar
                answerButton3.IsVisible = true;
                answerButton4.IsVisible = true;
            }*/
            ///////////////////////////////////////////////////////////////////////////////////////
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



namespace AEAQuiz.Pages;


public partial class GamePage : ContentPage
{
    public GamePage()
    {
        InitializeComponent();

        // TODO: Anropa metoden f�r att h�mta fr�ga och svar fr�n API n�r sidan laddas
        LoadTriviaQuestion();
    }

    private async void LoadTriviaQuestion()
    {
        // TODO: H�mta data fr�n API
        // Exempel:
        // var triviaData = await apiService.GetTriviaData();

        // TODO: Uppdatera questonLabel.Text med den h�mtade fr�gan
        // TODO: Uppdatera svarsknapparnas texter med de h�mtade svaren

        // Binda h�ndelsehanterare till knapparna f�r att hantera svar
        answerButton1.Clicked += OnAnswerButtonClicked;
        answerButton2.Clicked += OnAnswerButtonClicked;
        answerButton3.Clicked += OnAnswerButtonClicked;
        answerButton4.Clicked += OnAnswerButtonClicked;
    }

    private void OnAnswerButtonClicked(object sender, EventArgs e)
    {
        var selectedButton = sender as Button;
        if (selectedButton != null)
        {
            // TODO: R�tta den svarade fr�gan baserat p� vilken knapp som klickades
            // Exempel:
            // bool isCorrect = CheckAnswer(selectedButton.Text);
            // if (isCorrect) { /* Hantera r�tt svar */ }
            // else { /* Hantera fel svar */ }
        }
    }
}
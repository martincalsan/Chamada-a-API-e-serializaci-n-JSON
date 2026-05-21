using System;

[Serializable]
public class TriviaQuestion
{
    public string type;
    public string difficulty;
    public string category;
    public string question;
    public string correct_answer;
    public string[] incorrect_answers;
}
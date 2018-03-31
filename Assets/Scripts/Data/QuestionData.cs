[System.Serializable]
public class QuestionData
{
    public string index;
    public string infoText;
    public string questionText;
    public AnswerData[] answers;

    public bool wasAnswered = false;
    public float timeUsed = 0;
    public int mistakes = 0;
}
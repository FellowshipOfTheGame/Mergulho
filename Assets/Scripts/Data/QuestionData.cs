[System.Serializable]
public class QuestionData
{
    public string index;
    public string infoText;
    public string questionText;
    public AnswerData[] answers;

    public bool wasAnswered;
    public float timeUsed;
    public int mistakes;
    public int questionScore;
}
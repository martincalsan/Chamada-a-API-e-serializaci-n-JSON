using System;

[Serializable]
public class TriviaResponse
{
    public int response_code;
    public TriviaQuestion[] results;
}
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

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

[Serializable]
public class TriviaResponse
{
    public int response_code;
    public TriviaQuestion[] results;
}

public class TriviaAPIManager : MonoBehaviour
{
    public TriviaResponse datos;

    void Start()
    {
        StartCoroutine(ObtenerPreguntas());
    }

    IEnumerator ObtenerPreguntas()
    {
        using (UnityWebRequest request = UnityWebRequest.Get("https://opentdb.com/api.php?amount=10"))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
                yield break;
            }

            datos = JsonUtility.FromJson<TriviaResponse>(request.downloadHandler.text);

            foreach (TriviaQuestion q in datos.results)
            {
                q.question       = System.Net.WebUtility.HtmlDecode(q.question);
                q.correct_answer = System.Net.WebUtility.HtmlDecode(q.correct_answer);
                q.category       = System.Net.WebUtility.HtmlDecode(q.category);
            }

            Debug.Log("Preguntas recibidas: " + datos.results.Length);
            Debug.Log("Categoría: "          + datos.results[0].category);
            Debug.Log("Dificultade: "         + datos.results[0].difficulty);
            Debug.Log("Pregunta: "           + datos.results[0].question);
            Debug.Log("Resposta correcta: " + datos.results[0].correct_answer);
        }
    }
}
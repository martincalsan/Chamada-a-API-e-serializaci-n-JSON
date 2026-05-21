using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

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
            Debug.Log("Dificultad: "         + datos.results[0].difficulty);
            Debug.Log("Pregunta: "           + datos.results[0].question);
            Debug.Log("Respuesta correcta: " + datos.results[0].correct_answer);
        }
    }
}
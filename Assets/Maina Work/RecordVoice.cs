using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class RecordVoice : MonoBehaviour
{
    int pepe = 0;
    KeywordRecognizer keywordRecognizer;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
    public AudioSource pepperone;
    public DictationRecognizer dictationRecognizer;
    string testo = "";

    // Start is called before the first frame update
    void Start()
    {
        print("Piselloide");
        //Create keywords for keyword recognizer
        /* keywords.Add("link start", () =>
         {
             powerRanger();
         });
         keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());
         keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;

         keywordRecognizer.Start(); */




        //dictationRecognizer = new DictationRecognizer();
        //dictationRecognizer.DictationResult += DictationRecognizer_DictationResult;

        /*dictationRecognizer.DictationHypothesis += DictationRecognizer_DictationHypothesis;
        dictationRecognizer.DictationComplete += DictationRecognizer_DictationComplete;
        dictationRecognizer.DictationError += DictationRecognizer_DictationError;

        dictationRecognizer.DictationResult -= DictationRecognizer_DictationResult;
        dictationRecognizer.DictationComplete -= DictationRecognizer_DictationComplete;
        dictationRecognizer.DictationHypothesis -= DictationRecognizer_DictationHypothesis;
        dictationRecognizer.DictationError -= DictationRecognizer_DictationError;
        dictationRecognizer.Dispose(); */


    }

    private void DictationRecognizer_DictationResult(string text, ConfidenceLevel confidence)
    {
        /* print("RISUTLATO:");
         print(text);
         print(confidence); */
        testo = testo + text;
    }

    private void DictationRecognizer_DictationHypothesis(string text)
    {
        // print("RISUTLATO:");
        //  print(text);
    }

    private void DictationRecognizer_DictationComplete(DictationCompletionCause cause)
    {
        // do something
    }

    private void DictationRecognizer_DictationError(string error, int hresult)
    {
        // do something
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        // if the keyword recognized is in our dictionary, call that Action.
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }

    private void recordStart()
    {
        dictationRecognizer = new DictationRecognizer();
        dictationRecognizer.DictationResult += DictationRecognizer_DictationResult;
        dictationRecognizer.DictationHypothesis += DictationRecognizer_DictationHypothesis;
        dictationRecognizer.DictationComplete += DictationRecognizer_DictationComplete;
        dictationRecognizer.DictationError += DictationRecognizer_DictationError;
        dictationRecognizer.Start();
        pepe = 1;
    }

    private void recordEnd()
    {
        dictationRecognizer.DictationResult -= DictationRecognizer_DictationResult;
        dictationRecognizer.DictationComplete -= DictationRecognizer_DictationComplete;
        dictationRecognizer.DictationHypothesis -= DictationRecognizer_DictationHypothesis;
        dictationRecognizer.DictationError -= DictationRecognizer_DictationError;
        dictationRecognizer.Stop();
        dictationRecognizer.Dispose();
        print("il risultato è :" + testo);
        testo = "";
        pepe = 0;
    }


    /*void powerRanger()
    {
        pepperone.Play();
        print("NANANA NAAAAAAA NANA NAAAAANAAAAAAAAAA");
    } */

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if (pepe == 0)
            {
                recordStart();
            }
            else
            {
                recordEnd();
            }
        }
    }
}

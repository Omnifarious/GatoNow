using System;
using System.Net;
using System.IO;
using System.Collections;
using UnityEngine;
using System.Threading;
using UnityEngine.Networking;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

public class GetTranslatedText : MonoBehaviour {

    string token = "__TOKEN__";
    [SerializeField]
    private string text = "table";
    string translation;

    // Use this for initialization
    void Start()
    {
    }

    void GetTranslation()
    {
        if (token != null && text != null)
        {
            Debug.Log("Start get translation");
            ServicePointManager.ServerCertificateValidationCallback = MyRemoteCertificateValidationCallback;

            const string WEBSERVICE_URL = "https://api.microsofttranslator.com/v2/http.svc/Translate?text=";
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(WEBSERVICE_URL + text + "&to=es");
            Debug.Log(webRequest.RequestUri);
            if (webRequest != null)
            {
                webRequest.Method = "POST";
                webRequest.Accept = "application/xml";
                webRequest.Headers["Authorization"] = "Bearer " + token;

                webRequest.BeginGetRequestStream(new AsyncCallback(GetTranslateRequestStreamCallback), webRequest);
            }
        }
        else if (token == null)
        {
            GetToken();
        }
    }

    private void GetTranslateRequestStreamCallback(IAsyncResult asynchronousResult)
    {
        Debug.Log("GetTranslateRequestStreamCallback");
        HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;
        request.BeginGetResponse(new AsyncCallback(GetTranslateResponseCallback), request);
    }

    private void GetTranslateResponseCallback(IAsyncResult asynchronousResult)
    {
        Debug.Log("GetTranslateCallback");
        HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;

        // End the operation
        HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asynchronousResult);
        Stream streamResponse = response.GetResponseStream();
        StreamReader streamRead = new StreamReader(streamResponse);
        string responseString = streamRead.ReadToEnd();
        translation = responseString;
        Debug.Log("translation: " + translation);
        // Close the stream object
        streamResponse.Close();
        streamRead.Close();

        // Release the HttpWebResponse
        response.Close();
    }
    
    void GetToken()
    {
        Debug.Log("Start get token");
        ServicePointManager.ServerCertificateValidationCallback = MyRemoteCertificateValidationCallback;

        const string WEBSERVICE_URL = "https://api.cognitive.microsoft.com/sts/v1.0/issueToken";
        HttpWebRequest webRequest = (HttpWebRequest) WebRequest.Create(WEBSERVICE_URL);

        if (webRequest != null)
        {
            webRequest.Method = "POST";
            webRequest.ContentType = "application/json";
            webRequest.Accept = "application/jwt";
            webRequest.ContentLength = 0;
            webRequest.Headers["Ocp-Apim-Subscription-Key"] = "__KEY__";

            webRequest.BeginGetRequestStream(new AsyncCallback(GetTokenRequestStreamCallback), webRequest);
        }
    }
    private void GetTokenRequestStreamCallback(IAsyncResult asynchronousResult)
    {
        HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;
        request.BeginGetResponse(new AsyncCallback(GetTokenResponseCallback), request);
    }

    private void GetTokenResponseCallback(IAsyncResult asynchronousResult)
    {
        HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;

        // End the operation
        HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asynchronousResult);
        Stream streamResponse = response.GetResponseStream();
        StreamReader streamRead = new StreamReader(streamResponse);
        string responseString = streamRead.ReadToEnd();
        token = responseString;
        Debug.Log(token);
        // Close the stream object
        streamResponse.Close();
        streamRead.Close();

        // Release the HttpWebResponse
        response.Close();
    }

    public bool MyRemoteCertificateValidationCallback(System.Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
        bool isOk = true;
        // If there are errors in the certificate chain, look at each error to determine the cause.
        if (sslPolicyErrors != SslPolicyErrors.None)
        {
            for (int i = 0; i < chain.ChainStatus.Length; i++)
            {
                if (chain.ChainStatus[i].Status != X509ChainStatusFlags.RevocationStatusUnknown)
                {
                    chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
                    chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
                    chain.ChainPolicy.UrlRetrievalTimeout = new TimeSpan(0, 1, 0);
                    chain.ChainPolicy.VerificationFlags = X509VerificationFlags.AllFlags;
                    bool chainIsValid = chain.Build((X509Certificate2)certificate);
                    if (!chainIsValid)
                    {
                        isOk = false;
                    }
                }
            }
        }
        return isOk;
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("Start");
            GetTranslation();
        }
    }
}

using Newtonsoft.Json;
using RestSharp;
using SimpleFramework.Models;
using System;
using System.Configuration;


namespace SimpleFramework.Helpers
{
    public class RestHelper
    {
        public IRestResponse GetRequest(string endpoint)
        {
            var client = new RestClient(/*url*/);
            var request = new RestRequest(endpoint, Method.GET);

            /* Add necessary headers depending on site */
            // request.AddHeader("Authorization", settings.Token)
            request.AddHeader("Accept", "application/json");

            var response = client.Execute(request);
            return response;
        }

        public IRestResponse CreateUpdateRequest(Method method, string endpoint, dynamic payload)
        {
            var client = new RestClient(/*Url*/);
            RestRequest request = new RestRequest(endpoint, method);

            /* Depends on the defined format */
            //request.AddHeader("Authorization", token);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");

            request.AddJsonBody(payload);

            var response = client.Execute(request);
            return response;
        }

        //overload = with Token
        public IRestResponse CreateUpdateRequest(Method method, string endpoint, dynamic payload, string token)
        {
            var client = new RestClient(/*Url*/);
            RestRequest request = new RestRequest(endpoint, method);

            /* Depends on the defined format */
            //request.AddHeader("Authorization", token);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Cookie", "token=abc123");

            request.AddJsonBody(payload);

            var response = client.Execute(request);
            return response;
        }

        public string GetToken(string url, string user, string password1)
        { 
            var client = new RestClient($"{url}/auth");
            RestRequest request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new { username = user, password = password1});

            IRestResponse response = client.Execute(request);
            var tokenModel = JsonConvert.DeserializeObject<Token>(response.Content);

            if (tokenModel.token == null)
            {
                throw new ArgumentException("Error: " + tokenModel.reason);
            }
            else
            {
                return tokenModel.token;
            }
        }

        public T Deserialize<T>(IRestResponse response)
        {
            return JsonConvert.DeserializeObject<T>(response.Content);
        }
    }
}

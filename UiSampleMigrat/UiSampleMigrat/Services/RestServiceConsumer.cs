using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UiSampleMigrat.Models;
using Xamarin.Essentials;

namespace UiSampleMigrat.Services
{
    public class RestServiceConsumer
    {
        /*Metodo para chequear la conexion*/
        public Response CheckConnection()
        {
            Response returned = new Response();
            returned.IsSuccesFull = false;
            returned.Message = "Conexion No Disponible";
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet) {
                //Conexion a internet disponible -Acceso al servicio no garantizado-
                returned.IsSuccesFull = true;
                returned.Message = "Conexion Disponible";
                return returned;
            }
            return returned;
        }

        /*Metodo generico para consumir verbos GET*/
        public async Task<Response> Get<T>(string baseUrl, string prefix, string controller)
        {

            HttpClient client = new HttpClient();

            try
            {
                client.BaseAddress = new Uri(baseUrl);
                var url = $"{prefix}{controller}";
                var response = await client.GetAsync(url);
                var answer = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccesFull = false,
                        Message = answer,
                    };
                }

                var objects = JsonConvert.DeserializeObject<T>(answer);

                return new Response
                {
                    IsSuccesFull = true,
                    Result = objects,
                };

            }
            catch (Exception ex)
            {

                return new Response
                {
                    IsSuccesFull = false,
                    Message = ex.Message,
                };
            }

        }
        /*Metodo generico para consumir verbos GET que piden TOKEN*/
        public async Task<Response> Get<T>(string baseUrl, string prefix, string controller, string token)
        {

            HttpClient client = new HttpClient();

            try
            {

                client.DefaultRequestHeaders.Clear();
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", string.Format("Bearer {0}", CleanToken(token)));
                var url = $"{prefix}{controller}";
                var response = await client.GetAsync(url);
                var answer = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccesFull = false,
                        Message = answer,
                    };
                }

                var objects = JsonConvert.DeserializeObject<T>(answer);

                return new Response
                {
                    IsSuccesFull = true,
                    Result = objects,
                };

            }
            catch (Exception ex)
            {

                return new Response
                {
                    IsSuccesFull = false,
                    Message = ex.Message,
                };
            }

        }
        /*Metodo generico para consumir verbos POST*/
        public async Task<Response> Post<T>(string baseUrl, string prefix, string controller, T model)
        {
            HttpClient client = new HttpClient();

            try
            {
                client.BaseAddress = new Uri(baseUrl);
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var url = $"{prefix}{controller}";
                var response = await client.PostAsync(url, content);
                var answer = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccesFull = false,
                        Message = answer,
                    };
                }

                var objects = JsonConvert.DeserializeObject<T>(answer);

                return new Response
                {
                    IsSuccesFull = true,
                    Result = objects,
                };

            }
            catch (Exception ex)
            {

                return new Response
                {
                    IsSuccesFull = false,
                    Message = ex.Message,
                };
            }


        }
        /*Metodo generico para consumir verbos PUT que requieren TOKEN*/
        public async Task<Response> Put<T>(string baseUrl, string prefix, string controller, T model, string token) {
            HttpClient client = new HttpClient();
            try {


                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", string.Format("Bearer {0}", CleanToken(token)));
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var url = $"{prefix}{controller}";
                var response = await client.PutAsync(url, content);
                var answer = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccesFull = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                var objects = JsonConvert.DeserializeObject<T>(answer);

                return new Response
                {
                    IsSuccesFull = true,
                    Result = objects,
                };
            }
            catch (Exception ex) {
                return new Response
                {
                    IsSuccesFull = false,
                    Message = ex.Message,
                };
            }

        }





        /*Metodos para autenticacion y obtencion de informacion con token*/
        public async Task<T> GetAuth<T>(string url, string token)
        {
            HttpClient client = new HttpClient();
            try
            {

                var cleanToken = token.Replace('"', ' ');
                var cleanedToken = cleanToken.Trim();
                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", string.Format("Bearer {0}", cleanedToken));

                var response = await client.GetAsync(url);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var stringResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(stringResponse);
                }
                else
                {
                    return default(T);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<string> PostToken(string url, Object content, Type ez)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = null;

            try
            {
                var json = JsonConvert.SerializeObject(Convert.ChangeType(content, ez));
                var contentString = new StringContent(json, Encoding.UTF8, "application/json");

                response = await client.PostAsync(url, contentString);

                if (response.IsSuccessStatusCode)
                {
                    var stringRespond = await response.Content.ReadAsStringAsync();
                    if (stringRespond != string.Empty)
                        return stringRespond;
                    else
                        return string.Empty;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return string.Empty;
        }

    

        private string CleanToken(string token)
        {
            var cleanToken = token.Replace('"', ' ');
            var cleanedToken = cleanToken.Trim();
            return cleanedToken;
        }

    }
}
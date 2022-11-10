using ButlerLabsSDK.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Net.Http.Json;

namespace ButlerLabsSDK
{
    public interface IButlerlabs
    {
        Task<UploadedDocumentResponse?> UploadDocument(string URL, string queueId);
        Task<ExtractionResultsResponse> GetExtractionResults(string uploadId, string queueId);
    }

    public class Butlerlabs : IButlerlabs
    {
        readonly string APIKey;
        public Butlerlabs(string APIKey)
        {
            this.APIKey = APIKey;
        }

        public async Task<UploadedDocumentResponse?> UploadDocument(string URL, string queueId)
        {
            byte[] binaryFile = null;
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(URL))
                using (Stream streamToReadFrom = await response.Content.ReadAsStreamAsync())
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        await streamToReadFrom.CopyToAsync(stream);
                        binaryFile = stream.ToArray();
                    }
                }
            }

            UploadedDocumentResponse? documentResponse;
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://app.butlerlabs.ai/api/queues/" + queueId + "/uploads?extraResults=LineBlocks&extraResults=FormFields&extraResults=Tables"))
                {
                    request.Headers.TryAddWithoutValidation("accept", "*/*");
                    request.Headers.TryAddWithoutValidation("Authorization", "Bearer " + APIKey);

                    var multipartContent = new MultipartFormDataContent();
                    var file1 = new ByteArrayContent(binaryFile);
                    file1.Headers.Add("Content-Type", "application/pdf");
                    multipartContent.Add(file1, "files", Path.GetFileName("Document2.pdf"));
                    request.Content = multipartContent;

                    var response = await httpClient.SendAsync(request);

                    documentResponse = await response.Content.ReadFromJsonAsync<UploadedDocumentResponse>();
                }
            }

            return documentResponse;
        }

        public async Task<ExtractionResultsResponse> GetExtractionResults(string uploadId, string queueId)
        {

            // Specify variables for use in script below 
            var api_base_url = $"https://app.butlerlabs.ai/api/queues/{queueId}";


            // wait for and fetch results
            var client = new RestClient($"{api_base_url}");
            var request = new RestRequest("/extraction_results", Method.Get);

            // Authorization header
            request.AddHeader("Authorization", $"Bearer {APIKey}");
            request.AddHeader("Accept", "application/json");

            // Get extraction results for the specified upload
            request.AddParameter("uploadId", uploadId);

            var response = client.Execute(request);

            return JsonConvert.DeserializeObject<ExtractionResultsResponse>(response.Content);
        }
    }
}
using ButlerLabsSDK;

Console.WriteLine("Starting...");

const string APIKey = "";
const string queueId = "";
const string uploadId = "";


var lab = new Butlerlabs(APIKey);
var response = await lab.GetExtractionResults(uploadId, queueId);
using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AlmoxarifadoSmart.Application.Services.Implemetations.Comunicacao.Whatsapp;

public class WhatsappService
{
    public async Task<bool> SendMensageWhatsapp(string number, string Mensage)
    {
        try
        {
            var parameters = new NameValueCollection();
            var client = new WebClient();

            var url = "https://app.whatsgw.com.br/api/WhatsGw/Send/";

            parameters.Add("apikey", "b3d8ba24-d4ca-492b-afb4-387fd9799c41");
            parameters.Add("phone_number", "5579981125546");
            parameters.Add("contact_phone_number", $"55{number}");
            parameters.Add("message_custom_id", "tste");
            parameters.Add("message_type", "text");
            parameters.Add("message_body", Mensage);

            byte[] response_data = await client.UploadValuesTaskAsync(url, "POST", parameters);
            string responseString = Encoding.UTF8.GetString(response_data);

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"FAIL: {ex.Message}");
            // Log the exception or handle it more gracefully
            return false;
        }
    }
}

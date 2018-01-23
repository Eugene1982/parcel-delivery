using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using OwinSelfHost.Domain;
using OwinSelfHost.Repository;

namespace OwinSelfHost.WebApi
{
    public class ParcelsController : ApiController
    {
        private readonly IDistributeParcels distribute;

        public ParcelsController(IDistributeParcels distribute)
        {
            this.distribute = distribute;
        }

        // POST api/parcels 
        public async Task<HttpResponseMessage> Post(HttpRequestMessage request)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
            try
            {
                Stream contentStream = await request.Content.ReadAsStreamAsync();
                StreamReader reader = new StreamReader(contentStream);

                Parcel[] reply = distribute.Distribute(reader.ReadToEnd());

                string json = await Task.Run(() => JsonConvert.SerializeObject(reply));
                httpResponseMessage.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }
            catch (Exception exception)
            {
                HttpResponseMessage erroResponseMessage =
                    new HttpResponseMessage(HttpStatusCode.InternalServerError)
                    {
                        Content = new StringContent(exception.Message)
                    };
                return erroResponseMessage;
            }
            return httpResponseMessage;
        }

    }
}
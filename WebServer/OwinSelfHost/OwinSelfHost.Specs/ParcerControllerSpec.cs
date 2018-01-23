using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using FluentAssertions;
using OwinSelfHost.Domain;
using OwinSelfHost.Specs.Builders;
using OwinSelfHost.WebApi;
using Xunit;

namespace OwinSelfHost.Specs
{
    public class ParcerControllerSpec
    {
        public class When_distributing_parcels
        {
            private ParcelsController controller;
            private HttpResponseMessage response;
         
            private string xml = @"<Container>
            <Id>68465468</Id>
            <ShippingDate>2016-07-22T00:00:00+02:00</ShippingDate>
            <parcels>
                   <Parcel>
                      <Sender>
                        <Name>Klaas</Name>
                        <Address>
                          <Street>Uranusstraat</Street>
                          <HouseNumber>22</HouseNumber>
                          <PostalCode>2402AE</PostalCode>
                          <City>Alphen a/d Rijn</City>
                        </Address>
                      </Sender>
                      <Receipient>
                        <Name>Piet</Name>
                        <Address>
                           <Street>Schenklaan</Street>
                           <HouseNumber>22</HouseNumber>
                           <PostalCode>2497AV</PostalCode>
                           <City>Den Haag</City>
                       </Address>
                    </Receipient>
                  <Weight>0.02</Weight>
                  <Value>0.0</Value>
                 </Parcel>
            </parcels>
            </Container>
            ";
            public When_distributing_parcels()
            {
                controller = new ParcelsControllerBuilder().WithDepartments(new List<Department>
                {
                    new Department
                    {
                        Name = "Name1",
                        WeightMin = 0,
                        WeightMax = 1
                    }
                }).Build();

                var request = new HttpRequestMessage
                {
                    Content = new StringContent(xml, Encoding.UTF8, "application/xml")
                };

                response = controller.Post(request).Result;
            }

            [Fact]
            public void Then_it_should_return_destributed_parcels()
            {
               string result = response.Content.ReadAsStringAsync().Result;
                result.Should()
                    .Be("[{\"Weight\":0.02,\"Price\":0.0,\"From\":{\"Name\":\"Klaas\",\"Street\":\"Uranusstraat\",\"HouseNumber\":22,\"City\":\"Alphen a/d Rijn\",\"PostalCode\":\"2402AE\"},\"To\":{\"Name\":\"Piet\",\"Street\":\"Schenklaan\",\"HouseNumber\":22,\"City\":\"Den Haag\",\"PostalCode\":\"2497AV\"},\"DepartmentName\":\"Name1\"}]");
            }
        }
    }
}
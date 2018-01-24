using System;
using System.Collections.Generic;
using System.Net;
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
                    .Be(
                        "[{\"Weight\":0.02,\"Price\":0.0,\"From\":{\"Name\":\"Klaas\",\"Street\":\"Uranusstraat\",\"HouseNumber\":22,\"City\":\"Alphen a/d Rijn\",\"PostalCode\":\"2402AE\"},\"To\":{\"Name\":\"Piet\",\"Street\":\"Schenklaan\",\"HouseNumber\":22,\"City\":\"Den Haag\",\"PostalCode\":\"2497AV\"},\"DepartmentName\":\"Name1\"}]");
            }
        }

        public class When_distributing_parcels_from_wrong_xml_file
        {
            private ParcelsController controller;
            private HttpResponseMessage response;

            private string xml = @"<Container>
            <books>
            </books>
            </Container>
            ";

            public When_distributing_parcels_from_wrong_xml_file()
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
            public void Then_it_should_throw()
            {
                response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.InternalServerError);
            }
        }

        public class When_distributing_parcels_and_department_are_intersected_in_weight
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
                  <Weight>7</Weight>
                  <Value>0.0</Value>
                 </Parcel>
            </parcels>
            </Container>
            ";

            public When_distributing_parcels_and_department_are_intersected_in_weight()
            {
                controller = new ParcelsControllerBuilder().WithDepartments(new List<Department>
                {
                    new Department
                    {
                        Name = "OldDepartment",
                        WeightMin = 0,
                        WeightMax = 10,
                        CreatedAt = new DateTime(2018, 1, 1)
                    },
                    new Department
                    {
                        Name = "NewDepartment",
                        WeightMin = 5,
                        WeightMax = 20,
                        CreatedAt = new DateTime(2018, 1, 5)
                    }
                }).Build();

                var request = new HttpRequestMessage
                {
                    Content = new StringContent(xml, Encoding.UTF8, "application/xml")
                };

                response = controller.Post(request).Result;
            }

            [Fact]
            public void Then_it_should_distribute_to_newest_department()
            {
                string result = response.Content.ReadAsStringAsync().Result;

                result.Should().Contain("NewDepartment");
                
            }
        }

        public class When_distributing_parcels_that_have_price_and_weight
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
                  <Weight>7</Weight>
                  <Value>200</Value>
                 </Parcel>
            </parcels>
            </Container>
            ";

            public When_distributing_parcels_that_have_price_and_weight()
            {
                controller = new ParcelsControllerBuilder().WithDepartments(new List<Department>
                {
                    new Department
                    {
                        Name = "WeightDepartment",
                        WeightMin = 0,
                        WeightMax = 10
                    },
                    new Department
                    {
                        Name = "PriceDepartment",
                        PriceStart = 100
                    }
                }).Build();

                var request = new HttpRequestMessage
                {
                    Content = new StringContent(xml, Encoding.UTF8, "application/xml")
                };

                response = controller.Post(request).Result;
            }

            [Fact]
            public void Then_price_should_be_a_priority()
            {
                string result = response.Content.ReadAsStringAsync().Result;

                result.Should().Contain("PriceDepartment");
                
            }
        }

        public class When_distributing_parcels_and_department_does_not_exist
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
                  <Weight>7</Weight>
                  <Value>200</Value>
                 </Parcel>
            </parcels>
            </Container>
            ";
            public When_distributing_parcels_and_department_does_not_exist()
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
            public void Then_it_should_throw()
            {
                response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.InternalServerError);
            }
        }
    }
}
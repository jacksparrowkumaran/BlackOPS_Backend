using BlackOPS.Models;
using BlackOPS.Models.PromoLaunch;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace BlackOPS.Repository
{
    public class PromoLaunchRepository
    {
        private string ConnectionString = string.Empty;
        private IOptions<ConfigurationManager> settings;

        public PromoLaunchRepository(IOptions<ConfigurationManager> settings)
        {
            this.settings = settings;
        }

        public List<CountryList> GetCountryList(string prefix)
        {
            List<CountryList> countryList = new List<CountryList>();
            string query = string.Empty;
            using (SqlConnection con = new SqlConnection(this.settings.Value.ConnectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@prefix", prefix);

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        countryList.Add(new CountryList
                        {
                            CountryCode = reader[""].ToString(),
                            CountryName = reader[""].ToString()
                        });
                    }

                }
            }
            return countryList;
        }

        public List<ProductCodeList> GetProductCodeInfo(string prefix)
        {
            List<ProductCodeList> productCodes = new List<ProductCodeList>();
            productCodes.Add(new ProductCodeList
            {
                ProductCode = "3234234",
                ProductDescription = "asdfadf"
            });
            productCodes.Add(new ProductCodeList
            {
                ProductCode = "4564564",
                ProductDescription = "ar"
            });
            return productCodes;

            string query = string.Empty;
            using (SqlConnection con = new SqlConnection(this.settings.Value.ConnectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@prefix", prefix);

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        productCodes.Add(new ProductCodeList
                        {
                            ProductCode = reader[""].ToString(),
                            ProductDescription = reader[""].ToString()
                        });
                    }

                }
            }

            return productCodes;
        }

        public List<PricePlanInfo> GetPricePlanInfo(string prefix)
        {
            List<PricePlanInfo> productCodes = new List<PricePlanInfo>();
            productCodes.Add(new PricePlanInfo
            {
                PricePlanId = 2,
                PricePlanName = "ind"
            });
            productCodes.Add(new PricePlanInfo
            {
                PricePlanId = 1,
                PricePlanName = "england"
            });
            return productCodes;

            List<PricePlanInfo> pricePlanInfos = new List<PricePlanInfo>();
            string query = string.Empty;
            using (SqlConnection con = new SqlConnection(this.settings.Value.ConnectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@prefix", prefix);

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        pricePlanInfos.Add(new PricePlanInfo
                        {
                            PricePlanId = Convert.ToInt32(reader[""]),
                            PricePlanName = reader[""].ToString()
                        });
                    }

                }
            }

            return pricePlanInfos;
        }

        public List<AcitvePromoInfo> GetActivePromoInfo(string productCode, string schemeName, int pricePlanId)
        {
            List<AcitvePromoInfo> productCodes = new List<AcitvePromoInfo>();
            productCodes.Add(new AcitvePromoInfo
            {
                Id = 1,
                ProductCode = "test",
                ProductDesc = "test",
                StartDate = "test",
                EndDate = "test",
                SchemeName = "SchemeName",
                RegularPrice = 1,
                PromoPrice = 1
            });
            productCodes.Add(new AcitvePromoInfo
            { 
                Id = 2,
                ProductCode = "ProductCode2",
                ProductDesc = "ProductDesc3",
                StartDate = "StartDate",
                EndDate = "EndDate",
                SchemeName = "SchemeName2",
                RegularPrice = 3,
                PromoPrice = 3,
            });

            return productCodes;
            List<AcitvePromoInfo> pricePlanInfos = new List<AcitvePromoInfo>();
            string query = string.Empty;
            using (SqlConnection con = new SqlConnection(this.settings.Value.ConnectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@productCode", productCode);
                    command.Parameters.AddWithValue("@schemeName", schemeName);
                    command.Parameters.AddWithValue("@pricePlanId", pricePlanId);

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        pricePlanInfos.Add(new AcitvePromoInfo
                        {
                            Id = Convert.ToInt32(reader[""]),
                            ProductCode = reader[""].ToString(),
                            ProductDesc = reader[""].ToString(),
                            StartDate = reader[""].ToString(),
                            EndDate = reader[""].ToString(),
                            SchemeName = Convert.ToString(reader[""]),
                            RegularPrice = Convert.ToDecimal(reader[""]),
                            PromoPrice = Convert.ToDecimal(reader[""]),
                        });
                    }

                }
            }

            return pricePlanInfos;
        }
    }
}

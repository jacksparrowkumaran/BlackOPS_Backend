using BlackOPS.Models;
using BlackOPS.Models.PromoLaunch;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
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
            string query = "usp_GetActiveProducts";
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
                            CountryCode = reader["usp_GetActiveProducts"].ToString(),
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
            prefix = prefix ?? string.Empty;
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

            string query = "usp_GetActiveProducts";
            using (SqlConnection con = new SqlConnection(this.settings.Value.ConnectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@prefix", prefix);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        productCodes.Add(new ProductCodeList
                        {
                            ProductCode = reader["ProdCode"].ToString(),
                            ProductDescription = reader["ProdName"].ToString()
                        });
                    }

                }
            }

            return productCodes;
        }

        public List<PricePlanInfo> GetPricePlanInfo(string prefix)
        {
            List<PricePlanInfo> productCodes = new List<PricePlanInfo>();
            prefix = prefix ?? string.Empty;
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
            string query = "usp_GetActivePricePlan";
            using (SqlConnection con = new SqlConnection(this.settings.Value.ConnectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@prefix", prefix);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        pricePlanInfos.Add(new PricePlanInfo
                        {
                            PricePlanId = Convert.ToInt32(reader["PricePlanID"]),
                            PricePlanName = reader["Description"].ToString()
                        });
                    }

                }
            }

            return pricePlanInfos;
        }

        public List<AcitvePromoInfo> GetActivePromoInfo(SearchPromo searchPromo)
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
            string query = "usp_GetActivePromoBasedonProdCode";
            using (SqlConnection con = new SqlConnection(this.settings.Value.ConnectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@productCode", searchPromo.ProductCode);
                    command.Parameters.AddWithValue("@schemeName", searchPromo.PriceScheme);
                    command.Parameters.AddWithValue("@pricePlanId", searchPromo.PricePlanId);

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

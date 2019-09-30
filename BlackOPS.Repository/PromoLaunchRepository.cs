using BlackOPS.Interface.Promotion.Repositories;
using BlackOPS.Models;
using BlackOPS.Models.PromoLaunch;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace BlackOPS.Repository
{
    public class PromoLaunchRepository : IPromoLaunchRepository
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
            prefix = prefix ?? string.Empty;
            string query = "dbo.usp_GetCountryList";
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
                        countryList.Add(new CountryList
                        {
                            CountryCode =  reader["CountryCode"].ToString(),
                            CountryName = Convert.ToString(reader["CountryName"])
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
            prefix = prefix ?? string.Empty;

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
            List<AcitvePromoInfo> pricePlanInfos = new List<AcitvePromoInfo>();
            string query = "dbo.usp_GetActivePromoBasedonProdCode";
            using (SqlConnection con = new SqlConnection(this.settings.Value.GQNet))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ProductCode", searchPromo.ProductCode);
                    command.Parameters.AddWithValue("@PriceSchemeName", searchPromo.PriceScheme);
                    command.Parameters.AddWithValue("@PricePlanID", searchPromo.PricePlanId);

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        pricePlanInfos.Add(new AcitvePromoInfo
                        {
                            PriceSchemeId = Convert.ToInt32(reader["PriceSchemeID"]),
                            ProductCode = reader["ProdCode"].ToString(),
                            ProductDesc = Convert.ToString(reader["ProdName"]),
                            StartDate = Convert.ToDateTime(reader["StartDate"]).ToString("dd MMM yyyy hh:mm tt"),
                            EndDate = Convert.ToDateTime(reader["EndDate"]).ToString("dd MMM yyyy hh:mm tt"),
                            SchemeName = Convert.ToString(reader["PriceSchemeName"]),
                            RegularPrice = Convert.ToDecimal(reader["RegularPrice"]),
                            PromoPrice = Convert.ToDecimal(reader["PromoPrice"]),
                            IsRetail = Convert.ToBoolean(reader["IsRetail"]),
                        });
                    }

                }
            }

            return pricePlanInfos;
        }

        public APIResponse AddNewPromotion(AddNewPromoInfo addNewPromoInfo)
        {
            APIResponse aPIResponse = new APIResponse();
            try
            {
                string query = "dbo.usp_LaunchPromoBasedonProdCode";
                using (SqlConnection con = new SqlConnection(this.settings.Value.GQNet))
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ProdCode", addNewPromoInfo.ProductCode);
                        command.Parameters.AddWithValue("@PriceSchemeName", addNewPromoInfo.PriceScheme);
                        command.Parameters.AddWithValue("@PricePlanID", addNewPromoInfo.PricePlanId);
                        command.Parameters.AddWithValue("@StartDate", addNewPromoInfo.StartDate);
                        command.Parameters.AddWithValue("@EndDate", addNewPromoInfo.EndDate);
                        command.Parameters.AddWithValue("@OldEndDate", addNewPromoInfo.OldPromoDate);
                        command.Parameters.AddWithValue("@Currency", addNewPromoInfo.Currency);
                        command.Parameters.AddWithValue("@IRPrice", addNewPromoInfo.IRPromoPrice);
                        command.Parameters.AddWithValue("@ReIRPrice", addNewPromoInfo.IRRegularPrice);
                        command.Parameters.AddWithValue("@RetailPrice", addNewPromoInfo.RetailPromoPrice);
                        command.Parameters.AddWithValue("@ReRetailPrice", addNewPromoInfo.RetailPromoPrice);
                        command.Parameters.AddWithValue("@CUV", addNewPromoInfo.CUV);
                        command.Parameters.AddWithValue("@CountryCode", addNewPromoInfo.CountryCode);
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataReader reader =  command.ExecuteReader();
                        if (reader.Read())
                        {
                            aPIResponse.ErrorMessage = Convert.ToString(reader["Error"]);
                            aPIResponse.IsSuccess = string.IsNullOrEmpty(aPIResponse.ErrorMessage) ? true : false;
                        }
                    }
                }
                aPIResponse.IsSuccess = true;
            }
            catch (Exception ex)
            {
                aPIResponse.IsSuccess = false;
                aPIResponse.ErrorMessage = ex.Message;
            }

            return aPIResponse;
        }

        public APIResponse UpdatePromoInfo(UpdatePromoInfo updatePromoInfo)
        {
            APIResponse aPIResponse = new APIResponse();
            try
            {
                string query = "dbo.usp_UpdateLaunchPromotion";
                using (SqlConnection con = new SqlConnection(this.settings.Value.GQNet))
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PriceSchemeID", updatePromoInfo.PriceSchemeID);
                        command.Parameters.AddWithValue("@EndDate", updatePromoInfo.EndDate);
                        command.Parameters.AddWithValue("@RegularPrice", updatePromoInfo.RegularPrice);
                        command.Parameters.AddWithValue("@PromoPrice", updatePromoInfo.PromoPrice);
                        command.Parameters.AddWithValue("@Currency", updatePromoInfo.Currency);
                        command.Parameters.AddWithValue("@CountryCode", updatePromoInfo.CountryCode);

                        command.ExecuteNonQuery();

                    }
                }
                aPIResponse.IsSuccess = true;
            }
            catch (Exception ex)
            {
                aPIResponse.IsSuccess = false;
                aPIResponse.ErrorMessage = ex.Message;
            }

            return aPIResponse;

        }

        public SelectedPromoInfo GetSelectedPromo(int schemeId)
        {
            SelectedPromoInfo selectedPromoInfo = new SelectedPromoInfo();

            string query = "dbo.usp_GetSelectedPromoInfo";
            using (SqlConnection con = new SqlConnection(this.settings.Value.GQNet))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PriceSchemeID", schemeId);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        selectedPromoInfo.ProductCode = Convert.ToString(reader["ProdCode"]);
                        selectedPromoInfo.PricePlan = Convert.ToString(reader["Description"]);
                        selectedPromoInfo.IRRegularPrice = Convert.ToDecimal(reader["IRRegularPrice"]);
                        selectedPromoInfo.RetailRegularPrice = Convert.ToDecimal(reader["RetailRegularPrice"]);
                        selectedPromoInfo.PromoPrice = Convert.ToDecimal(reader["PromoPrice"]);
                        selectedPromoInfo.RetailPromoPrice = Convert.ToDecimal(reader["RetailPromoPrice"]);
                        selectedPromoInfo.StartDate = Convert.ToString(reader["StartDate"]);
                        selectedPromoInfo.EndtDate = Convert.ToString(reader["EndDate"]);
                        selectedPromoInfo.CUV = Convert.ToString(reader["CUV"]);
                        selectedPromoInfo.CountryCode = Convert.ToString(reader["CountryCode"]);
                        selectedPromoInfo.CountryName = Convert.ToString(reader["CountryName"]);
                        selectedPromoInfo.Currency = Convert.ToString(reader["Currency"]);
                    }

                }
            }


            return selectedPromoInfo;

        }
    }
}

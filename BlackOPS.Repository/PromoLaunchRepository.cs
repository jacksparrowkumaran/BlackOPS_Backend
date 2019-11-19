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
            prefix = prefix.ToLower() ?? string.Empty;
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
                            value = reader["CountryCode"].ToString(),
                            display = Convert.ToString(reader["CountryName"])
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
            string ad = string.Join(",", searchPromo.CountryCode);
            using (SqlConnection con = new SqlConnection(this.settings.Value.GQNet))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ProductCode", searchPromo.ProductCode.Trim());
                    command.Parameters.AddWithValue("@CountryCode", string.Join(",", searchPromo.CountryCode));
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
                            PromoPrice = Convert.ToDecimal(reader["FullPrice"]),
                            IsRetail = Convert.ToBoolean(reader["IsRetail"]),
                            CountryName = Convert.ToString(reader["CountryName"]),
                            CountryCode = Convert.ToString(reader["CountryCode"]),
                            PriceSchemeIds = Convert.ToString(reader["PriceSchemeIds"]),
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
                        // command.Parameters.AddWithValue("@PriceSchemeName", addNewPromoInfo.PriceScheme);
                        command.Parameters.AddWithValue("@PricePlanID", addNewPromoInfo.PricePlanId);
                        command.Parameters.AddWithValue("@StartDate", addNewPromoInfo.StartDate);
                        command.Parameters.AddWithValue("@EndDate", addNewPromoInfo.EndDate);
                        command.Parameters.AddWithValue("@OldEndDate", addNewPromoInfo.OldPromoDate);
                        command.Parameters.AddWithValue("@Currency", addNewPromoInfo.Currency);
                        command.Parameters.AddWithValue("@IRPrice", addNewPromoInfo.IRPromoPrice);
                        command.Parameters.AddWithValue("@ReIRPrice", addNewPromoInfo.IRRegularPrice);
                        command.Parameters.AddWithValue("@RetailPrice", addNewPromoInfo.RetailPromoPrice);
                        command.Parameters.AddWithValue("@ReRetailPrice", addNewPromoInfo.RetailRegularPrice);
                        command.Parameters.AddWithValue("@CUV", addNewPromoInfo.CUV);
                        command.Parameters.AddWithValue("@CountryCode", addNewPromoInfo.CountryCode);
                        command.Parameters.AddWithValue("@ShipFeeSH", addNewPromoInfo.ShipFee);
                        command.Parameters.AddWithValue("@DSP", addNewPromoInfo.RSP);
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            aPIResponse.ErrorMessage = Convert.ToString(reader["Error"]);
                            aPIResponse.IsSuccess = string.IsNullOrEmpty(aPIResponse.ErrorMessage) ? true : false;
                        }
                    }
                }
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
                        command.Parameters.AddWithValue("@PriceSchemeID", updatePromoInfo.PriceSchemeId);
                        command.Parameters.AddWithValue("@ProdCode", updatePromoInfo.ProductCode);
                        command.Parameters.AddWithValue("@PricePlanID", updatePromoInfo.PricePlanId);
                        command.Parameters.AddWithValue("@StartDate", updatePromoInfo.StartDate);
                        command.Parameters.AddWithValue("@EndDate", updatePromoInfo.EndDate);
                        command.Parameters.AddWithValue("@OldEndDate", updatePromoInfo.OldPromoDate);
                        command.Parameters.AddWithValue("@Currency", updatePromoInfo.Currency);
                        command.Parameters.AddWithValue("@IRPrice", updatePromoInfo.IRPromoPrice);
                        command.Parameters.AddWithValue("@ReIRPrice", updatePromoInfo.IRRegularPrice);
                        command.Parameters.AddWithValue("@RetailPrice", updatePromoInfo.RetailPromoPrice);
                        command.Parameters.AddWithValue("@ReRetailPrice", updatePromoInfo.RetailRegularPrice);
                        command.Parameters.AddWithValue("@CUV", updatePromoInfo.CUV);
                        command.Parameters.AddWithValue("@CountryCode", updatePromoInfo.CountryCode);
                        command.Parameters.AddWithValue("@ShipFeeSH", updatePromoInfo.ShipFee);
                        command.Parameters.AddWithValue("@DSP", updatePromoInfo.RSP);

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

        public SelectedPromoInfo GetSelectedPromo(ComboSearchInfo searchPromo)
        {
            SelectedPromoInfo selectedPromoInfo = new SelectedPromoInfo();

            string query = "dbo.usp_GetSelectedPromoInfo";
            using (SqlConnection con = new SqlConnection(this.settings.Value.GQNet))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 120;
                    command.Parameters.AddWithValue("@PriceSchemeIds", searchPromo.PriceSchemeIds);
                    command.Parameters.AddWithValue("@CountryCode", searchPromo.CountryCode);
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
                        selectedPromoInfo.CUV = (Convert.ToInt32(reader["CUV"]) * 1000).ToString();
                        selectedPromoInfo.CountryCode = Convert.ToString(reader["CountryCode"]);
                        selectedPromoInfo.CountryName = Convert.ToString(reader["CountryName"]);
                        selectedPromoInfo.Currency = Convert.ToString(reader["Currency"]);
                        selectedPromoInfo.Currency = Convert.ToString(reader["Currency"]);
                        selectedPromoInfo.ShipFee = Convert.ToDecimal(reader["ShipFee"]);
                        selectedPromoInfo.RSP = Convert.ToDecimal(reader["RSP"]);
                    }

                }
            }


            return selectedPromoInfo;

        }

        public APIResponse AddCombotPromo(AddComboPromoInfo addComboPromo)
        {
            APIResponse aPIResponse = new APIResponse();
            try
            {
                foreach (string country in addComboPromo.CountryCode)
                {
                    foreach (ComboPriceDetails comboPriceDetails in addComboPromo.ComboPriceDetails)
                    {
                        string query = "dbo.usp_AddComboProduct";
                        using (SqlConnection con = new SqlConnection(this.settings.Value.GQNet))
                        {
                            con.Open();
                            using (SqlCommand command = new SqlCommand(query, con))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@MainProductCode", addComboPromo.MainProductCode);
                                command.Parameters.AddWithValue("@MainProdPricePlanId", addComboPromo.PricePlanId);
                                command.Parameters.AddWithValue("@ProdCode", comboPriceDetails.ProductCode);
                                command.Parameters.AddWithValue("@PricePlanID", 16);
                                command.Parameters.AddWithValue("@IRPrice", comboPriceDetails.IRPromoPrice);
                                command.Parameters.AddWithValue("@ReIRPrice", comboPriceDetails.IRPrice);
                                command.Parameters.AddWithValue("@RetailPrice", comboPriceDetails.RetailPromoPrice);
                                command.Parameters.AddWithValue("@ReRetailPrice", comboPriceDetails.RetailPrice);
                                command.Parameters.AddWithValue("@CountryCode", country);

                                SqlDataReader reader = command.ExecuteReader();
                                if (reader.Read())
                                {
                                    aPIResponse.ErrorMessage = Convert.ToString(reader["Error"]);
                                    aPIResponse.IsSuccess = string.IsNullOrEmpty(aPIResponse.ErrorMessage) ? true : false;
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                aPIResponse.IsSuccess = false;
                aPIResponse.ErrorMessage = ex.Message;
            }

            return aPIResponse;
        }

        public APIResponse UpdateCombotPromo(AddComboPromoInfo addComboPromo)
        {
            APIResponse aPIResponse = new APIResponse();
            try
            {
                foreach (string country in addComboPromo.CountryCode)
                {
                    foreach (ComboPriceDetails comboPriceDetails in addComboPromo.ComboPriceDetails)
                    {
                        string query = "[dbo].[usp_UpdateComboProduct]";
                        using (SqlConnection con = new SqlConnection(this.settings.Value.GQNet))
                        {
                            con.Open();
                            using (SqlCommand command = new SqlCommand(query, con))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@PriceSchemeIds", addComboPromo.PriceSchemeIds);
                                command.Parameters.AddWithValue("@ProdCode", comboPriceDetails.ProductCode);
                                command.Parameters.AddWithValue("@PricePlanID", 16);
                                command.Parameters.AddWithValue("@IRPrice", comboPriceDetails.IRPromoPrice);
                                command.Parameters.AddWithValue("@ReIRPrice", comboPriceDetails.IRPrice);
                                command.Parameters.AddWithValue("@RetailPrice", comboPriceDetails.RetailPromoPrice);
                                command.Parameters.AddWithValue("@ReRetailPrice", comboPriceDetails.RetailPrice);
                                command.Parameters.AddWithValue("@CountryCode", country);

                                SqlDataReader reader = command.ExecuteReader();
                                if (reader.Read())
                                {
                                    aPIResponse.ErrorMessage = Convert.ToString(reader["Error"]);
                                    aPIResponse.IsSuccess = string.IsNullOrEmpty(aPIResponse.ErrorMessage) ? true : false;
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                aPIResponse.IsSuccess = false;
                aPIResponse.ErrorMessage = ex.Message;
            }

            return aPIResponse;
        }

        public APIResponse ValidatePromoForCombo(AddComboPromoInfo comboPrice)
        {
            APIResponse aPIResponse = new APIResponse();
            try
            {
                foreach (string country in comboPrice.CountryCode)
                {
                    foreach (ComboPriceDetails comboPriceDetails in comboPrice.ComboPriceDetails)
                    {
                        string query = "[dbo].[usp_ValidatePromoProductForCombo]";
                        using (SqlConnection con = new SqlConnection(this.settings.Value.GQNet))
                        {
                            con.Open();
                            using (SqlCommand command = new SqlCommand(query, con))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@MainProductCode", comboPrice.MainProductCode);
                                command.Parameters.AddWithValue("@MainProdPricePlanId", comboPrice.PricePlanId);
                                command.Parameters.AddWithValue("@ProdCode", comboPriceDetails.ProductCode);
                                command.Parameters.AddWithValue("@PricePlanID", 16);
                                command.Parameters.AddWithValue("@IRPrice", comboPriceDetails.IRPromoPrice);
                                command.Parameters.AddWithValue("@ReIRPrice", comboPriceDetails.IRPrice);
                                command.Parameters.AddWithValue("@RetailPrice", comboPriceDetails.RetailPromoPrice);
                                command.Parameters.AddWithValue("@ReRetailPrice", comboPriceDetails.RetailPrice);
                                command.Parameters.AddWithValue("@CountryCode", country);

                                SqlDataReader reader = command.ExecuteReader();
                                if (reader.Read())
                                {
                                    aPIResponse.ErrorMessage = Convert.ToString(reader["Error"]);
                                    aPIResponse.IsSuccess = string.IsNullOrEmpty(aPIResponse.ErrorMessage) ? true : false;
                                }
                                else
                                {
                                    aPIResponse.IsSuccess = true;
                                }

                            }
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                aPIResponse.IsSuccess = false;
                aPIResponse.ErrorMessage = ex.Message;
            }

            return aPIResponse;

        }

        public ActiveComborPromo GetComboPromoInfo(ComboSearchInfo searchPromo)
        {

            ActiveComborPromo pricePlanInfos = new ActiveComborPromo();
            int _Id = 0;
            string query = "[dbo].[usp_GetActiveComboPromo]";
            string ad = string.Join(",", searchPromo.CountryCode);
            using (SqlConnection con = new SqlConnection(this.settings.Value.GQNet))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PriceSchemeIds", searchPromo.PriceSchemeIds);
                    command.Parameters.AddWithValue("@CountryCode", searchPromo.CountryCode);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        pricePlanInfos.IR_RegularPrice = Convert.ToDecimal(reader["IR_RegularRice"]);
                        pricePlanInfos.IR_PromoPrice = Convert.ToDecimal(reader["IR_RegularRice"]);
                        pricePlanInfos.Retail_RegPrice = Convert.ToDecimal(reader["IR_RegularRice"]);
                        pricePlanInfos.Retail_PromoPrice = Convert.ToDecimal(reader["IR_RegularRice"]);

                    }
                    if (reader.NextResult())
                    {
                        pricePlanInfos.ComboPriceDetails = new List<ActiveComboPriceDetails>();
                        while (reader.Read())
                        {
                            pricePlanInfos.ComboPriceDetails.Add(new ActiveComboPriceDetails
                            {
                                Id = _Id + 1,
                                ProductCode = reader["ProdCode"].ToString(),
                                IRPrice = Convert.ToDecimal(reader["IR_RegularRice"]),
                                IRPromoPrice = Convert.ToDecimal(reader["IR_PromoPrice"]),
                                RetailPrice = Convert.ToDecimal(reader["Retail_Regular"]),
                                RetailPromoPrice = Convert.ToDecimal(reader["Retail_Promo"])
                            });
                        }
                    }
                }
            }

            return pricePlanInfos;
        }
    }
}

namespace JG.FinTech.Domain
{
    using JG.FinTech.Models;
    using Microsoft.Data.Sqlite;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class GiftAidRepository : IGiftAidRepository
    {
        private readonly DonorContext donorContext;
        //private readonly string connString;
        //private readonly string dbFilePath="GiftAid.db";
        private readonly IConfiguration configuration;
        public GiftAidRepository(DonorContext donorContext, IConfiguration configuration)
        {
            this.donorContext = donorContext;
            this.configuration = configuration;
            //this.connString = $"Data Source = {dbFilePath};Version=3; FailIfMissing=True; Foreign Keys=True;";
        }
        public async Task<bool> AddDonorDetails(DonorDetails donorDetails)
        {
            try
            {
                await this.donorContext.DonorDetails.AddAsync(donorDetails).ConfigureAwait(false);
                await this.donorContext.SaveChangesAsync().ConfigureAwait(false);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<bool> DeleteDonor(string donorID)
        {
            throw new NotImplementedException();
        }

        public Task<DonorDetails> FindDonorBy(string donorID)
        {
            var listOfDonors = new List<DonorDetails>();
            string connString = this.configuration["SqliteConn:DonorDBConnString"];
            string sql = donorID;

            try
            {
                using (SqliteConnection conn = new SqliteConnection(connString))
                {
                    conn.Open();
                    Guid id;

                    if (Guid.TryParse(donorID, out id))
                    {
                        sql = $"Select * from DonorDetails where DonorID like '%{donorID}%'";
                    }

                    using (SqliteCommand cmd = new SqliteCommand(sql, conn))
                    {
                        using (SqliteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                listOfDonors.Add(new DonorDetails
                                {
                                    DonorID = reader.GetGuid(0).ToString(),
                                    DonationAmount = reader.GetDouble(3),
                                    GiftAid = reader.GetDouble(4),
                                    Name = reader.GetString(1),
                                    PostCode = reader.GetString(2)
                                });
                            }
                        }
                    }
                }
            }
            catch (SqliteException ex)
            {
                throw ex;
            }

            return Task.FromResult(listOfDonors.FirstOrDefault());
        }

        public Task<bool> UpdateDonor(DonorDetails donorDetails)
        {
            throw new NotImplementedException();
        }
    }
}

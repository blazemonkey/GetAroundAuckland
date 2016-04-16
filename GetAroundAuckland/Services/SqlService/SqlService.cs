﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using GetAroundAuckland.Models;
using System.Configuration;
using System.Diagnostics;

namespace GetAroundAuckland.Services.SqlService
{
    public class SqlService : ISqlService
    {
        public readonly string CONNECTION_STRING = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

        private bool Add<T>(List<T> rows, string selectSql, string insertSql, string updateSql) where T : DbModel
        {            
            using (var conn = new SqlConnection(CONNECTION_STRING))
            {
                try
                {
                    conn.Open();

                    using (var trans = conn.BeginTransaction())
                    {
                        try
                        {
                            foreach (var model in rows)
                            {
                                var selectCmd = new SqlCommand(selectSql, conn);
                                selectCmd.Transaction = trans;
                                model.SetSqlParameters(selectCmd, "Select");
                                var selectReader = selectCmd.ExecuteReader();

                                if (!selectReader.HasRows)
                                {
                                    var insertCmd = new SqlCommand(insertSql, conn);
                                    insertCmd.Transaction = trans;
                                    model.SetSqlParameters(insertCmd, "Insert");
                                    insertCmd.ExecuteNonQuery();
                                }
                                else
                                {
                                    var cmp = model.Compare(selectReader, model);
                                    if (cmp)
                                    {
                                        var updateCmd = new SqlCommand(updateSql, conn);
                                        updateCmd.Transaction = trans;
                                        model.SetSqlParameters(updateCmd, "Update");
                                        updateCmd.ExecuteNonQuery();
                                    }                                    
                                }

                                selectReader.Close();
                            }

                            trans.Commit();
                        }
                        catch (Exception e)
                        {
                            Logger.Error(e);
                            trans.Rollback();
                            throw;
                        }
                    }


                    return true;
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public bool AddAgencies(List<Agency> agencies)
        {
            var stopWatch = new Stopwatch();
            Logger.Info("Inserting Agencies");
            stopWatch.Start();

            var result = Add(agencies, Agency.SelectSql, Agency.InsertSql, Agency.UpdateSql);

            stopWatch.Stop();
            Logger.Info("Finished Insert.");
            Logger.Info("Time Taken (ms): " + stopWatch.ElapsedMilliseconds);
            return result;
        }

        public bool AddCalendars(List<Calendar> calendars)
        {
            var stopWatch = new Stopwatch();
            Logger.Info("Inserting Calendars");
            stopWatch.Start();

            var result = Add(calendars, Calendar.SelectSql, Calendar.InsertSql, Calendar.UpdateSql);

            stopWatch.Stop();
            Logger.Info("Finished Insert.");
            Logger.Info("Time Taken (ms): " + stopWatch.ElapsedMilliseconds);
            return result;
        }
    }
}
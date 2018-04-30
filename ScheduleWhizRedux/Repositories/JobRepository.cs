using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using ScheduleWhizRedux.Interfaces;
using ScheduleWhizRedux.Models;

namespace ScheduleWhizRedux.Repositories
{
    public class JobRepository : Repository, IJobRepository
    {
        /// <summary>
        /// Add a job to the database.
        /// </summary>
        /// <param name="jobTitle"></param>
        /// <returns>True if successful.</returns>
        public bool Add(string jobTitle)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                string insertQuery = "INSERT INTO Jobs (JobTitle) VALUES (@JobTitle);";

                int result = connection.Execute(insertQuery, new
                {
                    JobTitle = jobTitle
                });

                return result != 0;
            }
        }

        /// <summary>
        /// Check if a job exists in the database.
        /// </summary>
        /// <param name="title"></param>
        /// <returns>True if job exists.</returns>
        public bool Exists(string title)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                string query = "select * from Jobs where JobTitle = @JobTitle;";

                var result = connection.QueryFirstOrDefault(query, new { JobTitle = title });

                return result != null;
            }
        }

        /// <summary>
        /// Check if a job exists by job record. Detects whether job ID has been correctly set.
        /// </summary>
        /// <param name="job"></param>
        /// <returns>True if successful.</returns>
        public bool Exists(Job job)
        {
            return job.Id == 0 ? Exists(job.JobTitle) : RecordExists(job);
        }

        /// <summary>
        /// Get a job Id from a job title.
        /// </summary>
        /// <param name="jobTitle"></param>
        /// <returns>Integer for job ID.</returns>
        public int GetId(string jobTitle)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                var queryString = "select Id from Jobs where JobTitle = @JobTitle;";

                int result = connection.QueryFirstOrDefault<int>(queryString,
                    new
                    {
                        JobTitle = jobTitle
                    });

                return result;
            }
        }

        /// <summary>
        /// Get a job object from a job ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Job object.</returns>
        public Job Get(int id)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                var queryString = "select * from Jobs where Id @Id;";

                Job result = connection.Query<Job>(queryString,
                    new
                    {
                        Id = id
                    }).First();

                return result;
            }
        }


        /// <summary>
        /// Get a job record from a job title.
        /// </summary>
        /// <param name="jobTitle"></param>
        /// <returns>Job object.</returns>
        public Job GetRecord(string jobTitle)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                var queryString = "select * from Jobs where JobTitle = @JobTitle;";

                var result = connection.QueryFirstOrDefault<Job>(queryString,
                    new
                    {
                        JobTitle = jobTitle
                    });

                return result;
            }
        }


        /// <summary>
        /// Check if a job exists in the database by job object. More explicit than JobExists.
        /// </summary>
        /// <param name="job"></param>
        /// <returns>True if job exists.</returns>
        public bool RecordExists(Job job)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                string query = "select * from Jobs where JobTitle = @JobTitle;";

                var result = connection.QueryFirstOrDefault<Job>(query,
                    new
                    {
                        job.Id,
                        job.JobTitle
                    });

                return result != null;
            }
        }

        /// <summary>
        /// Get a list of all job records from the database sorted by title.
        /// </summary>
        /// <returns>A list of job objects.</returns>
        public List<Job> GetAllSorted()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                var result = connection.Query<Job>("select * from Jobs order by LOWER(JobTitle);").ToList();

                return result;
            }
        }

        /// <summary>
        /// Get list of all job titles from the database.
        /// </summary>
        /// <returns>A list of strings.</returns>
        public List<string> GetAllTitles()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                var query = "select * from Jobs order by LOWER(JobTitle);";

                var result = connection.Query<Job>(query).ToList();

                List<string> listResult = new List<string>();

                foreach (var jobRecord in result)
                {
                    listResult.Add(jobRecord.JobTitle);
                }

                return listResult;
            }
        }

        /// <summary>
        /// Modify a job record. (Uses ID to determine which record to modify)
        /// </summary>
        /// <param name="job"></param>
        /// <returns>True if successful.</returns>
        public bool Modify(Job job)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                string updateQuery = "UPDATE Jobs SET JobTitle = @JobTitle WHERE Id = @Id;";

                int result = connection.Execute(updateQuery,
                    new
                    {
                        job.JobTitle,
                        job.Id
                    });

                return result != 0;
            }
        }

        /// <summary>
        /// Remove a job from a job object.
        /// </summary>
        /// <param name="job"></param>
        /// <returns>True if successful.</returns>
        public bool Remove(Job job)
        {
            if (job.Id == 0)
            {
                return Remove(job.JobTitle);
            }

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                string deleteQuery = "DELETE FROM Jobs WHERE Id = @Id;";

                int result = connection.Execute(deleteQuery,
                    new
                    {
                        job.Id
                    });

                return result != 0;
            }
        }

        /// <summary>
        /// Remove a job from the database.
        /// </summary>
        /// <param name="jobTitle"></param>
        /// <returns>True if successful.</returns>
        public bool Remove(string jobTitle)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                string deleteQuery = "DELETE FROM Jobs where JobTitle = @JobTitle;";

                int result = connection.Execute(deleteQuery,
                    new
                    {
                        JobTitle = jobTitle
                    });

                return result != 0;
            }
        }
    }
}

using System;
using System.Configuration;
using System.Data.SQLite;
using ScheduleWhizRedux.Interfaces;

namespace ScheduleWhizRedux.Repositories
{
    public abstract class Repository : IRepository
    {
        public IEmployeeRepository Employees => new EmployeeRepository();
        public IJobRepository Jobs => new JobRepository();
        public IAssignedJobRepository AssignedJobs => new AssignedJobRepository();
        public IAssignedShiftRepository AssignedShifts => new AssignedShiftRepository();

        public static string ConnectionString => ConfigurationManager.ConnectionStrings["SWReDB"].ConnectionString;

        public static void CheckIfDatabaseExists()
        {
            if (System.IO.File.Exists("Data\\swredata.db")) return;
            SQLiteConnection.CreateFile("Data\\swredata.db");

            string sql = "SELECT 1; " +
                         "PRAGMA foreign_keys=OFF; " +
                         "BEGIN TRANSACTION; " +
                         "CREATE TABLE [Jobs] (" +
                         "  [Id] INTEGER  NOT NULL" +
                         ", [JobTitle] text  NOT NULL" +
                         ", CONSTRAINT [sqlite_master_PK_Jobs] PRIMARY KEY ([Id])" +
                         " ); " +
                         "CREATE TABLE [Employees] ( " +
                         "  [Id] INTEGER  NOT NULL" +
                         ", [FirstName] text  NOT NULL" +
                         ", [LastName] text  NOT NULL" +
                         ", [EmailAddress] text  NOT NULL" +
                         ", [PhoneNumber] text  NOT NULL" +
                         ", CONSTRAINT [sqlite_master_PK_Employees] PRIMARY KEY ([Id])" +
                         " );" +
                         "CREATE TABLE [AssignedShifts] (" +
                         "  [Id] INTEGER  NOT NULL" +
                         ", [DayOfWeek] bigint  NOT NULL" +
                         ", [JobId] bigint  NOT NULL" +
                         ", [ShiftName] text  NOT NULL" +
                         ", [NumAvailable] bigint  NOT NULL" +
                         ", CONSTRAINT [sqlite_master_PK_AssignedShifts] PRIMARY KEY ([Id])" +
                         " );" +
                         "CREATE TABLE [AssignedJobs] ( " +
                         "  [Id] INTEGER  NOT NULL" +
                         ", [EmployeeId] bigint  NOT NULL" +
                         ", [JobId] bigint  NOT NULL" +
                         ", CONSTRAINT [sqlite_master_PK_AssignedJobs] PRIMARY KEY ([Id])" +
                         " );" +
                         "COMMIT;";

            using (var sqlite = new SQLiteConnection(Repository.ConnectionString))
            {
                sqlite.Open();
                SQLiteCommand command = new SQLiteCommand(sql, sqlite);
                command.ExecuteNonQuery();
            }
        }
    }
}


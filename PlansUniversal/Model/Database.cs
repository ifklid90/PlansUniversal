using System;
using System.IO;
using System.Collections.Generic;
namespace PlansUniversal
{
	public class Database
	{
		public static string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "database.db3");
		public Database()
		{
		}

		public static void saveTask(MainTask task)
		{
			using (var conn = new SQLite.SQLiteConnection(Database.dbPath))
			{
				conn.Insert(task);
			}
		}

		public static void SaveSubtask(SubTask task)
		{
			using (var conn = new SQLite.SQLiteConnection(Database.dbPath))
			{
				conn.Insert(task);
			}
		}

		public static int CountTasks()
		{
			using (var conn = new SQLite.SQLiteConnection(Database.dbPath))
			{
				 return conn.ExecuteScalar<int>("SELECT Count(*) FROM MainTask");
			}
		}

		public static List<MainTask> GetAllTasks()
		{
			using (var conn = new SQLite.SQLiteConnection(Database.dbPath))
			{
				return conn.Query<MainTask>("SELECT * FROM MainTask");
			}
		}

		public static List<MainTask> GetTodayTasks()
		{
			using (var conn = new SQLite.SQLiteConnection(Database.dbPath))
			{
				return conn.Query<MainTask>("SELECT * FROM MainTask WHERE [Date] >= date('now', 'start of day') AND [Date] <= date('now', 'start of day', '+1 day')");
			}
		}

		public static List<MainTask> GetTomorrowTasks()
		{
			using (var conn = new SQLite.SQLiteConnection(Database.dbPath))
			{
				return conn.Query<MainTask>("SELECT * FROM MainTask WHERE [Date] >= date('now', 'start of day', '+1 day') AND [Date] <= date('now', 'start of day', '+2 day')");
			}
		}

		public static List<MainTask> GetThisWeakTasks()
		{
			using (var conn = new SQLite.SQLiteConnection(Database.dbPath))
			{
				return conn.Query<MainTask>("SELECT * FROM MainTask WHERE [Date] >= date('now', 'weekday 1', '-7 day', 'start of day') AND [Date] <= date('now', 'weekday 1', 'start of day')");
			}
		}

		public static List<MainTask> GetNextWeekTasks()
		{
			using (var conn = new SQLite.SQLiteConnection(Database.dbPath))
			{
				return conn.Query<MainTask>("SELECT * FROM MainTask WHERE [Date] >= date('now', 'weekday 1', 'start of day') AND [Date] <= date('now', 'weekday 1', '+7 day', 'start of day')");
			}
		}

		public static List<SubTask> GetSubtasksByMainTaskID(int ID)
		{
			using (var conn = new SQLite.SQLiteConnection(Database.dbPath))
			{
				return conn.Query<SubTask>("SELECT * FROM SubTask WHERE [SuperTaskID] == " + ID);
			}
		}


		public static int CountNextWeekTasks()
		{
			using (var conn = new SQLite.SQLiteConnection(Database.dbPath))
			{
				return conn.ExecuteScalar<int>("SELECT Count(*) FROM MainTask WHERE [Date] >= date('now', 'weekday 1', 'start of day') AND [Date] <= date('now', 'weekday 1', '+7 day', 'start of day')");
			}
		}


		public static int CountThisWeakTasks()
		{
			using (var conn = new SQLite.SQLiteConnection(Database.dbPath))
			{
				return conn.ExecuteScalar<int>("SELECT Count(*) FROM MainTask WHERE [Date] >= date('now', 'weekday 1', '-7 day', 'start of day') AND [Date] <= date('now', 'weekday 1', 'start of day')");
			}
		}

		public static int CountTomorrowTasks()
		{
			using (var conn = new SQLite.SQLiteConnection(Database.dbPath))
			{
				return conn.ExecuteScalar<int>("SELECT Count(*) FROM MainTask WHERE [Date] >= date('now', 'start of day', '+1 day') AND [Date] <= date('now', 'start of day', '+2 day')");
			}
		}

		public static int CountTodayTasks()
		{
			using (var conn = new SQLite.SQLiteConnection(Database.dbPath))
			{
				return conn.ExecuteScalar<int>("SELECT Count(*) FROM MainTask WHERE [Date] >= date('now', 'start of day') AND [Date] <= date('now', 'start of day', '+1 day')");
			}
		}

		public static void DeleteMainTaskById(int id)
		{
			using (var conn = new SQLite.SQLiteConnection(Database.dbPath))
			{
				conn.Delete<MainTask>(id);
				//TODO удаление всех сабтасков
			}
		}

		public static void DeleteSubtaskByID(int id)
		{
			using (var conn = new SQLite.SQLiteConnection(Database.dbPath))
			{
				conn.Delete<SubTask>(id);
			}
		}
	}
}

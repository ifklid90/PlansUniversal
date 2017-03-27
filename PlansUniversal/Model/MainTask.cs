using System;
using SQLite;
namespace PlansUniversal
{
	public class MainTask
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string Title { get; set; }
		public DateTime Date { get; set; }
		public DateTime Time { get; set; }
		public string Description { get; set; }

		public override string ToString()
		{
			return string.Format("[MainTask: ID={0}, Title={1}, date = {2}, time = {3}, description {4}]", ID, Title, Date, Time, Description);
		}
	}
}

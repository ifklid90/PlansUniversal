using System;
using SQLite;
namespace PlansUniversal
{
	public class SubTask
	{
		public SubTask()
		{
		}
			
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public int SuperTaskID { get; set; }
		public string Title { get; set; }
		public bool Done { get; set; }

		public override string ToString()
		{
			return string.Format("[MainTask: ID={0}, Title={1}, SuperTaskID = {2}]", ID, Title, SuperTaskID);
		}

	}
}

using System;
using SQLite;
namespace PlansUniversal
{
	public class MainTask
	{											
		[PrimaryKey, AutoIncrement]				
		public int ID { get; set; }				
		public string Title { get; set; }		
		public string Comment { get; set; }		
		public bool Done { get; set; }			
		public DateTime Date { get; set; }		
		public DateTime Time { get; set; }			
		public string Description { get; set; }		
		public string Image { get; set; }
		public double Longtitude { get; set; }
		public double Latitude { get; set; }
													
													
													
		public override string ToString()			
		{
			return string.Format("[MainTask: ID={0}, Title={1}, date = {2}, time = {3}, description {4}]", ID, Title, Date, Time, Description);
		}
	}
}

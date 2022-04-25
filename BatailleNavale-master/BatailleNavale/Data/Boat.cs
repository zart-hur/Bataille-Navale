namespace BatailleNavale
{
	public class Boat
	{
		public Char Name { get; set; }
		public int? Id { get; set; }
		public int? Size { get; set; }
		public int? Health { get; set; }
		public int x0 { get; set; } = -1;
		public int y0 { get; set; } = -1;
		public Char Orientation { get; set; } 
		public Boat(int size, int id)
		{
			Size = size;
			Id = id;
			Health = size;
			IdToName();
		}

		public void LooseHealth()
		{
			Health--;

		}

		public void IdToName()
		{
			switch (Id)
			{
				case 0:
					Name = 'A';
					break;
				case 1:
					Name = 'B';
					break;
				case 2:
					Name = 'C';
					break;
				case 3:
					Name = 'D';
					break;
				case 4:
					Name = 'E';
					break;
			}
		}

	}
}